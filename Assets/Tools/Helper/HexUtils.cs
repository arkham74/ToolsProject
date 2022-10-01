using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace JD
{
	public static class HexUtils
	{
		private const float SQRT3 = 1.732050807568877293527446341505872366942805253810380628055806f;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex FromWorld(this Vector3 point) => FromWorld((Vector2)point);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex FromAxial(this Vector2 qr) => FromAxial(qr.x, qr.y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex FromCube(this Vector3 qrs) => FromCube(qrs.x, qrs.y, qrs.z);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex FromOffset(this Vector2Int xy) => FromOffset(xy.x, xy.y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex FromAxial(float q, float r) => new Hex(q, r, -q - r);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex FromCube(float q, float r, float s) => new Hex(q, r, s);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 ToAxial(this Hex hex) => new Vector2(hex.q, hex.r);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ToCube(this Hex hex) => new Vector3(hex.q, hex.r, hex.s);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 ToWorld(this Hex hex)
		{
			float x = SQRT3 * hex.q + SQRT3 / 2f * hex.r;
			float y = 3f / 2f * hex.r;
			return new Vector2(x, y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex FromWorld(this Vector2 point)
		{
			float y = point.y;
			float x = point.x;
			float q = SQRT3 / 3f * x - 1f / 3f * y;
			float r = 2f / 3f * y;
			return new Hex(q, r, -q - r);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex[] Neighbours(this Hex hex, int ring = 1)
		{
			ring = Mathf.Max(ring, 1);
			return new Hex[6]
			{
				new Hex(1, 0) * ring + hex,
				new Hex(-1, 0) * ring +  hex,
				new Hex(0, 1) * ring + hex,
				new Hex(0, -1) * ring +  hex,
				new Hex(1, -1) * ring +  hex,
				new Hex(-1, 1) * ring +  hex,
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(this Hex a, Hex b)
		{
			Hex vec = Sub(a, b);
			float q = Mathf.Abs(vec.q);
			float r = Mathf.Abs(vec.r);
			float s = Mathf.Abs(vec.s);
			return (q + r + s) / 2f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex Negate(this Hex hex)
		{
			float q = -hex.q;
			float r = -hex.r;
			float s = -q - r;
			return new Hex(q, r, s);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex Sub(this Hex a, Hex b)
		{
			float q = a.q - b.q;
			float r = a.r - b.r;
			float s = a.s - b.s;
			return new Hex(q, r, s);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex Add(this Hex a, Hex b)
		{
			float q = a.q + b.q;
			float r = a.r + b.r;
			float s = a.s + b.s;
			return new Hex(q, r, s);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex Mul(this Hex a, float b)
		{
			float q = a.q * b;
			float r = a.r * b;
			float s = a.s * b;
			return new Hex(q, r, s);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex Div(this Hex a, float b)
		{
			float q = a.q / b;
			float r = a.r / b;
			float s = a.s / b;
			return new Hex(q, r, s);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex FromOffset(int x, int y)
		{
			int q = x - (y + (y & 1)) / 2;
			int r = y;
			return FromAxial(q, r);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int ToOffset(this Hex hex)
		{
			Hex rounded = hex.Round();
			int q = (int)rounded.q;
			int r = (int)rounded.r;
			int col = q + (r + (r & 1)) / 2;
			int row = r;
			return new Vector2Int(col, row);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex Round(this Hex hex)
		{
			float q = Mathf.Round(hex.q);
			float r = Mathf.Round(hex.r);
			float s = Mathf.Round(hex.s);

			float qDiff = Mathf.Abs(q - hex.q);
			float rDiff = Mathf.Abs(r - hex.r);
			float sDiff = Mathf.Abs(s - hex.s);

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