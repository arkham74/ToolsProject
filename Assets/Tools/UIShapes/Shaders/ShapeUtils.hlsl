float AA(float dist)
{
	float distanceChange = fwidth(dist) * 1;
	return smoothstep(distanceChange, -distanceChange, dist);
}

float sdRoundedBox( in float2 p, in float2 b, in float4 r )
{
	r.xy = (p.x>0.0) ? r.xy : r.zw;
	r.x = (p.y>0.0) ? r.x : r.y;
	float2 q = abs(p) - b + r.x;
	return min(max(q.x, q.y), 0.0) + length(max(q, 0.0)) - r.x;
}

float dot2 (in float2 p)
{
	return dot(p, p);
}

float sdHeart( in float2 p )
{
	p.x = abs(p.x);

	if( p.y+p.x>1.0 )
	return sqrt(dot2(p-float2(0.25,0.75))) - sqrt(2.0)/4.0;

	return sqrt(min(dot2(p-float2(0.00,1.00)), dot2(p-0.5*max(p.x+p.y,0.0)))) * sign(p.x-p.y);
}

float sdTriangle( in float2 p, in float r )
{
	const float k = sqrt(3.0);
	p.x = abs(p.x) - r;
	p.y = p.y + r/k;
	if( p.x+k*p.y>0.0 ) p = float2(p.x-k*p.y,-k*p.x-p.y)/2.0;
	p.x -= clamp( p.x, -2.0*r, 0.0 );
	return -length(p)*sign(p.y);
}

float mod(float x, float y)
{
	return x - y * floor(x / y);
}

float sdStar(in float2 p, in float r, in int n, in float m) // m=[2,n]
{
	// these 4 lines can be precomputed for a given shape
	float an = 3.141593 / float(n);
	float en = 3.141593 / m;
	float2 acs = float2(cos(an), sin(an));
	float2 ecs = float2(cos(en), sin(en)); // ecs=float2(0,1) and simplify, for regular polygon,

	// reduce to first sector
	float bn = mod(atan2(p.x, p.y), 2.0 * an) - an;
	p = length(p) * float2(cos(bn), abs(sin(bn)));

	// line sdf
	p -= r * acs;
	p += ecs * clamp(-dot(p, ecs), 0.0, r * acs.y / ecs.y);
	return length(p) * sign(p.x);
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

float sdSegment(float2 p, float2 a, float2 b)
{
	float2 pa = p-a;
	float2 ba = b-a;
	float h = clamp(dot(pa, ba) / dot(ba, ba), 0.0, 1.0);
	return length(pa - ba * h);
}

float4 remap(float4 In, float2 InMinMax, float2 OutMinMax)
{
	return OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
}

float4 remap(float4 In, float InMin, float InMax, float OutMin, float OutMax)
{
	return remap(In, float2(InMin, InMax), float2(OutMin, OutMax));
}