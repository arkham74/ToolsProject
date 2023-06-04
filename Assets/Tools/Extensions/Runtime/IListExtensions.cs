using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Freya;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace JD
{
	public static class IListExtensions
	{
		public static void Shuffle<T>(this IList<T> list)
		{
			int n = list.Count();
			while (n > 1)
			{
				n--;
				int k = UnityEngine.Random.Range(0, n + 1);
				(list[n], list[k]) = (list[k], list[n]);
			}
		}

		public static void Populate<T>(this IList<T> list, T value)
		{
			for (int i = 0; i < list.Count; i++)
			{
				list[i] = value;
			}
		}

		public static void Trim<T>(this IList<T> list, int count)
		{
			while (list.Count() > count)
			{
				list.RemoveAt(list.Count() - 1);
			}
		}
	}
}
