using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PossibleMultipleEnumeration

public static class CollectionsExtensions
{
	public static bool InRange<T>(this List<T> list, int index)
	{
		bool gtz = index >= 0;
		bool ltl = index < list.Count;
		return gtz && ltl;
	}

	public static bool InRange<T>(this T[] array, int index)
	{
		bool gtz = index >= 0;
		bool ltl = index < array.Length;
		return gtz && ltl;
	}

	public static T Peek<T>(this LinkedList<T> list)
	{
		return list.First.Value;
	}

	public static void Enqueue<T>(this LinkedList<T> list, T element)
	{
		list.AddLast(element);
	}

	public static T Dequeue<T>(this LinkedList<T> list)
	{
		T element = list.First.Value;
		list.RemoveFirst();
		return element;
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

	public static T AtOrDefault<T>(this T[,] array, int i, T def = default)
	{
		(int x, int y) = Tools.Map1DTo2D(i, array.GetLength(0));
		return array.AtOrDefault(x, y, def);
	}

	public static T AtOrDefault<T>(this T[,] array, int x, int y, T def = default)
	{
		if (x >= 0 && x < array.GetLength(0))
		{
			if (y >= 0 && y < array.GetLength(1)) return array[x, y];
		}

		return def;
	}

	public static T AtOrDefault<T>(this IEnumerable<IEnumerable<T>> array, int x, int y, T def = default)
	{
		try
		{
			return array.ElementAt(x).ElementAt(y);
		}
		catch (ArgumentOutOfRangeException)
		{
			return def;
		}
	}

	public static void Populate<T>(this T[] arr, T value)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = value;
		}
	}

	public static T Repeat<T>(this IEnumerable<T> array, int index)
	{
		int lenght = array.Count();
		return array.ElementAt(index % lenght);
	}

	public static void GroupSetActive(this IEnumerable<GameObject> components, bool value)
	{
		foreach (GameObject item in components)
		{
			item.SetActive(value);
		}
	}

	public static void ForEach<T>(this IEnumerable<T> array, Action<T> action)
	{
		foreach (T item in array)
		{
			action(item);
		}
	}

	public static void ForEach<T>(this IEnumerable<T> array, Action<int, T> action)
	{
		for (int i = 0; i < array.Count(); i++)
		{
			action(i, array.ElementAt(i));
		}
	}

	public static IEnumerable<float> Normalize(this IEnumerable<float> array)
	{
		float sum = array.Sum();
		return array.Select(e => e / sum);
	}

	public static T Closest<T>(this IEnumerable<T> array, Component target) where T : Component
	{
		return array.Any() ? array.OrderBy(e => e.transform.Distance(target.transform)).First() : null;
	}

	public static T Random<T>(this IEnumerable<T> enumerable)
	{
		int index = UnityEngine.Random.Range(0, enumerable.Count());
		return enumerable.ElementAt(index);
	}

	public static T RandomOrDefault<T>(this IEnumerable<T> enumerable)
	{
		int index = UnityEngine.Random.Range(0, enumerable.Count());
		return enumerable.ElementAtOrDefault(index);
	}

	public static T Random<T>(this List<T> list)
	{
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	public static T Random<T>(this T[] array)
	{
		return array[UnityEngine.Random.Range(0, array.Length)];
	}

	public static T Random<T>(this T[,] array)
	{
		int l1 = array.GetLength(0);
		int l2 = array.GetLength(1);
		int x = UnityEngine.Random.Range(0, l1);
		int y = UnityEngine.Random.Range(0, l2);
		return array[x, y];
	}

	public static T Loop<T>(this IEnumerable<T> array, int index)
	{
		return array.ElementAt((int) Mathf.Repeat(index, array.Count()));
	}

	public static T AtIndexClamp<T>(this List<T> list, int index)
	{
		return list[Mathf.Clamp(index, 0, list.Count - 1)];
	}

	public static T AtIndexClamp<T>(this T[] array, int index)
	{
		return array[Mathf.Clamp(index, 0, array.Length - 1)];
	}

	public static IOrderedEnumerable<T> Shuffle<T>(this IEnumerable<T> array)
	{
		return array.OrderBy(e => UnityEngine.Random.value);
	}

	public static string Join<T>(this IEnumerable<T> array, string separator = ", ")
	{
		return string.Join(separator, array);
	}

	public static string IntArrayToString(this IEnumerable<int> intArray)
	{
		return string.Join(",", intArray);
	}

	public static void LogWarning<T>(this IEnumerable<T> array)
	{
		if (array == null)
		{
			Debug.LogWarning("Array is NULL");
			return;
		}

		if (!array.Any())
		{
			Debug.LogWarning("Array is empty");
			return;
		}

		foreach (T item in array)
		{
			if (item is Object obj)
				Debug.LogWarning(item, obj);
			else
				Debug.LogWarning(item);
		}
	}
}