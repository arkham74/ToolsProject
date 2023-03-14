using UnityEngine;

namespace JD.Draw
{
	public struct Circle
	{
		public Vector3 center;
		public Quaternion rotation;
		public Color color;
		public float radius;
		public float width;
		public int segments;

		public Circle(Vector3 center, Quaternion rotation, Color color, float radius = 1, float width = 0.1f, int segments = 64)
		{
			this.center = center;
			this.rotation = rotation;
			this.radius = radius;
			this.color = color;
			this.width = width;
			this.segments = segments;
		}
	}
}