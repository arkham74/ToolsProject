using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public static class IListExtensions
{
	public static bool CompareList<T>(this IList<T> l1, IList<T> l2)
	{
		if (l1.Count != l2.Count)
			return false;

		return l2.All(e => l1.Contains(e));
	}

	public static bool InRange<T>(this IList<T> list, int index)
	{
		bool gtz = index >= 0;
		bool ltl = index < list.Count;
		return gtz && ltl;
	}

	public static T Random<T>(this IList<T> list)
	{
		if (list == null) throw new ArgumentNullException();
		if (list.Count <= 0) throw new ArgumentException("List must have more than 0 elements");

		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	public static T Loop<T>(this IList<T> array, int index)
	{
		return array.ElementAt((int)Mathf.Repeat(index, array.Count));
	}

	public static T AtIndexClamp<T>(this IList<T> list, int index)
	{
		return list[Mathf.Clamp(index, 0, list.Count - 1)];
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
}