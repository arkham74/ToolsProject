int _StepWidth;

float2 frag(v2f i) : SV_Target
{
	// integer pixel position
	int2 uvInt = i.pos.xy;

	// initialize best distance at infinity
	float bestDist = FLOOD_INF;
	float2 bestCoord;

	// jump samples
	UNITY_UNROLL
	for(int u=-1; u<=1; u++)
	{
		UNITY_UNROLL
		for(int v=-1; v<=1; v++)
		{
			// calculate offset sample position
			int2 offsetUV = uvInt + int2(u, v) * _StepWidth;

			// .Load() acts funny when sampling outside of bounds, so don't
			offsetUV = clamp(offsetUV, int2(0,0), (int2)_MainTex_TexelSize.zw - 1);

			// decode position from buffer
			float2 offsetPos = _MainTex.Load(int3(offsetUV, 0)).rg * _MainTex_TexelSize.zw;

			// the offset from current position
			float2 disp = i.pos.xy - offsetPos;

			// square distance
			float dist = dot(disp, disp);

			// if offset position isn't a null position or is closer than the best
			// set as the new best and store the position
			if (offsetPos.y != FLOOD_NULL_POS && dist < bestDist)
			{
				bestDist = dist;
				bestCoord = offsetPos;
			}
		}
	}

	float mask = _MainTex.Load(int3(uvInt, 0)).a;
	// if not valid best distance output null position, otherwise output encoded position
	float2 result = isinf(bestDist) ? FLOOD_NULL_POS_FLOAT2 : bestCoord * _MainTex_TexelSize.xy;
	return result;
}