using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace JD
{
	public static class HexUtils
	{
		private const float SQRT3 = 1.732050807568877293527446341505872366942805253810380628055806f;
		private const float SQRT3D2 = SQRT3 / 2f;
		private const float SQRT3D3 = SQRT3 / 3f;
		private const float C1D3 = 1f / 3f;
		private const float C2D3 = 2f / 3f;
		private const float C3D2 = 3f / 2f;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex FromAxial(this Vector2 qr) => FromAxial(qr.x, qr.y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex FromOffset(this Vector2Int xy) => FromOffset(xy.x, xy.y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex FromAxial(float q, float r) => new Hex(q, r);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 ToAxial(this Hex hex) => new Vector2(hex.Q, hex.R);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ToCube(this Hex hex) => new Vector3(hex.Q, hex.R, hex.S);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 ToWorld(this Hex hex) => ToWorld(hex, Vector3.one);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex FromWorld(this Vector3 point) => FromWorld(point, Vector3.one);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 ToWorld(this Hex hex, Vector3 radius)
		{
			float x = radius.x * SQRT3 * hex.Q + SQRT3D2 * hex.R;
			float y = radius.y * C3D2 * hex.R;
			return new Vector2(x, y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex FromWorld(this Vector3 point, Vector3 radius)
		{
			float x = point.x / radius.x;
			float y = point.y / radius.y;
			float q = SQRT3D3 * x - C1D3 * y;
			float r = C2D3 * y;
			return new Hex(q, r);
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
			float q = Mathf.Abs(vec.Q);
			float r = Mathf.Abs(vec.R);
			float s = Mathf.Abs(vec.S);
			return (q + r + s) / 2f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex Negate(this Hex hex)
		{
			return new Hex(-hex.Q, -hex.R);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex Sub(this Hex a, Hex b)
		{
			float q = a.Q - b.Q;
			float r = a.R - b.R;
			return new Hex(q, r);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex Add(this Hex a, Hex b)
		{
			float q = a.Q + b.Q;
			float r = a.R + b.R;
			return new Hex(q, r);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex Mul(this Hex a, float b)
		{
			float q = a.Q * b;
			float r = a.R * b;
			return new Hex(q, r);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex Div(this Hex a, float b)
		{
			float q = a.Q / b;
			float r = a.R / b;
			return new Hex(q, r);
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
			int q = (int)rounded.Q;
			int r = (int)rounded.R;
			int col = q + (r + (r & 1)) / 2;
			int row = r;
			return new Vector2Int(col, row);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex Round(this Hex hex)
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

			return new Hex(q, r);
		}
	}
}