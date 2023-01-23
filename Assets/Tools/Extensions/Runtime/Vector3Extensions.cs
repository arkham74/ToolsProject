using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Freya;
using UnityEngine;

namespace JD
{
	public static class Vector3Extensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(this Vector3 v1, Vector3 v2)
		{
			return Vector3.Distance(v1, v2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 PosterizeCeil(this Vector3 v, float steps)
		{
			return (v * steps).Ceil() / steps;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 PosterizeFloor(this Vector3 v, float steps)
		{
			return (v * steps).Floor() / steps;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 PosterizeRound(this Vector3 v, float steps)
		{
			return (v * steps).Round() / steps;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 SetX(this Vector3 vector, float value)
		{
			return new Vector3(value, vector.y, vector.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 SetXY(this Vector3 vector, float x, float y)
		{
			return new Vector3(x, y, vector.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 SetXY(this Vector3 vector, Vector2 xy)
		{
			return new Vector3(xy.x, xy.y, vector.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 SetXZ(this Vector3 vector, float x, float z)
		{
			return new Vector3(x, vector.y, z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 SetY(this Vector3 vector, float value)
		{
			return new Vector3(vector.x, value, vector.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 SetZ(this Vector3 vector, float value)
		{
			return new Vector3(vector.x, vector.y, value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int SetX(this Vector3Int vector, int value)
		{
			return new Vector3Int(value, vector.y, vector.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int SetY(this Vector3Int vector, int value)
		{
			return new Vector3Int(vector.x, value, vector.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int SetZ(this Vector3Int vector, int value)
		{
			return new Vector3Int(vector.x, vector.y, value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 XZtoXY(this Vector3 v)
		{
			return new Vector2(v.x, v.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ClampAngle(this Vector3 lfAngle, Vector3 lfMin, Vector3 lfMax)
		{
			lfAngle.x = lfAngle.x.ClampAngle(lfMin.x, lfMax.x);
			lfAngle.y = lfAngle.y.ClampAngle(lfMin.y, lfMax.y);
			lfAngle.z = lfAngle.z.ClampAngle(lfMin.z, lfMax.z);
			return lfAngle;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ClampMagnitude(this Vector3 v, float max)
		{
			return Vector3.ClampMagnitude(v, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float PathLength(this IList<Vector3> waypoints)
		{
			float sum = 0;

			for (int i = 1; i < waypoints.Count; i++)
			{
				sum += Vector3.Distance(waypoints[i - 1], waypoints[i]);
			}

			return sum;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float PathLength(this Span<Vector3> waypoints)
		{
			float sum = 0;

			for (int i = 1; i < waypoints.Length; i++)
			{
				sum += Vector3.Distance(waypoints[i - 1], waypoints[i]);
			}

			return sum;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Approx(this Vector3 a, Vector3 b, float threshold = 0.01f)
		{
			bool xt = Mathf.Abs(a.x - b.x) < threshold;
			bool yt = Mathf.Abs(a.y - b.y) < threshold;
			bool zt = Mathf.Abs(a.z - b.z) < threshold;
			return xt && yt && zt;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float InverseLerp(this Vector3 v, Vector3 a, Vector3 b)
		{
			return VectorMath.InverseLerp(a, b, v);
		}

		/// <summary>
		/// Converts euler angles vector from -360..360 range to -180..180 range
		/// Same as NormalizeAngle
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 WrapAngle(this Vector3 angle)
		{
			angle.x = angle.x.WrapAngle();
			angle.y = angle.y.WrapAngle();
			angle.z = angle.z.WrapAngle();
			return angle;
		}

		/// <summary>
		/// Converts euler angles vector from -360..360 range to -180..180 range
		/// Same as WrapAngle
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 NormalizeAngle(this Vector3 angle)
		{
			angle.x = angle.x.WrapAngle();
			angle.y = angle.y.WrapAngle();
			angle.z = angle.z.WrapAngle();
			return angle;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Remap(this Vector3 v, float iMin, float iMax, float oMin, float oMax)
		{
			return Mathfs.Remap(iMin * Vector3.one, iMax * Vector3.one, oMin * Vector3.one, oMax * Vector3.one, v);
		}

		// Summary:
		//     Projects a vector onto a plane defined by a normal orthogonal to the plane.
		//
		// Parameters:
		//   planeNormal:
		//     The direction from the vector towards the plane.
		//
		//   vector:
		//     The location of the vector above the plane.
		//
		// Returns:
		//     The location of the vector on the plane.
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ProjectOnPlane(this Vector3 vector, Vector3 planeNormal)
		{
			return Vector3.ProjectOnPlane(vector, planeNormal);
		}
	}
}