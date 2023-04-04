Shader "Hidden/DrawShader"
{
	Properties
	{
		[MainTexture][HideInInspector] _MainTex ("Texture", 2D) = "white" {}
	}
	
	SubShader
	{
		Cull Off
		ZWrite Off
		ZTest Off
		Blend Off

		Pass
		{
			Name "DrawShader"

			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

			struct appdata
			{
				uint vertexID : SV_VertexID;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			struct Line
			{
				float3 start;
				float3 end;
				float4 color;
				float width;
			};

			v2f vert(appdata v)
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.vertex = GetFullScreenTriangleVertexPosition(v.vertexID);
				o.uv = GetFullScreenTriangleTexCoord(v.vertexID);
				return o;
			}

			TEXTURE2D_X(_MainTex);
			SAMPLER(sampler_MainTex);

			TEXTURE2D_X(_CameraDepthTexture);
			SAMPLER(sampler_CameraDepthTexture);

			StructuredBuffer<Line> _Lines;
			int _LineCount;

			float4 WorldToScreen(float3 world)
			{
				float4 camera = mul(unity_WorldToCamera, float4(world,1));
				float4 clip = mul(unity_CameraProjection, camera);
				return ComputeScreenPos(clip);
			}

			int2 WorldToPixel(float3 world)
			{
				float4 screen = WorldToScreen(world);
				screen.w = -abs(screen.w);
				int2 pixel = (screen.xyz / screen.w).xy * _ScreenParams.xy;
				pixel.x = _ScreenParams.x - pixel.x;
				return pixel;
			}

			float DrawLine(int2 p, int2 a, int2 b, float width, out float h)
			{
				float w2 = width * (_ScreenParams.y / 1080.0);
				float2 pa = p-a;
				float2 ba = b-a;
				h = clamp( dot(pa, ba) / dot(ba, ba), 0.0, 1.0 );
				float dist = length( pa - ba * h ) - w2;
				float change = fwidth(dist) * 0.5;
				float aa = smoothstep(change, -change, dist);
				return aa;
			}

			float4 frag(v2f i) : SV_Target
			{
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

				float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, i.uv).r;
				depth = LinearEyeDepth(depth, _ZBufferParams);

				float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
				int2 pos = i.uv * _ScreenParams.xy;

				for(int l = 0; l < _LineCount; l++)
				{
					float3 start = _Lines[l].start;
					float3 end = _Lines[l].end;
					float width = _Lines[l].width;

					int2 startPos = WorldToPixel(start);
					int2 endPos = WorldToPixel(end);
					float t = 0;
					float mask = DrawLine(pos, startPos, endPos, abs(width), t);

					float3 world = lerp(start, end, t);
					float4 camera = mul(unity_WorldToCamera, float4(world,1));
					VertexPositionInputs positionInputs = GetVertexPositionInputs(camera.xyz);
					float d = positionInputs.positionVS.z;
					float depthMask = step(0, depth - d);

					mask *= depthMask;
					color *= 1 - mask;
					color += mask * _Lines[l].color;
				}

				return color;
			}
			ENDHLSL
		}
	}
}