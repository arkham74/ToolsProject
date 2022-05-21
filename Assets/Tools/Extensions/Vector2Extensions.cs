using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Vector2Extensions
{
	// 	public static Vector3 SetZ(this Vector2 vector, float value)
	// 	{
	// 		return new Vector3(vector.x, vector.y, value);
	// 	}

	// 	/// <summary>
	// 	/// Normalized direction from two vector2s
	// 	/// </summary>
	// 	/// <param name="v"></param>
	// 	/// <param name="target"></param>
	// 	/// <returns></returns>
	// 	public static Vector2 DirTo(this Vector2 v, Vector2 target)
	// 	{
	// 		return (target - v).normalized;
	// 	}

	// 	// /// <summary>
	// 	// /// Non-normalized direction from two vectors2
	// 	// /// </summary>
	// 	// /// <param name="v"></param>
	// 	// /// <param name="target"></param>
	// 	// /// <returns></returns>
	// 	// public static Vector2 To(this Vector2 v, Vector2 target)
	// 	// {
	// 	// 	return target - v;
	// 	// }

	// 	public static Vector2 RoundToPlace(this Vector2 v, int places)
	// 	{
	// 		float mlt = Mathf.Pow(10, places);
	// 		return (v * mlt).Round() / mlt;
	// 	}

	// 	public static Vector2 CeilToPlace(this Vector2 v, int places)
	// 	{
	// 		float mlt = Mathf.Pow(10, places);
	// 		return (v * mlt).Ceil() / mlt;
	// 	}

	// 	public static Vector2 FloorToPlace(this Vector2 v, int places)
	// 	{
	// 		float mlt = Mathf.Pow(10, places);
	// 		return (v * mlt).Floor() / mlt;
	// 	}

	// 	public static Vector3 XYtoZY(this Vector2 v, float x = 0)
	// 	{
	// 		return new Vector3(x, v.y, v.x);
	// 	}

	public static Vector2 XYtoYX(this Vector2 v)
	{
		return new Vector2(v.y, v.x);
	}

	// 	public static Vector2 OneMinus(this Vector2 v)
	// 	{
	// 		return Vector2.one - v;
	// 	}

	public static Vector2 InvertX(this Vector2 v)
	{
		return v.SetX(-v.x);
	}

	public static Vector2 InvertY(this Vector2 v)
	{
		return v.SetY(-v.y);
	}

	// 	public static Vector2 Swap(this Vector2 vector)
	// 	{
	// 		return new Vector2(vector.y, vector.x);
	// 	}

	// 	public static Vector2Int Swap(this Vector2Int vector)
	// 	{
	// 		return new Vector2Int(vector.y, vector.x);
	// 	}

	public static Vector2Int SetX(this Vector2Int vector, int value)
	{
		return new Vector2Int(value, vector.y);
	}

	public static Vector2Int SetY(this Vector2Int vector, int value)
	{
		return new Vector2Int(vector.x, value);
	}

	// 	public static float Random(this Vector2 v)
	// 	{
	// 		return UnityEngine.Random.Range(v.x, v.y);
	// 	}

	// 	public static int Random(this Vector2Int v)
	// 	{
	// 		return UnityEngine.Random.Range(v.x, v.y);
	// 	}

	// 	// public static Vector2 Ceil(this Vector2 v)
	// 	// {
	// 	// 	return new Vector2(Mathf.Ceil(v.x), Mathf.Ceil(v.y));
	// 	// }

	// 	// public static Vector2 Round(this Vector2 v)
	// 	// {
	// 	// 	return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
	// 	// }

	// 	// public static Vector2 Floor(this Vector2 v)
	// 	// {
	// 	// 	return new Vector2(Mathf.Floor(v.x), Mathf.Floor(v.y));
	// 	// }

	// 	public static Vector2 InvSqrt(this Vector2 v)
	// 	{
	// 		return new Vector2(v.x.InvSqrt(), v.y.InvSqrt());
	// 	}

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

	// 	public static Vector2Int RoundToInt(this Vector2 v)
	// 	{
	// 		return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
	// 	}

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

	// 	public static Vector2 Remap(this Vector2 value, float from1 = -1, float to1 = 1, float from2 = 0, float to2 = 1)
	// 	{
	// 		value.x = (value.x - from1) / (to1 - from1) * (to2 - from2) + from2;
	// 		value.y = (value.y - from1) / (to1 - from1) * (to2 - from2) + from2;
	// 		return value;
	// 	}

	// 	public static Vector2 CalculateVelocityChange(this Vector2 targetVelocity, Vector2 currentVelocity, Vector2 maxChange)
	// 	{
	// 		Vector2 velocityChange = targetVelocity - currentVelocity;
	// 		velocityChange.x = Mathf.Clamp(velocityChange.x, -maxChange.x, maxChange.x);
	// 		velocityChange.y = Mathf.Clamp(velocityChange.y, -maxChange.y, maxChange.y);
	// 		return velocityChange;
	// 	}

	// 	public static Vector2 CalculateVelocityChange(this Vector2 targetVelocity, Vector2 currentVelocity, float maxChange)
	// 	{
	// 		return CalculateVelocityChange(targetVelocity, currentVelocity, Vector2.one * maxChange);
	// 	}

	// 	public static Vector2 CalculateVelocityChange(this Vector2 targetVelocity, Vector2 currentVelocity)
	// 	{
	// 		return targetVelocity - currentVelocity;
	// 	}

	// 	public static Vector2 ClampMagnitude(this Vector2 vec2, float mag)
	// 	{
	// 		return Vector2.ClampMagnitude(vec2, mag);
	// 	}

	public static float Distance(this Vector2 v1, Vector2 v2)
	{
		return Vector2.Distance(v1, v2);
	}

	// 	public static float PathLength(this IList<Vector2> waypoints)
	// 	{
	// 		float sum = 0;

	// 		for (int i = 1; i < waypoints.Count; i++)
	// 		{
	// 			sum += Vector2.Distance(waypoints[i - 1], waypoints[i]);
	// 		}

	// 		return sum;
	// 	}

	// 	public static float PathLength(this Span<Vector2> waypoints)
	// 	{
	// 		float sum = 0;

	// 		for (int i = 1; i < waypoints.Length; i++)
	// 		{
	// 			sum += Vector2.Distance(waypoints[i - 1], waypoints[i]);
	// 		}

	// 		return sum;
	// 	}
}