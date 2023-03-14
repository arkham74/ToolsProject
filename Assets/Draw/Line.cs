using UnityEngine;

namespace JD.Draw
{
	public struct Line
	{
		public Vector3 start;
		public Vector3 end;
		public Color color;
		public float width;

		public Line(Vector3 start, Vector3 end, Color color, float width = 0.1f)
		{
			this.start = start;
			this.end = end;
			this.color = color;
			this.width = width;
		}
	}
}