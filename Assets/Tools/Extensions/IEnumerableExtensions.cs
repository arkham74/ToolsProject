using System.Collections.Generic;

namespace JD
{
	public static class IEnumerableExtensions
	{
		public static string Join<T>(this IEnumerable<T> array, string separator = ", ")
		{
			return string.Join(separator, array);
		}
	}
}