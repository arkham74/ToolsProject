using System;
using System.Collections.Generic;
using System.Linq;
using Freya;
using UnityEngine;

namespace JD
{
	public static class Vector2Extensions
	{
		public static Vector2 XYtoYX(this Vector2 v)
		{
			return new Vector2(v.y, v.x);
		}

		public static Vector2 InvertX(this Vector2 v)
		{
			return v.SetX(-v.x);
		}

		public static Vector2 InvertY(this Vector2 v)
		{
			return v.SetY(-v.y);
		}

		public static Vector2Int SetX(this Vector2Int vector, int value)
		{
			return new Vector2Int(value, vector.y);
		}

		public static Vector2Int SetY(this Vector2Int vector, int value)
		{
			return new Vector2Int(vector.x, value);
		}

		public static Vector2 PosterizeCeil(this Vector2 v, float steps)
		{
			return (v * steps).Ceil() / steps;
		}

		public static Vector2 PosterizeFloor(this Vector2 v, float steps)
		{
			return (v * steps).Floor() / steps;
		}

		public static Vector2 PosterizeRound(this Vector2 v, float steps)
		{
			return (v * steps).Round() / steps;
		}

		public static Vector2 SetX(this Vector2 vector, float value)
		{
			return new Vector2(value, vector.y);
		}

		public static Vector2 SetY(this Vector2 vector, float value)
		{
			return new Vector2(vector.x, value);
		}

		public static Vector3 XYtoXZ(this Vector2 v, float y = 0)
		{
			return new Vector3(v.x, y, v.y);
		}

		public static Vector2 ClampMagnitude(this Vector2 vec2, float mag)
		{
			return Vector2.ClampMagnitude(vec2, mag);
		}

		public static float Distance(this Vector2 v1, Vector2 v2)
		{
			return Vector2.Distance(v1, v2);
		}

		public static float PathLength(this IList<Vector2> waypoints)
		{
			float sum = 0;

			for (int i = 1; i < waypoints.Count; i++)
			{
				sum += Vector2.Distance(waypoints[i - 1], waypoints[i]);
			}

			return sum;
		}

		public static float PathLength(this Span<Vector2> waypoints)
		{
			float sum = 0;

			for (int i = 1; i < waypoints.Length; i++)
			{
				sum += Vector2.Distance(waypoints[i - 1], waypoints[i]);
			}

			return sum;
		}

		public static float Random(this Vector2 v)
		{
			return UnityEngine.Random.Range(v.x, v.y);
		}

		public static int Random(this Vector2Int v)
		{
			return UnityEngine.Random.Range(v.x, v.y + 1);
		}

		public static float InverseLerp(this Vector2 v, Vector2 a, Vector2 b)
		{
			return VectorMath.InverseLerp(a, b, v);
		}

		/// <summary>
		/// Converts euler angles vector from -360..360 range to -180..180 range
		/// Same as NormalizeAngle
		/// </summary>
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
		public static Vector2 NormalizeAngle(this Vector2 angle)
		{
			angle.x = angle.x.WrapAngle();
			angle.y = angle.y.WrapAngle();
			return angle;
		}
	}
}