using System;
using UnityEngine;
using UnityEngine.Events;

namespace JD.Draw
{
	public static class Draw
	{
		public static readonly UnityEvent OnUpdate = new UnityEvent();

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void Init()
		{
			OnUpdate.RemoveAllListeners();
		}

		public static void Circle(Vector3 center, Quaternion rotation, Color color, float radius = 1, float width = 0.1f, int segments = 64)
		{
			Circle(new Circle(center, rotation, color, radius, width, segments));
		}

		public static void Line(Vector3 start, Vector3 end, Color color, float width = 0.1f)
		{
			Line(new Line(start, end, color, width));
		}

		public static void Line(Line line)
		{
			DrawPass.AddLine(line);
		}

		public static void Circle(Circle circle)
		{
			for (int i = 0; i < circle.segments; i++)
			{
				float t1 = (i + 0f) / circle.segments;
				float t2 = (i + 1f) / circle.segments;

				float x1 = Mathf.Sin(t1 * Mathf.PI * 2f) * circle.radius;
				float y1 = Mathf.Cos(t1 * Mathf.PI * 2f) * circle.radius;

				float x2 = Mathf.Sin(t2 * Mathf.PI * 2f) * circle.radius;
				float y2 = Mathf.Cos(t2 * Mathf.PI * 2f) * circle.radius;

				Vector3 v1 = circle.rotation * new Vector3(x1, 0, y1) + circle.center;
				Vector3 v2 = circle.rotation * new Vector3(x2, 0, y2) + circle.center;

				Draw.Line(v1, v2, circle.color, circle.width);
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

			Draw.Line(a, b, color, radius);
			Draw.Line(c, d, color, radius);
			Draw.Line(e, f, color, radius);
			Draw.Line(g, h, color, radius);
			Draw.Line(a, c, color, radius);
			Draw.Line(b, d, color, radius);
			Draw.Line(e, g, color, radius);
			Draw.Line(f, h, color, radius);
			Draw.Line(a, e, color, radius);
			Draw.Line(b, f, color, radius);
			Draw.Line(d, h, color, radius);
			Draw.Line(c, g, color, radius);
		}
	}
}