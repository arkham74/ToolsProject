Shader "Hidden/PathTrace"
{
	Properties
	{
		[MainTexture][HideInInspector] _MainTex ("Texture", 2D) = "white" {}
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

			struct appdata
			{
				uint vertexID : SV_VertexID;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

// float4x4 unity_CameraProjection;
// float4x4 unity_CameraInvProjection;
// float4x4 unity_WorldToCamera;
// float4x4 unity_CameraToWorld;

// float3 _WorldSpaceCameraPos;

// float4 _ProjectionParams;
// float4 _ScreenParams;
// float4 _ZBufferParams;
// float4 unity_OrthoParams;

// float4 unity_CameraWorldClipPlanes[6];

			// sphere of size ra centered at point ce
			float TraceSphere( float3 origin, float3 direction, float3 center, float radius )
			{
				float3 oc = origin - center;
				float b = dot( oc, direction );
				float c = dot( oc, oc ) - radius*radius;
				float h = b*b - c;
				if( h<0.0 ) return -1.0; // no intersection
				return 1;
			}

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = GetFullScreenTriangleVertexPosition(v.vertexID, _ProjectionParams.y);
				o.uv = GetFullScreenTriangleTexCoord(v.vertexID);
				return o;
			}

			float4 frag (v2f i) : SV_Target
			{
				float4 cameraSpace = mul(unity_CameraInvProjection, float4(i.uv - 0.5, 1, 1));
				float4 worldSpace = mul(unity_CameraToWorld, cameraSpace);
				float3 direction = normalize(worldSpace.xyz - _WorldSpaceCameraPos);
				return TraceSphere(_WorldSpaceCameraPos, direction, float3(0,0,0), 1);
			}
			ENDHLSL
		}
	}
}
