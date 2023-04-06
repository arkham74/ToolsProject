using System;
using UnityEngine;
using UnityEngine.Events;

namespace JD.ScreenDraw
{
	public static class ScreenDraw
	{
		public static readonly UnityEvent OnUpdate = new UnityEvent();

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		internal static void Init()
		{
			OnUpdate.RemoveAllListeners();
		}

		public static void Line(Vector3 start, Vector3 end, Color color, float width = 0.1f)
		{
			ScreenDrawPass.AddLine(start, end, color, width);
		}

		public static void Circle(Vector3 center, Quaternion rotation, Color color, float radius = 1, float width = 0.1f, int segments = 64)
		{
			for (int i = 0; i < segments; i++)
			{
				float t1 = (i + 0f) / segments;
				float t2 = (i + 1f) / segments;

				float x1 = Mathf.Sin(t1 * Mathf.PI * 2f) * radius;
				float y1 = Mathf.Cos(t1 * Mathf.PI * 2f) * radius;

				float x2 = Mathf.Sin(t2 * Mathf.PI * 2f) * radius;
				float y2 = Mathf.Cos(t2 * Mathf.PI * 2f) * radius;

				Vector3 v1 = rotation * new Vector3(x1, 0, y1) + center;
				Vector3 v2 = rotation * new Vector3(x2, 0, y2) + center;

				ScreenDraw.Line(v1, v2, color, width);
			}
		}

		public static void Box(Vector3 center, Vector3 extents, Quaternion rotation, Color color, float radius)
		{
			extents = extents / 2f;
			var a = rotation * new Vector3(+1 * extents.x, -1 * extents.y, +1 * extents.z) + center;
			var b = rotation * new Vector3(+1 * extents.x, +1 * extents.y, +1 * extents.z) + center;
			var c = rotation * new Vector3(-1 * extents.x, -1 * extents.y, +1 * extents.z) + center;
			var d = rotation * new Vector3(-1 * extents.x, +1 * extents.y, +1 * extents.z) + center;
			var e = rotation * new Vector3(+1 * extents.x, -1 * extents.y, -1 * extents.z) + center;
			var f = rotation * new Vector3(+1 * extents.x, +1 * extents.y, -1 * extents.z) + center;
			var g = rotation * new Vector3(-1 * extents.x, -1 * extents.y, -1 * extents.z) + center;
			var h = rotation * new Vector3(-1 * extents.x, +1 * extents.y, -1 * extents.z) + center;

			ScreenDraw.Line(a, b, color, radius);
			ScreenDraw.Line(c, d, color, radius);
			ScreenDraw.Line(e, f, color, radius);
			ScreenDraw.Line(g, h, color, radius);
			ScreenDraw.Line(a, c, color, radius);
			ScreenDraw.Line(b, d, color, radius);
			ScreenDraw.Line(e, g, color, radius);
			ScreenDraw.Line(f, h, color, radius);
			ScreenDraw.Line(a, e, color, radius);
			ScreenDraw.Line(b, f, color, radius);
			ScreenDraw.Line(d, h, color, radius);
			ScreenDraw.Line(c, g, color, radius);
		}
	}
}