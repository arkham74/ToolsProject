using UnityEngine;

namespace JD
{
	public static class Hex
	{
		private const float SQRT3 = 1.732050807568877293527446341505872366942805253810380628055806f;

		public static Vector3 WorldToHex(Vector2 point, float size = 1f)
		{
			float q = (SQRT3 / 3f * point.x - 1f / 3f * point.y) / size;
			float r = 2f / 3f * point.y / size;
			return new Vector3(q, r, -q - r);
		}

		public static Vector2 HexToWorld(Vector3 hex, float size = 1f)
		{
			float x = size * (SQRT3 * hex.x + SQRT3 / 2f * hex.y);
			float y = size * (3f / 2f * hex.y);
			return new Vector2(x, y);
		}

		public static Vector3 Round(Vector3 hex)
		{
			float q = Mathf.Round(hex.x);
			float r = Mathf.Round(hex.y);
			float s = Mathf.Round(hex.z);

			float qDiff = Mathf.Abs(q - hex.x);
			float rDiff = Mathf.Abs(r - hex.y);
			float sDiff = Mathf.Abs(s - hex.z);

			if (qDiff > rDiff && qDiff > sDiff)
			{
				q = -r - s;
			}
			else if (rDiff > sDiff)
			{
				r = -q - s;
			}
			else
			{
				s = -q - r;
			}

			return new Vector3(q, r, s);
		}
	}
}
