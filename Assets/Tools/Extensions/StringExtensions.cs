using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

public static class StringExtensions
{
	private static readonly TextInfo TextInfo = new CultureInfo("es-ES", false).TextInfo;

	public static bool IsNullOrWhiteSpace(this string str)
	{
		return string.IsNullOrWhiteSpace(str);
	}

	public static string ToSplitCamelCase(this string str)
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
			return value;
		else if (firstCharacters >= value.Length)
			return string.Empty;
		else
			return value[firstCharacters..];
	}

	public static string Ellipsis(this string value, int maxLength, string trail)
	{
		string shortName = value.Truncate(maxLength);
		if (value.Length > maxLength) shortName += trail;
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

	public static IOrderedEnumerable<string> Sort(this IEnumerable<string> array)
	{
		return array.OrderBy(e => e);
	}
}