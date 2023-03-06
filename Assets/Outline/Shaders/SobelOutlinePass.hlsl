float4 _OutlineColor;
float4 _BackgroundColor;

TEXTURE2D(_CameraDepthTexture);
SAMPLER(sampler_CameraDepthTexture);

float sobel(Texture2D tex, int2 uv)
{
	int3 uv3 = int3(uv, 0);
	float3 sum = 0;

	sum += tex.Load(clampuv(uv3, +1, +0)).rgb * 1;
	sum += tex.Load(clampuv(uv3, +0, +1)).rgb * 1;
	sum += tex.Load(clampuv(uv3, +1, +1)).rgb * 1;
	sum += tex.Load(clampuv(uv3, -1, +0)).rgb * 1;
	sum += tex.Load(clampuv(uv3, +0, -1)).rgb * 1;
	sum += tex.Load(clampuv(uv3, -1, +1)).rgb * 1;
	sum += tex.Load(clampuv(uv3, +1, -1)).rgb * 1;
	sum += tex.Load(clampuv(uv3, -1, -1)).rgb * 1;
	sum += tex.Load(clampuv(uv3, +0, +0)).rgb * -8;

	return 1 - step(length(sum),0);
}

float4 frag(v2f i) : SV_Target
{
	int2 uvInt = i.pos.xy;
	float edge = sobel(_MainTex, uvInt);

	// float rawDepth = _CameraDepthTexture.Load(int3(uvInt,0));
	// return Linear01Depth(rawDepth, _ZBufferParams);

	float4 final = lerp(_BackgroundColor, _OutlineColor, edge);
	final.a = 1;
	return final;
}