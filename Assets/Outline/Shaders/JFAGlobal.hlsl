#define FLOOD_INF 1.#INF
#define FLOOD_NULL_POS -1.0
#define FLOOD_NULL_POS_FLOAT2 float2(FLOOD_NULL_POS, FLOOD_NULL_POS)

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

struct appdata
{
	uint vertexID : SV_VertexID;
	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct v2f
{
	float2 uv : TEXCOORD0;
	float4 pos : SV_POSITION;
	UNITY_VERTEX_OUTPUT_STEREO
};

v2f vert(appdata v)
{
	v2f o;
	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	o.pos = GetFullScreenTriangleVertexPosition(v.vertexID);
	o.uv = GetFullScreenTriangleTexCoord(v.vertexID);
	return o;
}

TEXTURE2D(_MainTex);
SAMPLER(sampler_MainTex);

TEXTURE2D(_OutlineTargetMask);
SAMPLER(sampler_OutlineTargetMask);

CBUFFER_START(UnityPerMaterial)
	float4 _MainTex_TexelSize;
	float _Thickness;
	float _DepthSensitivity;
	float _NormalSensitivity;
	float _ColorSensitivity;
	float4 _Color;
CBUFFER_END

int3 clampuv(int3 uv, int x, int y)
{
	uv.xy = clamp(uv.xy + int2(x,y), int2(0,0), (int2)_MainTex_TexelSize.zw - 1);
	return uv;
}