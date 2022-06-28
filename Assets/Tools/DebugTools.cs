using System;
using UnityEngine;

public static class DebugTools
{
	public static void LogWarning(params object[] objs)
	{
#if UNITY_EDITOR || DEVELOPMENT_BUILD
		objs.LogWarning();
#endif
	}

	public static void DrawCircle(Vector3 point, Vector3 normal, float radius)
	{
#if UNITY_EDITOR || DEVELOPMENT_BUILD
		DrawCircle(point, normal, radius, Color.white, 0);
#endif
	}

	public static void DrawCircle(Vector3 point, Vector3 normal, float radius, Color color)
	{
#if UNITY_EDITOR || DEVELOPMENT_BUILD
		DrawCircle(point, normal, radius, color, 0);
#endif
	}

	public static void DrawCircle(Vector3 point, Vector3 normal, float radius, Color color, float duration = 0f)
	{
#if UNITY_EDITOR || DEVELOPMENT_BUILD
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
#endif
	}

	public static void DrawNormal(Vector3 point, Vector3 normal, Color color, float duration = 0f)
	{
#if UNITY_EDITOR || DEVELOPMENT_BUILD
		Debug.DrawRay(point, normal * 0.5f, color, duration);
		DrawCircle(point, normal, 0.1f, color, duration);
#endif
	}
}