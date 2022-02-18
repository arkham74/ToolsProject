using UnityEngine;

// ReSharper disable InconsistentNaming

// ReSharper disable IdentifierTypo
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

public static class VectorExtensions
{
	/// <summary>
	/// NON-Direction from two vectors2
	/// </summary>
	/// <param name="v"></param>
	/// <param name="target"></param>
	/// <returns></returns>
	public static Vector2 To(this Vector2 v, Vector2 target)
	{
		return target - v;
	}

	/// <summary>
	/// NON-Normalized Direction from two vector3s
	/// </summary>
	/// <param name="v"></param>
	/// <param name="target"></param>
	/// <returns></returns>
	public static Vector3 To(this Vector3 v, Vector3 target)
	{
		return target - v;
	}

	/// <summary>
	/// Normalized direction from two vector2s
	/// </summary>
	/// <param name="v"></param>
	/// <param name="target"></param>
	/// <returns></returns>
	public static Vector2 DirTo(this Vector2 v, Vector2 target)
	{
		return (target - v).normalized;
	}

	/// <summary>
	/// Normalized direction from two vectors3
	/// </summary>
	/// <param name="v"></param>
	/// <param name="target"></param>
	/// <returns></returns>
	public static Vector3 DirTo(this Vector3 v, Vector3 target)
	{
		return (target - v).normalized;
	}

	public static float Distance(this Vector3 v1, Transform v2)
	{
		return Vector3.Distance(v1, v2.position);
	}

	public static float Distance(this Vector3 v1, Vector3 v2)
	{
		return Vector3.Distance(v1, v2);
	}

	public static float Random(this Vector2 v)
	{
		return UnityEngine.Random.Range(v.x, v.y);
	}

	public static int Random(this Vector2Int v)
	{
		return UnityEngine.Random.Range(v.x, v.y);
	}

	public static Vector2 Ceil(this Vector2 v)
	{
		return new Vector2(Mathf.Ceil(v.x), Mathf.Ceil(v.y));
	}

	public static Vector2 Floor(this Vector2 v)
	{
		return new Vector2(Mathf.Floor(v.x), Mathf.Floor(v.y));
	}

	public static Vector2 InvSqrt(this Vector2 v)
	{
		return new Vector2(v.x.InvSqrt(), v.y.InvSqrt());
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

	public static Vector2 Round(this Vector2 v)
	{
		return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
	}

	public static Vector2 SetX(this Vector2 vector, float value)
	{
		return new Vector2(value, vector.y);
	}

	public static Vector2 SetY(this Vector2 vector, float value)
	{
		return new Vector2(vector.x, value);
	}

	public static Vector2 XZ(this Vector3 v)
	{
		return new Vector2(v.x, v.z);
	}

	public static Vector2Int SetX(this Vector2Int vector, int value)
	{
		return new Vector2Int(value, vector.y);
	}

	public static Vector2Int SetY(this Vector2Int vector, int value)
	{
		return new Vector2Int(vector.x, value);
	}

	public static Vector3 Abs(this Vector3 value)
	{
		return new Vector3(value.x.Abs(), value.y.Abs(), value.z.Abs());
	}

	public static Vector3 AddX(this Vector3 vector, float value)
	{
		return vector + Vector3.right * value;
	}

	public static Vector3 AddY(this Vector3 vector, float value)
	{
		return vector + Vector3.up * value;
	}

	public static Vector3 AddZ(this Vector3 vector, float value)
	{
		return vector + Vector3.forward * value;
	}

	public static Vector3 Sqrt(this Vector3 v)
	{
		return new Vector3(v.x.Sqrt(), v.y.Sqrt(), v.z.Sqrt());
	}

	public static Vector3 InvSqrt(this Vector3 v)
	{
		return new Vector3(v.x.InvSqrt(), v.y.InvSqrt(), v.z.InvSqrt());
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

	public static Vector3 SetZ(this Vector2 vector, float value)
	{
		return new Vector3(vector.x, vector.y, value);
	}

	public static Vector3Int Abs(this Vector3Int value)
	{
		return new Vector3Int(value.x.Abs(), value.y.Abs(), value.z.Abs());
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

	public static Vector2 Swap(this Vector2 vector)
	{
		return new Vector2(vector.y, vector.x);
	}

	public static Vector2Int Swap(this Vector2Int vector)
	{
		return new Vector2Int(vector.y, vector.x);
	}

	public static Vector3 Ceil(this Vector3 v)
	{
		return new Vector3(Mathf.Ceil(v.x), Mathf.Ceil(v.y), Mathf.Ceil(v.z));
	}

	public static Vector3 Floor(this Vector3 v)
	{
		return new Vector3(Mathf.Floor(v.x), Mathf.Floor(v.y), Mathf.Floor(v.z));
	}

	public static Vector3 Round(this Vector3 v)
	{
		return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
	}

	public static Vector3Int CeilInt(this Vector3 v)
	{
		return new Vector3Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y), Mathf.CeilToInt(v.z));
	}

	public static Vector3Int FloorInt(this Vector3 v)
	{
		return new Vector3Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y), Mathf.FloorToInt(v.z));
	}

	public static Vector3Int RoundInt(this Vector3 v)
	{
		return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
	}

	public static Vector2 InvertX(this Vector2 v)
	{
		return v.SetX(-v.x);
	}

	public static Vector2 InvertY(this Vector2 v)
	{
		return v.SetY(-v.y);
	}

	public static Vector3 InvertX(this Vector3 v)
	{
		return v.SetX(-v.x);
	}

	public static Vector3 InvertY(this Vector3 v)
	{
		return v.SetY(-v.y);
	}

	public static Vector3 InvertZ(this Vector3 v)
	{
		return v.SetZ(-v.z);
	}

	public static Vector3 XYtoXZ(this Vector3 v)
	{
		return new Vector3(v.x, v.z, v.y);
	}

	public static Vector3 XYtoXZ(this Vector2 v, float y = 0)
	{
		return new Vector3(v.x, y, v.y);
	}

	public static Vector2 XZtoXY(this Vector3 v)
	{
		return new Vector2(v.x, v.z);
	}

	public static Vector3 XYtoZY(this Vector2 v, float x = 0)
	{
		return new Vector3(x, v.y, v.x);
	}

	public static Vector2 XYtoYX(this Vector2 v)
	{
		return new Vector2(v.y, v.x);
	}

	public static Vector2 OneMinus(this Vector2 v)
	{
		return Vector2.one - v;
	}

	public static Vector3 OneMinus(this Vector3 v)
	{
		return Vector3.one - v;
	}

	public static Vector4 OneMinus(this Vector4 v)
	{
		return Vector4.one - v;
	}

	public static Vector2 Remap(this Vector2 value, float from1 = -1, float to1 = 1, float from2 = 0, float to2 = 1)
	{
		value.x = (value.x - from1) / (to1 - from1) * (to2 - from2) + from2;
		value.y = (value.y - from1) / (to1 - from1) * (to2 - from2) + from2;
		return value;
	}

	public static Vector3 Remap(this Vector3 value, float from1 = -1, float to1 = 1, float from2 = 0, float to2 = 1)
	{
		value.x = (value.x - from1) / (to1 - from1) * (to2 - from2) + from2;
		value.y = (value.y - from1) / (to1 - from1) * (to2 - from2) + from2;
		value.z = (value.z - from1) / (to1 - from1) * (to2 - from2) + from2;
		return value;
	}

	public static Vector4 Remap(this Vector4 value, float from1 = -1, float to1 = 1, float from2 = 0, float to2 = 1)
	{
		value.x = (value.x - from1) / (to1 - from1) * (to2 - from2) + from2;
		value.y = (value.y - from1) / (to1 - from1) * (to2 - from2) + from2;
		value.z = (value.z - from1) / (to1 - from1) * (to2 - from2) + from2;
		value.w = (value.w - from1) / (to1 - from1) * (to2 - from2) + from2;
		return value;
	}

	public static Vector3 Round(this Vector3 v, int places)
	{
		float mlt = Mathf.Pow(10, places);
		return (v * mlt).Round() / mlt;
	}

	public static Vector3 Ceil(this Vector3 v, int places)
	{
		float mlt = Mathf.Pow(10, places);
		return (v * mlt).Ceil() / mlt;
	}

	public static Vector3 Floor(this Vector3 v, int places)
	{
		float mlt = Mathf.Pow(10, places);
		return (v * mlt).Floor() / mlt;
	}

	public static Vector2 Round(this Vector2 v, int places)
	{
		float mlt = Mathf.Pow(10, places);
		return (v * mlt).Round() / mlt;
	}

	public static Vector2 Ceil(this Vector2 v, int places)
	{
		float mlt = Mathf.Pow(10, places);
		return (v * mlt).Ceil() / mlt;
	}

	public static Vector2 Floor(this Vector2 v, int places)
	{
		float mlt = Mathf.Pow(10, places);
		return (v * mlt).Floor() / mlt;
	}

	public static Vector3 CalculateVelocityChange(this Vector3 targetVelocity, Vector3 currentVelocity, Vector3 maxChange)
	{
		Vector3 velocityChange = targetVelocity - currentVelocity;
		velocityChange.x = Mathf.Clamp(velocityChange.x, -maxChange.x, maxChange.x);
		velocityChange.y = Mathf.Clamp(velocityChange.y, -maxChange.y, maxChange.y);
		velocityChange.z = Mathf.Clamp(velocityChange.z, -maxChange.z, maxChange.z);
		return velocityChange;
	}

	public static Vector3 CalculateVelocityChange(this Vector3 targetVelocity, Vector3 currentVelocity, float maxChange)
	{
		return CalculateVelocityChange(targetVelocity, currentVelocity, Vector3.one * maxChange);
	}

	public static Vector2 CalculateVelocityChange(this Vector2 targetVelocity, Vector2 currentVelocity, Vector2 maxChange)
	{
		Vector2 velocityChange = targetVelocity - currentVelocity;
		velocityChange.x = Mathf.Clamp(velocityChange.x, -maxChange.x, maxChange.x);
		velocityChange.y = Mathf.Clamp(velocityChange.y, -maxChange.y, maxChange.y);
		return velocityChange;
	}

	public static Vector2 CalculateVelocityChange(this Vector2 targetVelocity, Vector2 currentVelocity, float maxChange)
	{
		return CalculateVelocityChange(targetVelocity, currentVelocity, Vector2.one * maxChange);
	}

	public static Vector3 CalculateVelocityChange(this Vector3 targetVelocity, Vector3 currentVelocity)
	{
		return targetVelocity - currentVelocity;
	}

	public static float InverseLerp(this Vector3 value, Vector3 a, Vector3 b)
	{
		Vector3 AB = b - a;
		Vector3 AV = value - a;
		return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
	}
}