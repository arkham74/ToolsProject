using System;
using System.Collections.Generic;
using System.Linq;
using Freya;
using UnityEngine;

public static class Vector3Extensions
{
	public static float Distance(this Vector3 v1, Vector3 v2)
	{
		return Vector3.Distance(v1, v2);
	}

	public static Vector3 PosterizeCeil(this Vector3 v, float steps)
	{
		return (v * steps).Ceil() / steps;
	}

	public static Vector3 PosterizeFloor(this Vector3 v, float steps)
	{
		return (v * steps).Floor() / steps;
	}

	public static Vector3 PosterizeRound(this Vector3 v, float steps)
	{
		return (v * steps).Round() / steps;
	}

	public static Vector3 SetX(this Vector3 vector, float value)
	{
		return new Vector3(value, vector.y, vector.z);
	}

	public static Vector3 SetXY(this Vector3 vector, float x, float y)
	{
		return new Vector3(x, y, vector.z);
	}

	public static Vector3 SetXY(this Vector3 vector, Vector2 xy)
	{
		return new Vector3(xy.x, xy.y, vector.z);
	}

	public static Vector3 SetXZ(this Vector3 vector, float x, float z)
	{
		return new Vector3(x, vector.y, z);
	}

	public static Vector3 SetY(this Vector3 vector, float value)
	{
		return new Vector3(vector.x, value, vector.z);
	}

	public static Vector3 SetZ(this Vector3 vector, float value)
	{
		return new Vector3(vector.x, vector.y, value);
	}

	public static Vector3Int SetX(this Vector3Int vector, int value)
	{
		return new Vector3Int(value, vector.y, vector.z);
	}

	public static Vector3Int SetY(this Vector3Int vector, int value)
	{
		return new Vector3Int(vector.x, value, vector.z);
	}

	public static Vector3Int SetZ(this Vector3Int vector, int value)
	{
		return new Vector3Int(vector.x, vector.y, value);
	}

	public static Vector2 XZtoXY(this Vector3 v)
	{
		return new Vector2(v.x, v.z);
	}

	public static Vector3 ClampEuler(this Vector3 eulerAngles)
	{
		eulerAngles.x = eulerAngles.x.ClampEuler();
		eulerAngles.y = eulerAngles.y.ClampEuler();
		eulerAngles.z = eulerAngles.z.ClampEuler();
		return eulerAngles;
	}

	public static Vector3 ClampMagnitude(this Vector3 v, float max)
	{
		return Vector3.ClampMagnitude(v, max);
	}

	public static float PathLength(this IList<Vector3> waypoints)
	{
		float sum = 0;

		for (int i = 1; i < waypoints.Count; i++)
		{
			sum += Vector3.Distance(waypoints[i - 1], waypoints[i]);
		}

		return sum;
	}

	public static float PathLength(this Span<Vector3> waypoints)
	{
		float sum = 0;

		for (int i = 1; i < waypoints.Length; i++)
		{
			sum += Vector3.Distance(waypoints[i - 1], waypoints[i]);
		}

		return sum;
	}

	public static float InverseLerp(this Vector3 v, Vector3 a, Vector3 b)
	{
		return Mathfs.InverseLerp(a, b, v);
	}
}