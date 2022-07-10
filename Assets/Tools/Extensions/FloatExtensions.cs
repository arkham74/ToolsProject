using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Freya;
using UnityEngine;

public static class FloatExtensions
{
	public static float PingPong(this float t, float len = 1f)
	{
		return Mathf.PingPong(t, len);
	}

	public static bool Approx(this float a, float b)
	{
		return Mathfs.Approximately(a, b);
	}

	// 	public static IEnumerable<float> Normalize(this IEnumerable<float> array)
	// 	{
	// 		float sum = array.Sum();
	// 		return array.Select(e => e / sum);
	// 	}

	// 	public static IOrderedEnumerable<float> Sort(this IEnumerable<float> array)
	// 	{
	// 		return array.OrderBy(e => e);
	// 	}

	public static float ClampEuler(this float eulerAngle)
	{
		if (eulerAngle > 180)
			eulerAngle -= 360;

		if (eulerAngle < -180)
			eulerAngle += 360;

		return eulerAngle;
	}

	// 	public static float ClampAngle(this float lfAngle, float lfMin, float lfMax)
	// 	{
	// 		if (lfAngle < -360f) lfAngle += 360f;
	// 		if (lfAngle > 360f) lfAngle -= 360f;
	// 		return Mathf.Clamp(lfAngle, lfMin, lfMax);
	// 	}

	// 	public static float Random(this float value, float start = 0)
	// 	{
	// 		return UnityEngine.Random.Range(start, value);
	// 	}

	// 	// public static float Abs(this float value)
	// 	// {
	// 	// 	return Mathf.Abs(value);
	// 	// }

	public static float InvSqrt(this float f)
	{
		return 1f / f.SignedSqrt();
	}

	// 	// public static float Sqrt(this float f)
	// 	// {
	// 	// 	return Mathf.Sqrt(f);
	// 	// }

	public static float SignedSqrt(this float f)
	{
		return Mathf.Sqrt(Mathf.Abs(f)) * Mathf.Sign(f);
	}

	// 	public static float Sign(this float f)
	// 	{
	// 		return Mathf.Sign(f);
	// 	}

	// 	public static float Clamp(this float value, float min, float max)
	// 	{
	// 		return Mathf.Clamp(value, min, max);
	// 	}

	// 	// public static float Clamp01(this float value)
	// 	// {
	// 	// 	return Mathf.Clamp01(value);
	// 	// }

	// 	// public static float Repeat(this float value, float lenght = 1f)
	// 	// {
	// 	// 	return Mathf.Repeat(value, lenght);
	// 	// }

	public static float RoundPosterize(this float v, float steps)
	{
		return (v * steps).Round() / steps;
	}

	public static float CeilPosterize(this float v, float steps)
	{
		return (v * steps).Ceil() / steps;
	}

	public static float FloorPosterize(this float v, float steps)
	{
		return (v * steps).Floor() / steps;
	}

	public static float TriangleWave(this float x)
	{
		float Mod(float x, float y)
		{
			return y * (x / y - Mathf.Floor(x / y));
		}

		return Mathf.Abs(Mod(x, 4) - 2) - 1;
	}

	public static float TriangleWave2(this float x)
	{
		return TriangleWave(x + 1);
	}

	// 	// public static float Remap(this float value, float from, float to, float min, float max)
	// 	// {
	// 	// 	return (value - from) / (to - from) * (max - min) + min;
	// 	// }

	// 	public static float RemapClamped(this float value, float inputMin, float inputMax, float outputMin, float outputMax)
	// 	{
	// 		return Mathf.Clamp(value.Remap(inputMin, inputMax, outputMin, outputMax), outputMin, outputMax);
	// 	}

	// 	public static int RoundToInt(this float v)
	// 	{
	// 		return Mathf.RoundToInt(v);
	// 	}

	// 	public static int FloorToInt(this float v)
	// 	{
	// 		return Mathf.FloorToInt(v);
	// 	}

	// 	public static int CeilToInt(this float v)
	// 	{
	// 		return Mathf.CeilToInt(v);
	// 	}

	public static float RoundToDecimal(this float v, int places)
	{
		float mlt = Mathf.Pow(10, places);
		return v.RoundPosterize(mlt);
	}

	public static float CeilToDecimal(this float v, int places)
	{
		float mlt = Mathf.Pow(10, places);
		return v.CeilPosterize(mlt);
	}

	public static float FloorToDecimal(this float v, int places)
	{
		float mlt = Mathf.Pow(10, places);
		return v.FloorPosterize(mlt);
	}

	public static float ToLogDBScale(this float value)
	{
		if (value <= 0)
			value = 0.0001f;
		return Mathf.Log10(value) * 20;
	}

	// 	public static float FromLog10(this float value)
	// 	{
	// 		return Mathf.Pow(10f, value / 20f);
	// 	}

	public static float OneMinus(this float value)
	{
		return 1f - value;
	}

	// 	public static float ToKPH(this float mps)
	// 	{
	// 		return mps * 3.6f;
	// 	}

	// 	public static float ToMPS(this float kph)
	// 	{
	// 		return kph / 3.6f;
	// 	}

	/// <summary>Exponential interpolation, the multiplicative version of lerp, useful for values such as scaling or zooming</summary>
	/// <param name="a">The start value</param>
	/// <param name="b">The end value</param>
	/// <param name="t">The t-value from 0 to 1 representing position along the lerp</param>
	public static float Eerp(this float t, float a, float b)
	{
		return Freya.Mathfs.Eerp(a, b, t);
	}

	/// <summary>Inverse exponential interpolation, the multiplicative version of InverseLerp, useful for values such as scaling or zooming</summary>
	/// <param name="a">The start value</param>
	/// <param name="b">The end value</param>
	/// <param name="v">A value between a and b. Note: values outside this range are still valid, and will be extrapolated</param>
	public static float InverseEerp(this float v, float a, float b)
	{
		return Mathfs.InverseEerp(a, b, v);
	}

	/// <summary>Given a value between a and b, returns its normalized location in that range, as a t-value (interpolant) from 0 to 1</summary>
	/// <param name="a">The start of the range, where it would return 0</param>
	/// <param name="b">The end of the range, where it would return 1</param>
	/// <param name="value">A value between a and b. Note: values outside this range are still valid, and will be extrapolated</param>
	public static float InverseLerp(this float v, float a, float b)
	{
		return Mathfs.InverseLerp(a, b, v);
	}
}