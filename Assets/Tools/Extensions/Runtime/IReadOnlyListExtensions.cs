using System.Collections.Generic;
using Freya;

namespace JD
{
	public static class IReadOnlyListExtensions
	{
		public static string Join<T>(this IReadOnlyList<T> array, string separator = ", ")
		{
			return string.Join(separator, array);
		}

		// public static bool ContainsIndex<T>(this IReadOnlyList<T> list, int index)
		// {
		// 	bool greaterThanEqualZero = index >= 0;
		// 	bool lessThanCount = index < list.Count;
		// 	return greaterThanEqualZero && lessThanCount;
		// }

		public static int IndexOf<T>(this IReadOnlyList<T> list, T element)
		{
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				if (list[i].Equals(element))
				{
					return i;
				}
			}
			return -1;
		}
	}
}