using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace JD
{
	public static partial class DebugTools
	{
#if UNITY_EDITOR
		public static bool TestMode
		{
			get => EditorPrefs.GetBool("test", false);
			set => EditorPrefs.SetBool("test", value);
		}
#else
		public const bool TestMode = false;
#endif

		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void LogWarning(params object[] array)
		{
			array.LogWarning();
		}

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

		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void DrawWireCube(Vector3 center, Vector3 size, Color color, float duration = 0f)
		{
			Bounds bounds = new Bounds(center, size);
			Vector3[] corners = bounds.GetCorners();
			Debug.DrawLine(corners[0], corners[1], color, duration);
			Debug.DrawLine(corners[1], corners[2], color, duration);
			Debug.DrawLine(corners[2], corners[3], color, duration);
			Debug.DrawLine(corners[3], corners[0], color, duration);
			Debug.DrawLine(corners[4], corners[5], color, duration);
			Debug.DrawLine(corners[5], corners[6], color, duration);
			Debug.DrawLine(corners[6], corners[7], color, duration);
			Debug.DrawLine(corners[7], corners[4], color, duration);
			Debug.DrawLine(corners[4], corners[4], color, duration);
			Debug.DrawLine(corners[5], corners[5], color, duration);
			Debug.DrawLine(corners[6], corners[6], color, duration);
			Debug.DrawLine(corners[7], corners[7], color, duration);
		}
	}
}