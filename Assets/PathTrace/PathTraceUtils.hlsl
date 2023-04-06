struct Ray
{
	float3 origin;
	float3 direction;
};

struct Material
{
	float4 color;
	float4 emission;
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
	Material material;
};

HitInfo TraceSphere( Ray ray, Sphere sphere )
{
	HitInfo hitInfo = (HitInfo)0;

	float3 oc = ray.origin - sphere.center;
	float b = dot( oc, ray.direction );
	float c = dot( oc, oc ) - sphere.radius * sphere.radius;
	float h = b * b - c;

	if(h >= 0)
	{
		hitInfo.distance = -b - sqrt(h);
		if(hitInfo.distance >= 0)
		{
			hitInfo.hit = true;
			hitInfo.position = ray.origin + ray.direction * hitInfo.distance;
			hitInfo.normal = normalize(hitInfo.position - sphere.center);
			hitInfo.material = sphere.material;
		}
	}

	return hitInfo;
}

StructuredBuffer<Sphere> _Spheres;
int _SphereCount;

HitInfo TraceAllSpheres(Ray ray)
{
	HitInfo hitInfo = (HitInfo)0;
	hitInfo.distance = 1.#INF;

	for(int i = 0; i < _SphereCount; i++)
	{
		Sphere sphere = _Spheres[i];
		HitInfo hit = TraceSphere(ray, sphere);

		if(hit.hit && hit.distance < hitInfo.distance)
		{
			hitInfo = hit;
		}
	}

	return hitInfo;
}

uint _Bounces;

float random(inout uint seed)
{
	seed = seed * 747796405 + 2891336453;
	uint result = ((seed >> ((seed >> 28) + 4)) ^ seed) * 277803737;
	result = (result >> 22) ^ result;
	float value = float(result) / 4294967295.0;
	return value;
}

float3 randomDirection(float3 normal, inout uint seed)
{
	float theta = random(seed) * 2.0 * 3.14159265358979323846;
	float phi = random(seed) * 2.0 * 3.14159265358979323846;
	float x = sin(theta) * cos(phi);
	float y = sin(theta) * sin(phi);
	float z = cos(theta);
	float3 dir = float3(x, y, z);
	float NdotD = dot(normal, dir);
	return dir * sign(NdotD);
}

float3 TracePath(Ray ray, inout uint seed)
{
	float3 light = 0;
	float3 color = 1;

	for(uint i = 0; i < _Bounces; i++)
	{
		HitInfo hit = TraceAllSpheres(ray);
		if(hit.hit)
		{
			ray.direction = randomDirection(hit.normal, seed);
			// return float3(seed & 0xFF, (seed >> 8) & 0xFF, (seed >> 16) & 0xFF) / 255.0; // Return seed value as color
			// return float3(random(seed), random(seed), random(seed)); // Return random values as color
			// return hit.normal;
			// return ray.direction;
			// ray.direction = reflect(ray.direction, hit.normal);
			ray.origin = hit.position + ray.direction * 0.1;

			Material mat = hit.material;
			light += mat.emission.rgb * color;
			color *= mat.color.rgb;
		}
		else
		{
			break;
		}
	}

	return light;
}

int _Samples;

float3 Trace(Ray ray, inout uint seed)
{
	uint rayCount = _Samples;
	float3 color = 0;
	for (uint j = 0; j < rayCount; j++)
	{
		color += TracePath(ray, seed);
	}
	color /= rayCount;
	return color;
}