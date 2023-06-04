using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Freya;

namespace JD
{
	public static class ArrayExtensions
	{
		public static bool Exists<T>(this T[] array, Predicate<T> match)
		{
			return Array.Exists(array, match);
		}

		public static bool TrueForAll<T>(this T[] array, Predicate<T> match)
		{
			return Array.TrueForAll(array, match);
		}

		public static int BinarySearch<T>(this T[] array, T value)
		{
			return Array.BinarySearch(array, value);
		}

		public static int FindIndex<T>(this T[] array, Predicate<T> match)
		{
			return Array.FindIndex(array, match);
		}

		public static int FindLastIndex<T>(this T[] array, Predicate<T> match)
		{
			return Array.FindLastIndex(array, match);
		}

		public static int IndexOf<T>(this T[] array, T value)
		{
			return Array.IndexOf(array, value);
		}

		public static int LastIndexOf<T>(this T[] array, T value)
		{
			return Array.LastIndexOf(array, value);
		}

		public static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
		{
			return Array.AsReadOnly(array);
		}

		public static T Find<T>(this T[] array, Predicate<T> match)
		{
			return Array.Find(array, match);
		}

		public static T FindLast<T>(this T[] array, Predicate<T> match)
		{
			return Array.FindLast(array, match);
		}

		public static T[] FindAll<T>(this T[] array, Predicate<T> match)
		{
			return Array.FindAll(array, match);
		}

		public static void Fill<T>(this T[] array, T value)
		{
			Array.Fill(array, value);
		}

		public static void ForEach<T>(this T[] array, Action<T> action)
		{
			Array.ForEach(array, action);
		}

		public static void Reverse<T>(this T[] array)
		{
			Array.Reverse(array);
		}

		public static void Sort<T>(this T[] array)
		{
			Array.Sort(array);
		}
	}
}