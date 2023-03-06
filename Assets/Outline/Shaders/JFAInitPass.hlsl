float sobel(Texture2D tex, int2 uv)
{
	int3 uv3 = int3(uv, 0);
	float3 sum = 0;

	sum += tex.Load(clampuv(uv3, +1, +0)).rgb * 1;
	sum += tex.Load(clampuv(uv3, +0, +1)).rgb * 1;
	sum += tex.Load(clampuv(uv3, +1, +1)).rgb * 1;

	// sum += tex.Load(clampuv(uv3, -1, +0)).rgb * 1;
	// sum += tex.Load(clampuv(uv3, +0, -1)).rgb * 1;
	// sum += tex.Load(clampuv(uv3, -1, +1)).rgb * 1;
	// sum += tex.Load(clampuv(uv3, +1, -1)).rgb * 1;
	// sum += tex.Load(clampuv(uv3, -1, -1)).rgb * 1;
	
	sum += tex.Load(clampuv(uv3, +0, +0)).rgb * -3;

	return 1-step(length(sum),0);
}

float2 frag(v2f i) : SV_Target
{
	int2 uvInt = i.pos.xy;
	float edge = sobel(_MainTex, uvInt);
	return lerp(FLOOD_NULL_POS_FLOAT2, i.uv, edge);
}