TEXTURE2D(_DBufferTexture0);
SAMPLER(sampler_DBufferTexture0);

float4 frag (v2f i) : SV_Target
{
	float4 decal = _DBufferTexture0.Sample(sampler_DBufferTexture0, i.uv);
	decal.a = 1 - decal.a;
	return decal;
}