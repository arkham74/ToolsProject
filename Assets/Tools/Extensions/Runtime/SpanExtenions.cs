namespace JD
{
	public static class SpanExtenions
	{
		public static Span<T> Join<T>(this Span<T> left, Span<T> right)
		{
			return SpanUtils.Join(left, right);
		}

		public static void Append<T>(this ref Span<T> left, Span<T> right)
		{
			left = SpanUtils.Join(left, right);
		}

		public static void CopyTo<T>(this Span<T> source, ref Span<T> dest, int offset)
		{
			SpanUtils.CopyTo(source, ref dest, offset);
		}

		public static void CopyFrom<T>(this ref Span<T> dest, Span<T> source, int offset = 0)
		{
			SpanUtils.CopyTo(source, ref dest, offset);
		}
	}
}