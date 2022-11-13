using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JD
{
	public static class IEnumerableExtensions
	{
		public static int FindIndex<T>(this IEnumerable<T> collection, T locale)
		{
			int i = 0;
			foreach (T item in collection)
			{
				if (item.Equals(locale))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		public static T MinBy<T, S>(this IEnumerable<T> collection, Func<T, S> selector)
		{
			return collection.Select(e => (selector(e), e)).Min().Item2;
		}

		public static T MaxBy<T, S>(this IEnumerable<T> collection, Func<T, S> selector)
		{
			return collection.Select(e => (selector(e), e)).Max().Item2;
		}

		public static string Join<T>(this IEnumerable<T> collection, string separator = ", ")
		{
			return string.Join(separator, collection);
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

		public static T Random<T>(this IEnumerable<T> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException();
			}

			int count = list.Count();
			if (count <= 0)
			{
				throw new ArgumentException("List must have more than 0 elements");
			}

			return list.ElementAt(UnityEngine.Random.Range(0, count));
		}

		public static IOrderedEnumerable<T> Shuffle<T>(this IEnumerable<T> collection)
		{
			return collection.OrderBy(e => UnityEngine.Random.value);
		}

		public static IOrderedEnumerable<T> ShuffleThen<T>(this IOrderedEnumerable<T> collection)
		{
			return collection.ThenBy(e => UnityEngine.Random.value);
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
