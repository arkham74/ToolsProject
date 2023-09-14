using System;
using System.Collections.Generic;
using System.Linq;
using Freya;
using UnityEngine;
using UnityEngine.Pool;

namespace JD
{
	public static class IEnumerableExtensions
	{
		public static PooledObject<List<T>> AsList<T>(this IEnumerable<T> collection, out List<T> list)
		{
			PooledObject<List<T>> pooledObject = ListPool<T>.Get(out list);
			list.AddRange(collection);
			return pooledObject;
		}

		public static IEnumerable<T> Page<T>(this IEnumerable<T> collection, int page, int pageSize)
		{
			return collection.Skip(page * pageSize).Take(pageSize);
		}

		public static string Join<T>(this IEnumerable<T> collection, Func<T, string> selector, string separator = ", ")
		{
			return string.Join(separator, collection.Select(selector));
		}

		public static string Join<T>(this IEnumerable<T> collection, string separator = ", ")
		{
			return string.Join(separator, collection);
		}

		public static int FindIndex<T>(this IEnumerable<T> collection, T element)
		{
			int i = 0;
			foreach (T item in collection)
			{
				if (item.Equals(element))
				{
					return i;
				}

				i++;
			}

			return -1;
		}

		public static T MinBy<T, S>(this IEnumerable<T> collection, Func<T, S> selector)
		{
			return collection.Select(value => (selector(value), value)).Min().value;
		}

		public static T MaxBy<T, S>(this IEnumerable<T> collection, Func<T, S> selector)
		{
			return collection.Select(value => (selector(value), value)).Max().value;
		}

		public static T Random<T>(this IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException();
			}

			int count = collection.Count();

			if (count <= 0)
			{
				throw new ArgumentException("List must have more than 0 elements");
			}

			return collection.ElementAt(UnityEngine.Random.Range(0, count));
		}

		public static IOrderedEnumerable<T> Shuffle<T>(this IEnumerable<T> collection)
		{
			return collection.OrderBy(e => UnityEngine.Random.value);
		}

		public static IOrderedEnumerable<T> ShuffleThen<T>(this IOrderedEnumerable<T> collection)
		{
			return collection.ThenBy(e => UnityEngine.Random.value);
		}

		public static T Repeat<T>(this IEnumerable<T> collection, int index)
		{
			return collection.ElementAt(Mathfs.Mod(index, collection.Count()));
		}

		public static T Sample<T>(this IEnumerable<T> collection, float t)
		{
			t = Mathfs.Clamp01(t);
			int size = Mathf.Max(collection.Count() - 1, 0);
			float value = t * size;
			int index = Mathfs.RoundToInt(value);
			return collection.ElementAt(index);
		}

		public static T RandomOrDefault<T>(this IEnumerable<T> collection)
		{
			if (collection != null)
			{
				return collection.ElementAtOrDefault(UnityEngine.Random.Range(0, collection.Count()));
			}

			return default;
		}

		public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
		{
			foreach (T item in collection)
			{
				action(item);
			}
		}

		public static void ForEach<T>(this IEnumerable<T> collection, Action<int, T> action)
		{
			for (int i = 0; i < collection.Count(); i++)
			{
				action(i, collection.ElementAt(i));
			}
		}

		public static T AtIndexClamp<T>(this IEnumerable<T> collection, int index)
		{
			return collection.ElementAt(Mathf.Clamp(index, 0, collection.Count() - 1));
		}

		public static bool TryRandom<T>(this IEnumerable<T> collection, out T element)
		{
			element = default;
			if (collection == null)
			{
				return false;
			}

			int count = collection.Count();
			if (count <= 0)
			{
				return false;
			}

			element = collection.ElementAt(UnityEngine.Random.Range(0, count));
			return true;
		}

		public static bool CompareList<T>(this IEnumerable<T> collection1, IEnumerable<T> collection2)
		{
			if (collection1.Count() != collection2.Count())
			{
				return false;
			}

			return collection2.All(e => collection1.Contains(e));
		}

		public static C[] ForEachPair<T, C>(this IEnumerable<T> collection, Func<T, T, C> method)
		{
			if (collection == null)
			{
				throw new ArgumentException("ForEachPair", nameof(collection));
			}

			if (method == null)
			{
				throw new ArgumentException("ForEachPair", nameof(method));
			}

			int count = collection.Count() - 1;
			C[] list = new C[count];

			for (int i = 0; i < count; i++)
			{
				list[i] = method(collection.ElementAt(i), collection.ElementAt(i + 1));
			}

			return list;
		}

		public static void ForEachPair<T>(this IEnumerable<T> collection, Action<T, T> method)
		{
			if (collection == null)
			{
				throw new ArgumentException("ForEachPair", nameof(collection));
			}

			if (method == null)
			{
				throw new ArgumentException("ForEachPair", nameof(method));
			}

			for (int i = 1; i < collection.Count(); i++)
			{
				method(collection.ElementAt(i - 1), collection.ElementAt(i));
			}
		}

		public static bool ContainsIndex<T>(this IEnumerable<T> collection, int index)
		{
			bool greaterThanEqualZero = index >= 0;
			bool lessThanCount = index < collection.Count();
			return greaterThanEqualZero && lessThanCount;
		}

		public static int IndexOf<T>(this IEnumerable<T> collection, T element)
		{
			int count = collection.Count();
			for (int i = 0; i < count; i++)
			{
				if (collection.ElementAt(i).Equals(element))
				{
					return i;
				}
			}

			return -1;
		}

		public static T AtOrDefault<T>(this IEnumerable<T> collection, int x, int y, int width, T def = default)
		{
			return collection.AtOrDefault(IndexTools.Map2DTo1D(x, y, width), def);
		}

		public static T AtOrDefault<T>(this IEnumerable<T> collection, int i, T def = default)
		{
			return i >= 0 && i < collection.Count() ? collection.ElementAt(i) : def;
		}

		public static T AtOrDefault<T>(this IEnumerable<IEnumerable<T>> collection, int x, int y, T def = default)
		{
			try
			{
				return collection.ElementAt(x).ElementAt(y);
			}
			catch (ArgumentOutOfRangeException)
			{
				return def;
			}
		}
	}
}