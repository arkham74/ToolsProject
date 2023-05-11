float AA(float dist)
{
	float distanceChange = fwidth(dist) * 1;
	return smoothstep(distanceChange, -distanceChange, dist);;
}

float sdRoundedBox( in float2 p, in float2 b, in float4 r )
{
	r.xy = (p.x>0.0) ? r.xy : r.zw;
	r.x = (p.y>0.0) ? r.x : r.y;
	float2 q = abs(p) - b + r.x;
	return min(max(q.x, q.y), 0.0) + length(max(q, 0.0)) - r.x;
}

float dot2 (in float2 vec)
{
	return dot(vec, vec);
}

float sdHeart( in float2 p )
{
	p.x = abs(p.x);

	if( p.y+p.x>1.0 )
		return sqrt(dot2(p-float2(0.25,0.75))) - sqrt(2.0)/4.0;

	return sqrt(min(dot2(p-float2(0.00,1.00)), dot2(p-0.5*max(p.x+p.y,0.0)))) * sign(p.x-p.y);
}

float opRound( in float p, in float r )
{
  return p - r;
}

float opOnion( in float p, in float r )
{
	return abs(p) - r;
}

float2 center(float2 p)
{
	return p * 2 - 1;
}