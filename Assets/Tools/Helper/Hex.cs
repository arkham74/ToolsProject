using UnityEngine;

namespace SAR
{
	public readonly struct Hex
	{
		private const float SQRT3 = 1.732050807568877293527446341505872366942805253810380628055806f;

		public readonly float Q, R, S;

		private Hex(float q, float r, float s)
		{
			Q = q;
			R = r;
			S = s;
		}

		private Hex(float q, float r)
		{
			Q = q;
			R = r;
			S = -q - r;
		}

		public static Hex WorldToHex(Vector2 point, float size = 1f)
		{
			float q = (SQRT3 / 3f * point.x - 1f / 3f * point.y) / size;
			float r = (2f / 3f * point.y) / size;
			return new Hex(q, r);
		}

		public static Vector2 HexToWorld(Hex hex, float size = 1f)
		{
			float x = size * (SQRT3 * hex.Q + SQRT3 / 2f * hex.R);
			float y = size * (3f / 2f * hex.R);
			return new Vector2(x, y);
		}

		public static Hex Round(Hex hex)
		{
			float q = Mathf.Round(hex.Q);
			float r = Mathf.Round(hex.R);
			float s = Mathf.Round(hex.S);

			float qDiff = Mathf.Abs(q - hex.Q);
			float rDiff = Mathf.Abs(r - hex.R);
			float sDiff = Mathf.Abs(s - hex.S);

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

			return new Hex(q, r, s);
		}
	}
}