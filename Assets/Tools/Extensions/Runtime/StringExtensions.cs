using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace JD
{
	public static class StringExtensions
	{
		private static readonly TextInfo TextInfo = new CultureInfo("en-GB", false).TextInfo;

		public static string LinebreakAfter(this string lines, int max)
		{
			StringBuilder stringBuilder = new StringBuilder();

			string[] del = { "\r\n", "\n" };
			string[] split = lines.Split(del, StringSplitOptions.None);

			foreach (string line in split)
			{
				stringBuilder.AppendLine(line.BreakAfter(max));
			}

			stringBuilder.TrimEnd();
			return stringBuilder.ToString();
		}

		private static string BreakAfter(this string inputText, int lineLength)
		{
			StringBuilder sb = Pools.GetStringBuilder();

			char[] delimiters = { ' ' };
			string[] words = inputText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
			int charCounter = 0;

			foreach (string t in words)
			{
				sb.AppendFormat("{0} ", t);
				charCounter += t.Length + 1;

				if (charCounter - 1 >= lineLength)
				{
					sb.Remove(sb.Length - 1, 1);
					sb.AppendLine();
					charCounter = 0;
				}
			}

			sb.TrimEnd();
			var val = sb.ToString();
			Pools.Release(sb);
			return val;
		}

		public static bool IsNullOrWhiteSpaceOrEmpty(this string str)
		{
			return string.IsNullOrWhiteSpace(str);
		}

		public static string ToConstantCase(this string str)
		{
			str = str.UnderscoreToSpace();
			str = str.SplitCamelCase();
			str = str.Replace("  ", " ");
			str = str.Replace("  ", " ");
			str = str.ToUpper();
			str = str.SpaceToUnderscore();
			return str;
		}

		public static string SplitCamelCase(this string str)
		{
			return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
		}

		public static string ToTitleCase(this string str)
		{
			return TextInfo.ToTitleCase(str.ToLower());
		}

		public static string TruncateAfterCharacter(this string value, char character)
		{
			return string.IsNullOrWhiteSpace(value) ? value : value[..value.IndexOf(character)];
		}

		public static string Truncate(this string value, int maxLength)
		{
			return string.IsNullOrWhiteSpace(value) ? value : value.Length <= maxLength ? value : value[..maxLength];
		}

		public static string Cut(this string value, int firstCharacters)
		{
			firstCharacters = Math.Max(firstCharacters, 0);
			if (string.IsNullOrWhiteSpace(value))
			{
				return value;
			}

			if (firstCharacters >= value.Length)
			{
				return string.Empty;
			}

			return value[firstCharacters..];
		}

		public static string Ellipsis(this string value, int maxLength, string trail)
		{
			string shortName = value.Truncate(maxLength);
			if (value.Length > maxLength)
			{
				shortName += trail;
			}

			return shortName;
		}

		public static string UnderscoreToSpace(this string str)
		{
			return str.Replace("_", " ").Trim();
		}

		public static string SpaceToUnderscore(this string str)
		{
			return str.Replace(" ", "_").Trim();
		}

		public static int[] StringToIntArray(this string str)
		{
			return string.IsNullOrWhiteSpace(str) ? Array.Empty<int>() : str.Split(',').Select(int.Parse).ToArray();
		}

		// public static IOrderedEnumerable<string> Sort(this IEnumerable<string> array)
		// {
		// 	return array.OrderBy(e => e);
		// }

		public static string RemoveDiacritics(this string text)
		{
			int length = text.Length;
			ReadOnlySpan<char> normalizedString = text.Normalize(NormalizationForm.FormD);
			Span<char> span = length < 1000 ? stackalloc char[length] : new char[length];

			int i = 0;
			foreach (char c in normalizedString)
			{
				if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
				{
					span[i] = c;
					i++;
				}
			}

			return new string(span).Normalize(NormalizationForm.FormC);
		}
	}
}