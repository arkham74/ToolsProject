struct Ray
{
	float3 origin;
	float3 direction;
};

struct Material
{
	float4 color;
};

struct Sphere
{
	float3 center;
	float radius;
	Material material;
};

struct HitInfo
{
	bool hit;
	float distance;
	float3 position;
	float3 normal;
};