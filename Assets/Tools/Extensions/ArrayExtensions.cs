using System;
using System.Linq;
using UnityEngine;

public static class ArrayExtensions
{
	public static bool InRange<T>(this T[] array, int index)
	{
		bool gtz = index >= 0;
		bool ltl = index < array.Length;
		return gtz && ltl;
	}

	public static int IndexOf<T>(this T[] array, T element)
	{
		return Array.IndexOf(array, element);
	}

	public static T AtOrDefault<T>(this T[] array, int x, int y, int width, T def = default)
	{
		return array.AtOrDefault(Tools.Map2DTo1D(x, y, width), def);
	}

	public static T AtOrDefault<T>(this T[] array, int i, T def = default)
	{
		return i >= 0 && i < array.Length ? array[i] : def;
	}

	public static void Populate<T>(this T[] arr, T value)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = value;
		}
	}

	public static T Repeat<T>(this T[] array, int index)
	{
		return array[index % array.Length];
	}

	public static bool TryRandom<T>(this T[] array, out T element)
	{
		element = default;
		if (array == null) return false;
		if (array.Length <= 0) return false;
		element = array[UnityEngine.Random.Range(0, array.Length)];
		return true;
	}

	public static T Random<T>(this T[] array)
	{
		if (array == null) throw new ArgumentNullException();
		if (array.Length <= 0) throw new ArgumentException("Array must have more than 0 elements");

		return array[UnityEngine.Random.Range(0, array.Length)];
	}

	public static T Loop<T>(this T[] array, int index)
	{
		return array[(int)Mathf.Repeat(index, array.Length)];
	}

	public static T AtIndexClamp<T>(this T[] array, int index)
	{
		return array[Mathf.Clamp(index, 0, array.Length - 1)];
	}
}
