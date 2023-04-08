using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Freya;
using UnityEngine;

namespace JD
{
	public static class Vector4Extensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 OneMinus(this Vector4 v)
		{
			return Vector4.one - v;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Ceil(this Vector4 v)
		{
			return new Vector4(Mathf.Ceil(v.x), Mathf.Ceil(v.y), Mathf.Ceil(v.z), Mathf.Ceil(v.w));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Floor(this Vector4 v)
		{
			return new Vector4(Mathf.Floor(v.x), Mathf.Floor(v.y), Mathf.Floor(v.z), Mathf.Floor(v.w));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 InvSqrt(this Vector4 v)
		{
			return new Vector4(v.x.InvSqrt(), v.y.InvSqrt(), v.z.InvSqrt(), v.w.InvSqrt());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Round(this Vector4 v)
		{
			return new Vector4(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z), Mathf.Round(v.w));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 SetW(this Vector4 vector, float value)
		{
			return new Vector4(vector.x, vector.y, vector.z, value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 SetX(this Vector4 vector, float value)
		{
			return new Vector4(value, vector.y, vector.z, vector.w);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 SetY(this Vector4 vector, float value)
		{
			return new Vector4(vector.x, value, vector.z, vector.w);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 SetZ(this Vector4 vector, float value)
		{
			return new Vector4(vector.x, vector.y, value, vector.w);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float PathLength(this IList<Vector4> waypoints)
		{
			float sum = 0;

			for (int i = 1; i < waypoints.Count; i++)
			{
				sum += Vector4.Distance(waypoints[i - 1], waypoints[i]);
			}

			return sum;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float InverseLerp(this Vector4 v, Vector4 a, Vector4 b)
		{
			return VectorMath.InverseLerp(a, b, v);
		}

		/// <summary>
		/// Converts euler angles vector from -360..360 range to -180..180 range
		/// Same as NormalizeAngle
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 WrapAngle(this Vector4 angle)
		{
			angle.x = angle.x.WrapAngle();
			angle.y = angle.y.WrapAngle();
			angle.z = angle.z.WrapAngle();
			angle.w = angle.w.WrapAngle();
			return angle;
		}

		/// <summary>
		/// Converts euler angles vector from -360..360 range to -180..180 range
		/// Same as WrapAngle
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 NormalizeAngle(this Vector4 angle)
		{
			angle.x = angle.x.WrapAngle();
			angle.y = angle.y.WrapAngle();
			angle.z = angle.z.WrapAngle();
			angle.w = angle.w.WrapAngle();
			return angle;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Remap(this Vector4 v, float iMin, float iMax, float oMin, float oMax)
		{
			return Mathfs.Remap(iMin * Vector4.one, iMax * Vector4.one, oMin * Vector4.one, oMax * Vector4.one, v);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Approx(this Vector4 a, Vector4 b)
		{
			return Mathfs.Approximately(a.x, b.x) && Mathfs.Approximately(a.y, b.y) && Mathfs.Approximately(a.z, b.z) && Mathfs.Approximately(a.w, b.w);
		}
	}
}