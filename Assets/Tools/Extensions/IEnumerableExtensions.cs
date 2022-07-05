using System.Collections.Generic;

public static class IEnumerableExtensions
{
	public static string Join<T>(this IEnumerable<T> array, string separator = ", ")
	{
		return string.Join(separator, array);
	}
}