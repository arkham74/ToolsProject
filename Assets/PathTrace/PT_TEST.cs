using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PT_TEST : MonoBehaviour
{
	[SerializeField] private int count = 100;
	[SerializeField] private uint seed = 2137;
	[SerializeField] private Quaternion normal;

	private void OnDrawGizmos()
	{
		Gizmos.matrix = transform.localToWorldMatrix;
		uint state = seed;
		for (int i = 0; i < count; i++)
		{
			Gizmos.DrawWireSphere(randomDirection(normal * Vector3.up, ref state), 0.01f);
		}
	}

	float random(ref uint state)
	{
		state = state * 747796405 + 2891336453;
		uint v = ((state >> 28) + 4);
		uint word = ((state >> (int)v) ^ state) * 277803737u;
		return (word >> 22) ^ word;
	}

	float3 randomDirection(float3 normal, ref uint state)
	{
		float theta = random(ref state) * 2.0f * 3.14159265358979323846f;
		float phi = random(ref state) * 2.0f * 3.14159265358979323846f;
		float x = math.sin(theta) * math.cos(phi);
		float y = math.sin(theta) * math.sin(phi);
		float z = math.cos(theta);
		float3 dir = new float3(x, y, z);
		float NdotD = math.dot(normal, dir);
		if (NdotD < 0)
		{
			dir = -dir;
		}
		return dir;
	}
}
