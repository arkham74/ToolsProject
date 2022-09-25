using Freya;
using UnityEngine;

namespace JD
{
	public static class Hex
	{
		private const float SQRT3 = 1.732050807568877293527446341505872366942805253810380628055806f;

		public static Vector3 WorldToHex(Vector2 point, float radius)
		{
			return WorldToHex(point, Vector2.one * radius);
		}

		public static Vector2 HexToWorld(Vector3 hex, float radius)
		{
			return HexToWorld(hex, Vector2.one * radius);
		}

		public static Vector3 WorldToHex(Vector2 point, Vector2 radius)
		{
			float q = SQRT3 / 3f * (point.x / radius.x) - 1f / 3f * (point.y / radius.y);
			float r = 2f / 3f * (point.y / radius.y);
			return new Vector3(q, r, -q - r);
		}

		public static Vector2 HexToWorld(Vector3 hex, Vector2 radius)
		{
			float x = radius.x * (SQRT3 * hex.x + SQRT3 / 2f * hex.y);
			float y = radius.y * (3f / 2f * hex.y);
			return new Vector2(x, y);
		}

		public static Vector3Int RoundToInt(Vector3 hex)
		{
			return Round(hex).RoundToInt();
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
