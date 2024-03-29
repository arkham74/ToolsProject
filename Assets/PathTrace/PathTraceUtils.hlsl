struct Ray
{
	float3 origin;
	float3 direction;
};

struct Material
{
	float4 color;
	float4 emission;
	float smoothness;
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

float3 randomDirectionInCone(float3 coneDirection, float coneAngle, inout uint seed)
{
	float rand1 = random(seed); // use a random value based on some texture coordinates or other input
	float rand2 = random(seed); // use a second random value based on different input
	
	float cosTheta = 1.0 - rand1 * (1.0 - cos(coneAngle));
	float sinTheta = sqrt(1.0 - cosTheta * cosTheta);
	float phi = rand2 * 2.0 * PI;
	
	float3 basis1 = normalize(cross(float3(0.0, 0.0, 1.0), coneDirection));
	float3 basis2 = normalize(cross(basis1, coneDirection));
	
	float3 randomDirection = sinTheta * cos(phi) * basis1 + sinTheta * sin(phi) * basis2 + cosTheta * coneDirection;
	return normalize(randomDirection);
}

/* float3 randomDirectionInCone(float3 dir, float angle, inout uint seed)
{
	float3 u = normalize(cross(dir, float3(0, 1, 0)));
	float3 v = cross(u, dir);
	float r = sqrt(random(seed));
	float theta = random(seed) * 2.0 * 3.14159265358979323846;
	float x = r * cos(theta);
	float y = r * sin(theta);
	float z = sqrt(1.0 - random(seed));
	z = z * cos(angle);
	return normalize(dir + x * u + y * v + z * dir);
}
 */
float3 TracePath(Ray ray, inout uint seed)
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

			float3 diffuse = randomDirection(hit.normal, seed);
			float3 specular = reflect(ray.direction, hit.normal);
			ray.direction = lerp(diffuse, specular, mat.smoothness);
			ray.direction = randomDirectionInCone(specular, (1 - mat.smoothness) * PI, seed);
			// return ray.direction;
			ray.origin = hit.position + ray.direction * 0.01f;
		}
		else
		{
			color *= unity_AmbientSky;
			break;
		}
	}

	return light + color;
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