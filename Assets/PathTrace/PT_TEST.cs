using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PT_TEST : MonoBehaviour
{
	[SerializeField][Range(0, 1)] private float angle = 0f;
	[SerializeField] private int count = 100;
	[SerializeField] private int seed = 2137;
	[SerializeField] private Quaternion normal;

	private void OnDrawGizmos()
	{
		Gizmos.matrix = transform.localToWorldMatrix;
		int state = seed;
		for (int i = 0; i < count; i++)
		{
			Vector3 direction = RandomDirectionInCone(normal * Vector3.up, angle, ref state);
			Gizmos.DrawLine(Vector3.zero, direction);
		}
	}

	public static Vector3 RandomDirectionInCone(Vector3 coneDirection, float coneAngle, ref int seed)
	{
		// Generate a random vector direction within a unit hemisphere oriented around the z axis
		Random.InitState(seed);
		seed = seed * 2137 + 420;
		float angle = Random.Range(0f, coneAngle * Mathf.PI * 0.5f);
		float theta = Random.Range(0f, 2f * Mathf.PI);
		Vector3 direction = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, new Vector3(Mathf.Sin(theta), 0f, Mathf.Cos(theta))) * coneDirection;
		return direction;
	}
}
