using System.Collections.Generic;
using UnityEngine;

public static class Vector4Extensions
{
	public static Vector4 OneMinus(this Vector4 v)
	{
		return Vector4.one - v;
	}

	public static Vector4 Remap(this Vector4 value, float from1 = -1, float to1 = 1, float from2 = 0, float to2 = 1)
	{
		value.x = (value.x - from1) / (to1 - from1) * (to2 - from2) + from2;
		value.y = (value.y - from1) / (to1 - from1) * (to2 - from2) + from2;
		value.z = (value.z - from1) / (to1 - from1) * (to2 - from2) + from2;
		value.w = (value.w - from1) / (to1 - from1) * (to2 - from2) + from2;
		return value;
	}

	public static Vector4 Ceil(this Vector4 v)
	{
		return new Vector4(Mathf.Ceil(v.x), Mathf.Ceil(v.y), Mathf.Ceil(v.z), Mathf.Ceil(v.w));
	}

	public static Vector4 Floor(this Vector4 v)
	{
		return new Vector4(Mathf.Floor(v.x), Mathf.Floor(v.y), Mathf.Floor(v.z), Mathf.Floor(v.w));
	}

	public static Vector4 InvSqrt(this Vector4 v)
	{
		return new Vector4(v.x.InvSqrt(), v.y.InvSqrt(), v.z.InvSqrt(), v.w.InvSqrt());
	}

	public static Vector4 Round(this Vector4 v)
	{
		return new Vector4(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z), Mathf.Round(v.w));
	}

	public static Vector4 SetW(this Vector4 vector, float value)
	{
		return new Vector4(vector.x, vector.y, vector.z, value);
	}

	public static Vector4 SetX(this Vector4 vector, float value)
	{
		return new Vector4(value, vector.y, vector.z, vector.w);
	}

	public static Vector4 SetY(this Vector4 vector, float value)
	{
		return new Vector4(vector.x, value, vector.z, vector.w);
	}

	public static Vector4 SetZ(this Vector4 vector, float value)
	{
		return new Vector4(vector.x, vector.y, value, vector.w);
	}

	public static float PathLength(this IList<Vector4> waypoints)
	{
		float sum = 0;

		for (int i = 1; i < waypoints.Count; i++)
		{
			sum += Vector4.Distance(waypoints[i - 1], waypoints[i]);
		}

		return sum;
	}
}