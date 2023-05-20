//based on https://gist.github.com/bgolus/a18c1a3fc9af2d73cc19169a809eb195

Shader "Hidden/OutlineJFASobel"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}

	SubShader
	{
		HLSLINCLUDE

		#define FLOOD_INF 1.#INF
		#define FLOOD_NULL_POS -1.0
		#define FLOOD_NULL_POS_FLOAT2 float2(FLOOD_NULL_POS, FLOOD_NULL_POS)

		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

		struct appdata
		{
			float4 vertex : POSITION;
		};

		struct v2f
		{
			float4 pos : SV_POSITION;
		};

		v2f vert(appdata v)
		{
			v2f o;
			o.pos = TransformObjectToHClip(v.vertex.rgb);
			return o;
		}

		TEXTURE2D(_MainTex);
		SAMPLER(sampler_MainTex);

		TEXTURE2D(_OutlineTargetMask);
		SAMPLER(sampler_OutlineTargetMask);

		CBUFFER_START(UnityPerMaterial)
			float4 _MainTex_TexelSize;
		CBUFFER_END
		ENDHLSL

		Cull Off
		ZWrite Off
		ZTest Always

		Pass //0
		{
			Name "JFAFILL"

			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float frag(v2f i) : SV_Target
			{
				return 1;
			}
			ENDHLSL
		}

		Pass //1
		{
			Name "JFAINIT"

			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float2 frag(v2f i) : SV_Target
			{
				// integer pixel position
				int2 uvInt = i.pos.xy;

				// sample silhouette texture for sobel
				half3x3 values;
				UNITY_UNROLL
				for(int u=0; u<3; u++)
				{
					UNITY_UNROLL
					for(int v=0; v<3; v++)
					{
						uint2 sampleUV = clamp(uvInt + int2(u-1, v-1), int2(0,0), (int2)_MainTex_TexelSize.zw - 1);
						values[u][v] = _MainTex.Load(int3(sampleUV, 0)).r;
					}
				}

				// calculate output position for this pixel
				float2 outPos = i.pos.xy * abs(_MainTex_TexelSize.xy);

				// interior, return position
				if (values._m11 > 0.99)
				return outPos;

				// exterior, return no position
				if (values._m11 < 0.01)
				return FLOOD_NULL_POS_FLOAT2;

				// sobel to estimate edge direction
				float2 dir = -float2(
				values[0][0] + values[0][1] * 2.0 + values[0][2] - values[2][0] - values[2][1] * 2.0 - values[2][2],
				values[0][0] + values[1][0] * 2.0 + values[2][0] - values[0][2] - values[1][2] * 2.0 - values[2][2]
				);

				// if dir length is small, this is either a sub pixel dot or line
				// no way to estimate sub pixel edge, so output position
				if (abs(dir.x) <= 0.005 && abs(dir.y) <= 0.005)
				return outPos;

				// normalize direction
				dir = normalize(dir);

				// sub pixel offset
				float2 offset = dir * (1.0 - values._m11);

				// output encoded offset position
				return (i.pos.xy + offset) * abs(_MainTex_TexelSize.xy);
			}
			ENDHLSL
		}

		Pass //2
		{
			Name "JFAVORONOI"

			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			int _StepWidth;

			float2 frag(v2f i) : SV_Target
			{
				// integer pixel position
				int2 uvInt = i.pos.xy;

				// initialize best distance at infinity
				float bestDist = FLOOD_INF;
				float2 bestCoord;

				// jump samples
				UNITY_UNROLL
				for(int u=-1; u<=1; u++)
				{
					UNITY_UNROLL
					for(int v=-1; v<=1; v++)
					{
						// calculate offset sample position
						int2 offsetUV = uvInt + int2(u, v) * _StepWidth;

						// .Load() acts funny when sampling outside of bounds, so don't
						offsetUV = clamp(offsetUV, int2(0,0), (int2)_MainTex_TexelSize.zw - 1);

						// decode position from buffer
						float2 offsetPos = _MainTex.Load(int3(offsetUV, 0)).rg * _MainTex_TexelSize.zw;

						// the offset from current position
						float2 disp = i.pos.xy - offsetPos;

						// square distance
						float dist = dot(disp, disp);

						// if offset position isn't a null position or is closer than the best
						// set as the new best and store the position
						if (offsetPos.y != FLOOD_NULL_POS && dist < bestDist)
						{
							bestDist = dist;
							bestCoord = offsetPos;
						}
					}
				}

				float mask = _MainTex.Load(int3(uvInt, 0)).a;
				// if not valid best distance output null position, otherwise output encoded position
				float2 result = isinf(bestDist) ? FLOOD_NULL_POS_FLOAT2 : bestCoord * _MainTex_TexelSize.xy;
				return result;
			}
			ENDHLSL
		}

		Pass //3
		{
			Name "JFAOUTLINE"
			Blend SrcAlpha OneMinusSrcAlpha

			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4 _OutlineColor;
			float _OutlineWidth;

			float4 frag(v2f i) : SV_Target
			{
				// integer pixel position
				int2 uvInt = i.pos.xy;

				// load encoded position
				float2 encodedPos = _MainTex.Load(int3(uvInt, 0)).rg;
				float mask = _OutlineTargetMask.Load(int3(uvInt, 0)).r;

				// early out if null position
				if (encodedPos.y == FLOOD_NULL_POS || mask > 0.5)
				return float4(0,0,0,0);

				// decode closest position
				float2 nearestPos = encodedPos * abs(_ScreenParams.xy);

				// current pixel position
				float2 currentPos = i.pos.xy;

				// distance in pixels to closest position
				float dist = length(nearestPos - currentPos);

				// calculate outline
				// + 1.0 is because encoded nearest position is half a pixel inset
				// not + 0.5 because we want the anti-aliased edge to be aligned between pixels
				// distance is already in pixels, so this is already perfectly anti-aliased!
				float outline = saturate(_OutlineWidth - dist + 1.0);

				// apply outline to alpha
				float4 col = _OutlineColor;
				col.a = outline;

				// profit!
				return col;
			}
			ENDHLSL
		}

		Pass //4
		{
			Name "SOBELOUTLINE"

			Blend SrcAlpha OneMinusSrcAlpha

			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4 _OutlineColor;

			float sobel(Texture2D tex, int2 uv)
			{
				int3 uv3 = int3(uv, 0);
				float sum = 0;

				sum += tex.Load(uv3 + int3(-1, 0, 0)).r * 1;
				sum += tex.Load(uv3 + int3(1, 0, 0)).r * 1;
				sum += tex.Load(uv3 + int3(0, -1, 0)).r * 1;
				sum += tex.Load(uv3 + int3(0, 1, 0)).r * 1;
				sum += tex.Load(uv3 + int3(0, 0, 0)).r * -4;

				return sum;
			}

			float4 frag(v2f i) : SV_Target
			{
				int2 uvInt = i.pos.xy;
				float edge = sobel(_MainTex, uvInt);
				float4 color = _OutlineColor;
				color.a = step(0.5, edge);
				return color;
			}
			ENDHLSL
		}
	}
}
