#if UNITY_URP
using UnityEngine;

namespace JD.ScreenDraw
{
	internal struct ScreenDrawLine
	{
		public Vector3 start;
		public Vector3 end;
		public Color color;
		public float width;

		public ScreenDrawLine(Vector3 start, Vector3 end, Color color, float width)
		{
			this.start = start;
			this.end = end;
			this.color = color;
			this.width = width;
		}
	}
}
#endif