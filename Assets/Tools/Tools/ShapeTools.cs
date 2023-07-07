using UnityEngine;

#if TOOLS_LOCALIZATION
using UnityEngine.Localization.Settings;
#endif

namespace JD
{
	public static class ShapeTools
	{
		public static Vector2 Hex(int cornerIndex, float radius, float angleOffset = 0f)
		{
			float rad = Mathf.Deg2Rad * (60f * cornerIndex + angleOffset);
			float x = Mathf.Cos(rad) * radius;
			float y = Mathf.Sin(rad) * radius;
			return new Vector2(x, y);
		}

		public static Vector2 Circle(float t, float radius)
		{
			float rad = t * Mathf.PI * 2;
			float x = Mathf.Sin(rad) * radius;
			float y = Mathf.Cos(rad) * radius;
			return new Vector2(x, y);
		}
	}
}
