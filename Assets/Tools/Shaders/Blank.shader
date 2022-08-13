Shader "Hidden/Blank"
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

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

			struct appdata
			{
				uint vertexID : SV_VertexID;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			TEXTURE2D_X(_MainTex);
			SAMPLER(sampler_MainTex);

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = GetFullScreenTriangleVertexPosition(v.vertexID);
				o.uv = GetFullScreenTriangleTexCoord(v.vertexID);
				return o;
			}

			float3 frag(v2f i) : SV_Target
			{
				float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
				return color.rgb;
			}
			ENDHLSL
		}
	}
}