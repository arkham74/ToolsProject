using System;

namespace JD
{
	public static class SpanUtils
	{
		public static Span<T> Join<T>(Span<T> left, Span<T> right)
		{
			Span<T> combined = new T[left.Length + right.Length];
			left.CopyTo(combined);
			for (int i = 0; i < right.Length; i++)
			{
				combined[i + left.Length] = right[i];
			}
			return combined;
		}

		public static void CopyTo<T>(Span<T> source, ref Span<T> destination, int offset = 0)
		{
			int len = Math.Min(destination.Length, source.Length);
			len = Math.Min(len, destination.Length - offset);
			int start = -Math.Min(0, offset);
			for (int i = start; i < len; i++)
			{
				destination[i + offset] = source[i];
			}
		}
	}
}