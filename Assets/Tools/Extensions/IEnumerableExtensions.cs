using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class IEnumerableExtensions
{
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

	public static T Loop<T>(this IEnumerable<T> array, int index)
	{
		return array.ElementAt((int)Mathf.Repeat(index, array.Count()));
	}

	public static T Random<T>(this IEnumerable<T> enumerable)
	{
		if (enumerable == null) throw new ArgumentNullException();
		int count = enumerable.Count();
		if (count <= 0) throw new ArgumentException("Enumerable must have more than 0 elements");

		int index = UnityEngine.Random.Range(0, count);
		return enumerable.ElementAtOrDefault(index);
	}

	public static IOrderedEnumerable<T> Shuffle<T>(this IEnumerable<T> array)
	{
		return array.OrderBy(e => UnityEngine.Random.value);
	}

	public static string Join<T>(this IEnumerable<T> array, string separator = ", ")
	{
		return string.Join(separator, array);
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
}
