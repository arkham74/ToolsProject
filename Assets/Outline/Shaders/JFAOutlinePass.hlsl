float4 _OutlineColor;
float _OutlineWidth;

float4 frag(v2f i) : SV_Target
{
	// integer pixel position
	int2 uvInt = i.pos.xy;

	// load encoded position
	float2 encodedPos = _MainTex.Load(int3(uvInt, 0)).rg;

	// early out if null position
	if (encodedPos.y == FLOOD_NULL_POS)
	return float4(0,0,0,1);

	// decode closest position
	float2 nearestPos = encodedPos * abs(_ScreenParams.xy);

	// current pixel position
	float2 currentPos = i.pos.xy;

	// distance in pixels to closest position
	float dist = length(nearestPos - currentPos);

	// calculate outline
	// + 1.0 is because encoded nearest position is half a pixel inset
	// not + 0.5 because we want the anti-aliased edge to be aligned between pixels
	// distance is already in pixels, so this is already perfectly anti-aliased!
	float outline = step(dist, _OutlineWidth + 1.0);

	//apply color
	float4 col = outline * _OutlineColor;
	col.a = 1;

	// profit!
	return col;
}