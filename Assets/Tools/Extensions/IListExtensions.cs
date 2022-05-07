using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

public static class IListExtensions
{
	public static T Repeat<T>(this IList<T> array, int index)
	{
		return array[index % array.Count];
	}

	public static T Loop<T>(this IList<T> array, int index)
	{
		return array[(int)Mathf.Repeat(index, array.Count)];
	}

	public static T Random<T>(this IList<T> list)
	{
		if (list == null) throw new ArgumentNullException();
		if (list.Count <= 0) throw new ArgumentException("List must have more than 0 elements");

		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	public static IOrderedEnumerable<T> Shuffle<T>(this IEnumerable<T> array)
	{
		return array.OrderBy(e => UnityEngine.Random.value);
	}

	public static void Shuffle<T>(this IList<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = UnityEngine.Random.Range(0, n + 1);
			(list[n], list[k]) = (list[k], list[n]);
		}
	}

	public static int IndexOf<T>(this T[] array, T element)
	{
		return Array.IndexOf(array, element);
	}

	public static T RepeatOrDefault<T>(this IList<T> array, int index)
	{
		int lenght = array.Count();
		return lenght > 0 ? array.ElementAt(index % lenght) : default;
	}

	public static void ForEach<T>(this IList<T> array, Action<T> action)
	{
		foreach (T item in array)
		{
			action(item);
		}
	}

	public static void ForEach<T>(this IList<T> array, Action<int, T> action)
	{
		for (int i = 0; i < array.Count(); i++)
		{
			action(i, array.ElementAt(i));
		}
	}

	public static T Closest<T>(this IList<T> enumerable, Component target) where T : Component
	{
		T clos = null;
		float min = float.MaxValue;
		foreach (T item in enumerable)
		{
			float dist = Vector3.Distance(item.transform.position, target.transform.position);
			if (dist < min)
			{
				min = dist;
				clos = item;
			}
		}
		return clos;
	}

	public static string Join<T>(this IList<T> array, string separator = ", ")
	{
		return string.Join(separator, array);
	}

	public static void LogWarning<T>(this IList<T> array)
	{
		var sb = new StringBuilder();

		if (array == null)
		{
			Debug.LogWarning("Array is NULL");
			return;
		}

		if (array.Count <= 0)
		{
			Debug.LogWarning("Array is empty");
			return;
		}

		for (int i = 0; i < array.Count; i++)
		{
			sb.AppendFormat($"[{i}] {array[i]}");
			sb.AppendLine();
		}

		Debug.LogWarning(sb);
	}

	public static void Log<T>(this IList<T> array)
	{
		var sb = new StringBuilder();

		if (array == null)
		{
			Debug.LogWarning("Array is NULL");
			return;
		}

		if (array.Count <= 0)
		{
			Debug.LogWarning("Array is empty");
			return;
		}

		for (int i = 0; i < array.Count; i++)
		{
			sb.Append(array[i]);
			sb.AppendLine();
		}

		Debug.Log(sb);
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

	public static bool InRange<T>(this IList<T> list, int index)
	{
		bool gtz = index >= 0;
		bool ltl = index < list.Count;
		return gtz && ltl;
	}

	public static T AtIndexClamp<T>(this IList<T> array, int index)
	{
		return array[Mathf.Clamp(index, 0, array.Count - 1)];
	}

	public static T AtOrDefault<T>(this IList<T> array, int x, int y, int width, T def = default)
	{
		return array.AtOrDefault(Tools.Map2DTo1D(x, y, width), def);
	}

	public static T AtOrDefault<T>(this IList<T> array, int i, T def = default)
	{
		return i >= 0 && i < array.Count ? array[i] : def;
	}

	public static void Populate<T>(this IList<T> arr, T value)
	{
		for (int i = 0; i < arr.Count; i++)
		{
			arr[i] = value;
		}
	}

	public static bool TryRandom<T>(this IList<T> array, out T element)
	{
		element = default;
		if (array == null) return false;
		if (array.Count <= 0) return false;
		element = array[UnityEngine.Random.Range(0, array.Count)];
		return true;
	}

	public static bool CompareList<T>(this IList<T> l1, IList<T> l2)
	{
		if (l1.Count != l2.Count)
			return false;

		return l2.All(e => l1.Contains(e));
	}

	public static C[] ForEachPair<T, C>(this IList<T> array, Func<T, T, C> method)
	{
		if (array == null)
			throw new ArgumentException("ForEachPair", nameof(array));

		if (method == null)
			throw new ArgumentException("ForEachPair", nameof(method));

		int count = array.Count - 1;
		C[] list = new C[count];

		for (int i = 0; i < count; i++)
		{
			list[i] = method(array[i], array[i + 1]);
		}

		return list;
	}
}