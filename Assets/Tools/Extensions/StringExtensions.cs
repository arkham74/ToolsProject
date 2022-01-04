using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

public static class StringExtensions
{
	private static readonly TextInfo TextInfo = new CultureInfo("es-ES", false).TextInfo;

	public static string AddSpaceBeforeCapital(this string e)
	{
		return string.Concat(e.Select(x => char.IsUpper(x) ? " " + x : x.ToString()));
	}

	public static string SplitCamelCase(this string str)
	{
		return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
	}

	public static string ToTitleCase(this string str)
	{
		return TextInfo.ToTitleCase(str.ToLower());
	}

	public static string Truncate(this string value, int maxLength)
	{
		return string.IsNullOrWhiteSpace(value) ? value : value.Length <= maxLength ? value : value.Substring(0, maxLength);
	}

	public static string Ellipsis(this string value, int maxLength, string trail)
	{
		string shortName = value.Truncate(maxLength);
		if (value.Length > maxLength) shortName += trail;
		return shortName;
	}

	public static string RemoveAfter(this string value, char character)
	{
		return string.IsNullOrWhiteSpace(value) ? value : value.Substring(0, value.IndexOf(character));
	}

	public static string UnderscoreToSpace(this string str)
	{
		return str.Replace("_", " ").Trim();
	}

	public static int[] StringToIntArray(this string str)
	{
		return string.IsNullOrWhiteSpace(str) ? Array.Empty<int>() : str.Split(',').Select(int.Parse).ToArray();
	}
}