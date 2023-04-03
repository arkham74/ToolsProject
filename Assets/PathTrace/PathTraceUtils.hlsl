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

HitInfo TraceSphere( Ray ray, Sphere sphere )
{
	float3 oc = ray.origin - sphere.center;
	float b = dot( oc, ray.direction );
	float c = dot( oc, oc ) - sphere.radius * sphere.radius;
	float h = b * b - c;

	HitInfo hitInfo = (HitInfo)0;
	hitInfo.hit = step(0, h);
	hitInfo.distance = -b-sqrt(h);
	hitInfo.position = ray.origin + ray.direction * hitInfo.distance;
	hitInfo.normal = normalize(hitInfo.position - sphere.center);

	return hitInfo;
}