using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Freya;
using UnityEngine;

namespace JD
{
	public static class Vector2Extensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 XYtoYX(this Vector2 v)
		{
			return new Vector2(v.y, v.x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 InvertX(this Vector2 v)
		{
			return v.SetX(-v.x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 InvertY(this Vector2 v)
		{
			return v.SetY(-v.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int SetX(this Vector2Int vector, int value)
		{
			return new Vector2Int(value, vector.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int SetY(this Vector2Int vector, int value)
		{
			return new Vector2Int(vector.x, value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 PosterizeCeil(this Vector2 v, float steps)
		{
			return (v * steps).Ceil() / steps;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 PosterizeFloor(this Vector2 v, float steps)
		{
			return (v * steps).Floor() / steps;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 PosterizeRound(this Vector2 v, float steps)
		{
			return (v * steps).Round() / steps;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 SetX(this Vector2 vector, float value)
		{
			return new Vector2(value, vector.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 SetY(this Vector2 vector, float value)
		{
			return new Vector2(vector.x, value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 XYtoXZ(this Vector2 v, float y = 0)
		{
			return new Vector3(v.x, y, v.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 ClampMagnitude(this Vector2 vec2, float mag)
		{
			return Vector2.ClampMagnitude(vec2, mag);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(this Vector2 v1, Vector2 v2)
		{
			return Vector2.Distance(v1, v2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float PathLength(this IList<Vector2> waypoints)
		{
			float sum = 0;

			for (int i = 1; i < waypoints.Count; i++)
			{
				sum += Vector2.Distance(waypoints[i - 1], waypoints[i]);
			}

			return sum;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float PathLength(this Span<Vector2> waypoints)
		{
			float sum = 0;

			for (int i = 1; i < waypoints.Length; i++)
			{
				sum += Vector2.Distance(waypoints[i - 1], waypoints[i]);
			}

			return sum;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Random(this Vector2 v)
		{
			return UnityEngine.Random.Range(v.x, v.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Random(this Vector2Int v)
		{
			return UnityEngine.Random.Range(v.x, v.y + 1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float InverseLerp(this Vector2 v, Vector2 a, Vector2 b)
		{
			return VectorMath.InverseLerp(a, b, v);
		}

		/// <summary>
		/// Converts euler angles vector from -360..360 range to -180..180 range
		/// Same as NormalizeAngle
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 WrapAngle(this Vector2 angle)
		{
			angle.x = angle.x.WrapAngle();
			angle.y = angle.y.WrapAngle();
			return angle;
		}

		/// <summary>
		/// Converts euler angles vector from -360..360 range to -180..180 range
		/// Same as WrapAngle
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 NormalizeAngle(this Vector2 angle)
		{
			angle.x = angle.x.WrapAngle();
			angle.y = angle.y.WrapAngle();
			return angle;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Min(this Vector2Int size)
		{
			return Mathf.Min(size.x, size.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Remap(this Vector2 v, float iMin, float iMax, float oMin, float oMax)
		{
			return Mathfs.Remap(iMin * Vector2.one, iMax * Vector2.one, oMin * Vector2.one, oMax * Vector2.one, v);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Cross(this Vector2 a, Vector2 b)
		{
			return a.x * b.y - a.y * b.x;
		}
	}
}