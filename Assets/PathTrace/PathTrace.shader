Shader "Hidden/PathTrace"
{
	Properties
	{
	}

	SubShader
	{
		Cull Off
		ZWrite Off
		ZTest Always
		Blend Off

		Pass
		{
			Name "PathTracePass"

			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "PathTraceUtils.hlsl"

			struct appdata
			{
				uint vertexID : SV_VertexID;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldPos : TEXCOORD1;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = GetFullScreenTriangleVertexPosition(v.vertexID, _ProjectionParams.y);
				o.uv = GetFullScreenTriangleTexCoord(v.vertexID);
				o.worldPos = mul(unity_ObjectToWorld, o.vertex).xyz;
				return o;
			}

			float4 frag (v2f i) : SV_Target
			{
				float nearPlane = _ProjectionParams.y;
				float aspect = _ScreenParams.x / _ScreenParams.y;
				
				float fov = 2.0 * atan(1.0 / unity_CameraProjection[1][1]);
				float height = nearPlane * tan(fov * 0.5f) * 2;
				float width = aspect * height;

				float4 viewParams = float4(width, height, nearPlane, 1);
				float4 cameraSpace = viewParams * float4(i.uv - 0.5, 1, 1);
				float4 worldSpace = mul(unity_CameraToWorld, cameraSpace);

				Ray ray = (Ray)0;
				ray.origin = _WorldSpaceCameraPos;
				ray.direction = normalize(worldSpace.xyz - _WorldSpaceCameraPos);

				uint2 pixelCoord = i.vertex.xy;
				uint seed = pixelCoord.x | (pixelCoord.y << 16);

				return float4(Trace(ray, seed), 1);
			}
			ENDHLSL
		}
	}
}
