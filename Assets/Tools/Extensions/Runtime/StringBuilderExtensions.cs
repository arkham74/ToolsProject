using System;
using System.Text;

namespace JD
{
	public static class StringBuilderExtensions
	{
		public static void RemoveLast(this StringBuilder stringBuilder, int count)
		{
			stringBuilder.Remove(stringBuilder.Length - count, count);
		}

		public static void TrimEnd(this StringBuilder stringBuilder)
		{
			while (stringBuilder.Length > 0 && char.IsWhiteSpace(stringBuilder[^1]))
			{
				stringBuilder.Length -= 1;
			}
		}

		public static void AppendRepeat(this StringBuilder stringBuilder, string value, int count)
		{
			for (int i = 0; i < count; i++)
			{
				stringBuilder.Append(value);
			}
		}

		public static void AppendRepeatLine(this StringBuilder stringBuilder, string value, int count)
		{
			if (count > 0)
			{
				stringBuilder.AppendRepeat(value, count);
				stringBuilder.AppendLine();
			}
		}

		public static void AppendLine(this StringBuilder stringBuilder, byte value)
		{
			stringBuilder.Append(value);
			stringBuilder.AppendLine();
		}

		public static void AppendLine(this StringBuilder stringBuilder, bool value)
		{
			stringBuilder.Append(value);
			stringBuilder.AppendLine();
		}

		public static void AppendLine(this StringBuilder stringBuilder, ulong value)
		{
			stringBuilder.Append(value);
			stringBuilder.AppendLine();
		}

		public static void AppendLine(this StringBuilder stringBuilder, uint value)
		{
			stringBuilder.Append(value);
			stringBuilder.AppendLine();
		}

		public static void AppendLine(this StringBuilder stringBuilder, ushort value)
		{
			stringBuilder.Append(value);
			stringBuilder.AppendLine();
		}

		public static void AppendLine(this StringBuilder stringBuilder, char value)
		{
			stringBuilder.Append(value);
			stringBuilder.AppendLine();
		}

		public static void AppendLine(this StringBuilder stringBuilder, StringBuilder value)
		{
			stringBuilder.Append(value);
			stringBuilder.AppendLine();
		}

		public static void AppendLine(this StringBuilder stringBuilder, sbyte value)
		{
			stringBuilder.Append(value);
			stringBuilder.AppendLine();
		}

		public static void AppendLine(this StringBuilder stringBuilder, ReadOnlySpan<char> value)
		{
			stringBuilder.Append(value);
			stringBuilder.AppendLine();
		}

		public static void AppendLine(this StringBuilder stringBuilder, object value)
		{
			stringBuilder.Append(value);
			stringBuilder.AppendLine();
		}

		public static void AppendLine(this StringBuilder stringBuilder, long value)
		{
			stringBuilder.Append(value);
			stringBuilder.AppendLine();
		}

		public static void AppendLine(this StringBuilder stringBuilder, int value)
		{
			stringBuilder.Append(value);
			stringBuilder.AppendLine();
		}

		public static void AppendLine(this StringBuilder stringBuilder, short value)
		{
			stringBuilder.Append(value);
			stringBuilder.AppendLine();
		}

		public static void AppendLine(this StringBuilder stringBuilder, double value)
		{
			stringBuilder.Append(value);
			stringBuilder.AppendLine();
		}

		public static void AppendLine(this StringBuilder stringBuilder, char[] value)
		{
			stringBuilder.Append(value);
			stringBuilder.AppendLine();
		}

		public static void AppendLine(this StringBuilder stringBuilder, float value)
		{
			stringBuilder.Append(value);
			stringBuilder.AppendLine();
		}

		public static void AppendLine(this StringBuilder stringBuilder, decimal value)
		{
			stringBuilder.Append(value);
			stringBuilder.AppendLine();
		}
	}
}
