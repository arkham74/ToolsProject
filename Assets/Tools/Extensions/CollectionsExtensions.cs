using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

public static class CollectionsExtensions
{
	public static bool CompareList<T>(this List<T> l1, List<T> l2)
	{
		if (l1.Count != l2.Count)
			return false;

		return l2.All(e => l1.Contains(e));
	}

	public static float Length(this Vector3[] waypoints)
	{
		float sum = 0;

		for (int i = 1; i < waypoints.Length; i++)
		{
			sum += Vector3.Distance(waypoints[i - 1], waypoints[i]);
		}

		return sum;
	}


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

	public static T Repeat<T>(this T[] array, int index)
	{
		return array[index % array.Length];
	}

	public static T Repeat<T>(this IEnumerable<T> array, int index)
	{
		int lenght = array.Count();
		return array.ElementAt(index % lenght);
	}

	public static T RepeatOrDefault<T>(this IEnumerable<T> array, int index)
	{
		int lenght = array.Count();
		return lenght > 0 ? array.ElementAt(index % lenght) : default;
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

	public static T Closest<T>(this IEnumerable<T> enumerable, Component target) where T : Component
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

	public static T Random<T>(this IEnumerable<T> enumerable)
	{
		if (enumerable == null) throw new ArgumentNullException();
		int count = enumerable.Count();
		if (count <= 0) throw new ArgumentException("Enumerable must have more than 0 elements");

		int index = UnityEngine.Random.Range(0, count);
		return enumerable.ElementAtOrDefault(index);
	}

	public static T Random<T>(this List<T> list)
	{
		if (list == null) throw new ArgumentNullException();
		if (list.Count <= 0) throw new ArgumentException("List must have more than 0 elements");

		return list[UnityEngine.Random.Range(0, list.Count)];
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
		return array.ElementAt((int)Mathf.Repeat(index, array.Count()));
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

	public static IOrderedEnumerable<int> Sort(this IEnumerable<int> array)
	{
		return array.OrderBy(e => e);
	}

	public static IOrderedEnumerable<float> Sort(this IEnumerable<float> array)
	{
		return array.OrderBy(e => e);
	}

	public static IOrderedEnumerable<string> Sort(this IEnumerable<string> array)
	{
		return array.OrderBy(e => e);
	}

	public static void Shuffle<T>(this List<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = UnityEngine.Random.Range(0, n + 1);
			(list[n], list[k]) = (list[k], list[n]);
		}
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
		var sb = new StringBuilder();

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

		int i = 0;
		foreach (T item in array)
		{
			sb.AppendFormat($"[{i}] {item}\n");
			i++;
		}

		Debug.LogWarning(sb);
	}
}