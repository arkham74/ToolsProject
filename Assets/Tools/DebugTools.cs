using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class DebugTools
{
	[Conditional("UNITY_EDITOR")]
	[Conditional("DEVELOPMENT_BUILD")]
	public static void LogWarning<T>(this IList<T> array, string separator = ", ")
	{
		if (array == null)
		{
			Debug.LogWarning("Array is NULL");
			return;
		}

		if (array.Count <= 0)
		{
			Debug.LogWarning("Array is empty");
			return;
		}

		Debug.LogWarning(string.Join(separator, array));
	}

	[Conditional("UNITY_EDITOR")]
	[Conditional("DEVELOPMENT_BUILD")]
	public static void DrawCircle(Vector3 point, Vector3 normal, float radius)
	{
		DrawCircle(point, normal, radius, Color.white, 0);
	}

	[Conditional("UNITY_EDITOR")]
	[Conditional("DEVELOPMENT_BUILD")]
	public static void DrawCircle(Vector3 point, Vector3 normal, float radius, Color color)
	{
		DrawCircle(point, normal, radius, color, 0);
	}

	[Conditional("UNITY_EDITOR")]
	[Conditional("DEVELOPMENT_BUILD")]
	public static void DrawCircle(Vector3 point, Vector3 normal, float radius, Color color, float duration = 0f)
	{
		const int steps = 16;

		Quaternion rot = Quaternion.LookRotation(normal);

		for (int i = 1; i <= steps; i++)
		{
			float angle1 = (i - 1f) / steps * Mathf.PI * 2;
			float angle2 = (i - 0f) / steps * Mathf.PI * 2;

			float startX = Mathf.Sin(angle1) * radius;
			float startY = Mathf.Cos(angle1) * radius;
			float endX = Mathf.Sin(angle2) * radius;
			float endY = Mathf.Cos(angle2) * radius;

			Vector3 start = rot * new Vector2(startX, startY);
			Vector3 end = rot * new Vector2(endX, endY);

			Debug.DrawLine(point + start, point + end, color, duration);
		}
	}

	[Conditional("UNITY_EDITOR")]
	[Conditional("DEVELOPMENT_BUILD")]
	public static void DrawNormal(Vector3 point, Vector3 normal, Color color, float duration = 0f)
	{
		Debug.DrawRay(point, normal * 0.5f, color, duration);
		DrawCircle(point, normal, 0.1f, color, duration);
	}
}