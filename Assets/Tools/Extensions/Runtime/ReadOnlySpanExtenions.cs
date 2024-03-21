namespace JD
{
	public static class ReadOnlySpanExtenions
	{
		public static ReadOnlySpan<T> Join<T>(this ReadOnlySpan<T> left, ReadOnlySpan<T> right)
		{
			return ReadOnlySpanUtils.Join(left, right);
		}

		public static void Append<T>(this ref ReadOnlySpan<T> left, ReadOnlySpan<T> right)
		{
			left = ReadOnlySpanUtils.Join(left, right);
		}

		public static void CopyTo<T>(this ReadOnlySpan<T> source, ref ReadOnlySpan<T> dest, int offset)
		{
			ReadOnlySpanUtils.CopyTo(source, ref dest, offset);
		}

		public static void CopyFrom<T>(this ref ReadOnlySpan<T> dest, ReadOnlySpan<T> source, int offset = 0)
		{
			ReadOnlySpanUtils.CopyTo(source, ref dest, offset);
		}
	}
}