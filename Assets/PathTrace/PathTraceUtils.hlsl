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
	hitInfo.distance = 1.#INF;

	float3 oc = ray.origin - sphere.center;
	float b = dot( oc, ray.direction );
	float c = dot( oc, oc ) - sphere.radius * sphere.radius;
	float h = b * b - c;

	if(h >= 0)
	{
		hitInfo.distance = -b -sign(c) * sqrt(h);
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
	float s = seed * 12.9898;
	seed = s;
	return frac(sin(s) * 43758.5453123);
}

float3 randomDirection(float3 normal, inout uint seed)
{
		float theta = random(seed) * 2.0 * 3.14159265358979323846;
		float phi = random(seed) * 2.0 * 3.14159265358979323846;
		float x = sin(theta) * cos(phi);
		float y = sin(theta) * sin(phi);
		float z = cos(theta);
		return float3(x, y, z);
}

float3 Trace(Ray ray, inout uint seed)
{
	float3 light = 0;
	float3 color = 1;

	for(uint i = 0; i < _Bounces; i++)
	{
		HitInfo hit = TraceAllSpheres(ray);
		if(hit.hit)
		{
			Material mat = hit.material;
			light += mat.emission.rgb * color;
			color *= mat.color.rgb;

			ray.origin = hit.position;
			ray.direction = randomDirection(hit.normal, seed);
			// ray.direction = reflect(ray.direction, hit.normal);
		}
		else
		{
			break;
		}
	}

	return light;
}