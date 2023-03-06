TEXTURE2D(_CameraDepthTexture);
SAMPLER(sampler_CameraDepthTexture);

TEXTURE2D(_CameraNormalsTexture);
SAMPLER(sampler_CameraNormalsTexture);

// float3 DecodeNormal(float4 enc)
// {
// 	float kScale = 1.7777;
// 	float3 nn = enc.xyz*float3(2*kScale,2*kScale,0) + float3(-kScale,-kScale,1);
// 	float g = 2.0 / dot(nn.xyz,nn.xyz);
// 	float3 n;
// 	n.xy = g*nn.xy;
// 	n.z = g-1;
// 	return n;
// }

static const int SobelX[9] = { 1, 0, -1, 2, 0, -2, 1, 0, -1 };
static const int SobelY[9] = { 1, 2, 1, 0, 0, 0, -1, -2, -1 };


float4 load(float2 uv, int2 offset, Texture2D tex)
{
	int2 pos = int2(uv * _ScreenParams.xy);
	pos += offset;
	pos = clamp(pos, int2(0,0), _ScreenParams.xy-1);
	return tex.Load(int3(pos, 0));
}

float sobel(float2 uv, Texture2D tex)
{
	float sum = 0;
	sum += load(uv, int2(+1, +0), tex).r * +1;
	sum += load(uv, int2(+0, +1), tex).r * +1;
	sum += load(uv, int2(+1, +1), tex).r * +1;
	sum += load(uv, int2(-1, +0), tex).r * +1;
	sum += load(uv, int2(+0, -1), tex).r * +1;
	sum += load(uv, int2(-1, +1), tex).r * +1;
	sum += load(uv, int2(+1, -1), tex).r * +1;
	sum += load(uv, int2(-1, -1), tex).r * +1;
	sum += load(uv, int2(+0, +0), tex).r * -8;
	return sum;
}

float4 frag (v2f i) : SV_Target
{
	float color = sobel(i.uv, _MainTex) * _ColorSensitivity;
	float depth = sobel(i.uv, _CameraDepthTexture) * _DepthSensitivity;
	float normal = sobel(i.uv, _CameraNormalsTexture) * _NormalSensitivity;
	float final = max(color, max(depth, normal));
	final = 1-step(final,0);
	return float4(_Color.rgb * final, 1);
}