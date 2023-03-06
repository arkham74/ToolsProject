#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

struct appdata_surface
{
	float4 positionOS : POSITION;
	float4 color : COLOR;
};

struct v2f_surface
{
	float4 positionCS : SV_POSITION;
	float3 objectPosition : TEXCOORD7;
	float4 color : COLOR;
};

float random(float2 seed, float min, float max)
{
	float randomno = frac(sin(dot(seed, float2(12.9898, 78.233)))*43758.5453);
	return lerp(min, max, randomno);
}

float3 random_color(float3 pos)
{
	float3 color;
	color.r = random(pos.x + pos.y, 0, 1);
	color.g = random(pos.x + pos.z, 0, 1);
	color.b = random(pos.z + pos.y, 0, 1);
	return color;
}

v2f_surface vert (appdata_surface v)
{
		v2f_surface o;
		VertexPositionInputs positionInputs = GetVertexPositionInputs(v.positionOS.xyz);
		o.positionCS = positionInputs.positionCS;
		o.objectPosition = UNITY_MATRIX_M._m03_m13_m23;
		o.color = v.color;
		return o;
}

float4 frag (v2f_surface i) : SV_Target
{
	float3 seed = i.color.rgb + i.objectPosition.rgb;
	float4 color = 1;
	color.rgb = random_color(seed);
	return color;
}