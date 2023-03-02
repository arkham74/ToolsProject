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
		// public static T Min<T, S>(this IList<T> collection, Func<T, S> selector) where S : IComparable<S>
		// {
		// 	T minimal = default;
		// 	foreach (T value in collection)
		// 	{
		// 		if (selector(value).CompareTo(selector(minimal)) > 0)
		// 		{
		// 			minimal = value;
		// 		}
		// 	}
		// 	return minimal;
		// }

		public static void Trim<T>(this IList<T> array, int count)
		{
			while (array.Count > 12)
			{
				array.RemoveAt(array.Count - 1);
			}
		}

		public static string Join<T>(this List<T> array, string separator = ", ")
		{
			return string.Join(separator, array);
		}

		public static string Join<T>(this IList<T> array, string separator = ", ")
		{
			return string.Join(separator, array);
		}

		public static T Repeat<T>(this IList<T> array, int index)
		{
			return array[Mathfs.Mod(index, array.Count)];
		}

		public static T Sample<T>(this IList<T> array, float t)
		{
			t = Mathfs.Clamp01(t);
			int size = Mathf.Max(array.Count - 1, 0);
			float value = t * size;
			int index = Mathfs.RoundToInt(value);
			return array[index];
		}

		public static T Random<T>(this IList<T> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException();
			}

			if (list.Count <= 0)
			{
				throw new ArgumentException("List must have more than 0 elements");
			}

			return list[UnityEngine.Random.Range(0, list.Count)];
		}

		public static T Random<T>(this IList<T> list, int seed)
		{
			if (list == null)
			{
				throw new ArgumentNullException();
			}

			if (list.Count <= 0)
			{
				throw new ArgumentException("List must have more than 0 elements");
			}

			UnityEngine.Random.InitState(seed);
			return list[UnityEngine.Random.Range(0, list.Count)];
		}

		public static T RandomOrDefault<T>(this IList<T> list)
		{
			if (list != null)
			{
				return list.ElementAtOrDefault(UnityEngine.Random.Range(0, list.Count));
			}

			return default;
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

		public static bool ContainsIndex<T>(this IList<T> list, int index)
		{
			bool gtz = index >= 0;
			bool ltl = index < list.Count;
			return gtz && ltl;
		}

		public static bool ContainsIndex<T>(this IReadOnlyList<T> list, int index)
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
			if (array == null)
			{
				return false;
			}

			if (array.Count <= 0)
			{
				return false;
			}

			element = array[UnityEngine.Random.Range(0, array.Count)];
			return true;
		}

		public static bool CompareList<T>(this IList<T> l1, IList<T> l2)
		{
			if (l1.Count != l2.Count)
			{
				return false;
			}

			return l2.All(e => l1.Contains(e));
		}

		public static C[] ForEachPair<T, C>(this IList<T> array, Func<T, T, C> method)
		{
			if (array == null)
			{
				throw new ArgumentException("ForEachPair", nameof(array));
			}

			if (method == null)
			{
				throw new ArgumentException("ForEachPair", nameof(method));
			}

			int count = array.Count - 1;
			C[] list = new C[count];

			for (int i = 0; i < count; i++)
			{
				list[i] = method(array[i], array[i + 1]);
			}

			return list;
		}
	}
}
