namespace JD
{
	public static class ReadOnlySpanUtils
	{
		public static ReadOnlySpan<T> Join<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
		{
			Span<T> combined = new T[left.Length + right.Length];
			left.CopyTo(combined);
			for (int i = 0; i < right.Length; i++)
			{
				combined[i + left.Length] = right[i];
			}
			return combined;
		}

		public static void CopyTo<T>(ReadOnlySpan<T> source, ref ReadOnlySpan<T> destination, int offset = 0)
		{
			Span<T> dest = new T[destination.Length];
			destination.CopyTo(dest);
			int len = Math.Min(destination.Length, source.Length);
			len = Math.Min(len, destination.Length - offset);
			int start = -Math.Min(0, offset);
			for (int i = start; i < len; i++)
			{
				dest[i + offset] = source[i];
			}
			destination = dest;
		}
	}
}