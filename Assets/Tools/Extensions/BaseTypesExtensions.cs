using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

public static class BaseTypesExtensions
{
	private static readonly TextInfo TextInfo = new CultureInfo("es-ES", false).TextInfo;

	public static int Abs(this int value)
	{
		return Mathf.Abs(value);
	}

	public static int Clamp(this int value, int min, int max)
	{
		return Mathf.Clamp(value, min, max);
	}

	public static int Repeat(this int value, int lenght)
	{
		return (int) Mathf.Repeat(value, lenght);
	}

	public static int LayerToMask(this int layerIndex)
	{
		return Mathf.RoundToInt(Mathf.Pow(2, layerIndex));
	}

	public static float Abs(this float value)
	{
		return Mathf.Abs(value);
	}

	public static float InvSqrt(this float f)
	{
		return 1f / f.SignedSqrt();
	}

	public static float Sqrt(this float f)
	{
		return Mathf.Sqrt(f);
	}

	public static float SignedSqrt(this float f)
	{
		return Mathf.Sqrt(Mathf.Abs(f)) * Mathf.Sign(f);
	}

	public static float Sign(this float f)
	{
		return Mathf.Sign(f);
	}

	public static float Mod(this float x, float y)
	{
		return y * (x / y - Mathf.Floor(x / y));
	}

	public static float Clamp(this float value, float min, float max)
	{
		return Mathf.Clamp(value, min, max);
	}

	public static float Repeat(this float value, float lenght)
	{
		return Mathf.Repeat(value, lenght);
	}

	public static float RoundPosterize(this float v, float steps)
	{
		return Mathf.Round(v * steps) / steps;
	}

	public static float CeilPosterize(this float v, float steps)
	{
		return Mathf.Ceil(v * steps) / steps;
	}

	public static float FloorPosterize(this float v, float steps)
	{
		return Mathf.Floor(v * steps) / steps;
	}

	public static float TriangleWave(this float x)
	{
		return Mathf.Abs(Mod(x, 4) - 2) - 1;
	}

	public static float TriangleWave2(this float x)
	{
		return TriangleWave(x + 1);
	}

	public static float Remap(this float value, float from, float to, float min, float max)
	{
		return (value - from) / (to - from) * (max - min) + min;
	}

	public static float RemapClamped(this float value, float inputMin, float inputMax, float outputMin, float outputMax)
	{
		return Mathf.Clamp(value.Remap(inputMin, inputMax, outputMin, outputMax), outputMin, outputMax);
	}

	public static bool[] ToBoolArray(this byte b)
	{
		bool[] result = new bool[8];
		for (int i = 0; i < 8; i++)
		{
			result[i] = (b & (1 << i)) != 0;
		}

		Array.Reverse(result);
		return result;
	}

	public static int ToInt(this bool value)
	{
		return Convert.ToInt32(value);
	}

	public static byte ToByte(this bool[] source)
	{
		byte result = 0;
		int index = 8 - source.Length;
		foreach (bool b in source)
		{
			if (b)
			{
				result |= (byte) (1 << (7 - index));
			}

			index++;
		}

		return result;
	}

	public static float RoundToDecimal(this float v, int places)
	{
		float mlt = Mathf.Pow(10, places);
		return Mathf.Round(v * mlt) / mlt;
	}

	public static float CeilToDecimal(this float v, int places)
	{
		float mlt = Mathf.Pow(10, places);
		return Mathf.Ceil(v * mlt) / mlt;
	}

	public static float FloorToDecimal(this float v, int places)
	{
		float mlt = Mathf.Pow(10, places);
		return Mathf.Floor(v * mlt) / mlt;
	}

	public static float ToLog10(this float value)
	{
		if (value <= 0) value = 0.0001f;
		return Mathf.Log10(value) * 20;
	}

	public static float FromLog10(this float value)
	{
		return Mathf.Pow(10f, value / 20f);
	}

	public static string SplitCamelCase(this string str)
	{
		return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
	}

	public static string ToTitleCase(this string str)
	{
		return TextInfo.ToTitleCase(str.ToLower());
	}

	public static string Truncate(this string value, int maxLength)
	{
		return string.IsNullOrWhiteSpace(value) ? value : value.Length <= maxLength ? value : value.Substring(0, maxLength);
	}

	public static string Ellipsis(this string value, int maxLength, string trail)
	{
		string shortName = value.Truncate(maxLength);
		if (value.Length > maxLength) shortName += trail;
		return shortName;
	}

	public static string RemoveAfter(this string value, char character)
	{
		return string.IsNullOrWhiteSpace(value) ? value : value.Substring(0, value.IndexOf(character));
	}

	public static string UnderscoreToSpace(this string str)
	{
		return str.Replace("_", " ").Trim();
	}

	public static int[] StringToIntArray(this string str)
	{
		return string.IsNullOrWhiteSpace(str) ? Array.Empty<int>() : str.Split(',').Select(int.Parse).ToArray();
	}

	public static float OneMinus(this float value)
	{
		return 1f - value;
	}

	public static float ToKPH(this float mps)
	{
		return mps * 3.6f;
	}

	public static float ToMPS(this float kph)
	{
		return kph / 3.6f;
	}
}