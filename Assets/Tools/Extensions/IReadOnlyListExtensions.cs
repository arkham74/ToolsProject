using System.Collections.Generic;
using Freya;

public static class IReadOnlyListExtensions
{
	public static string Join<T>(this IReadOnlyList<T> array, string separator = ", ")
	{
		return string.Join(separator, array);
	}

	public static T Loop<T>(this IReadOnlyList<T> array, int index)
	{
		return array[Mathfs.Mod(index, array.Count)];
	}

	public static bool ContainsIndex<T>(this IReadOnlyList<T> list, int index)
	{
		bool greaterThanEqualZero = index >= 0;
		bool lessThanCount = index < list.Count;
		return greaterThanEqualZero && lessThanCount;
	}

	public static int IndexOf<T>(this IReadOnlyList<T> list, T element)
	{
		return (list as IList<T>).IndexOf(element);
	}
}