#if TOOLS_LOCALIZATION
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using TMPro;

public static class LocalizationExtensions
{
	public static void SetLocalizedText(this TMP_Text text, string localeKey)
	{
		LocalizedStringDatabase stringDatabase = LocalizationSettings.StringDatabase;
		string localizedString = stringDatabase.GetLocalizedString(localeKey);
		text.SetText(localizedString);
	}

	public static void SetLocalizedText(this TMP_Text text, string localeKey, params object[] args)
	{
		LocalizedStringDatabase stringDatabase = LocalizationSettings.StringDatabase;
		string localizedString = stringDatabase.GetLocalizedString(localeKey, args);
		text.SetText(localizedString);
	}

	public static void SetLocalizedText(this TMP_Text text, string tableKey, string localeKey)
	{
		LocalizedStringDatabase stringDatabase = LocalizationSettings.StringDatabase;
		string localizedString = stringDatabase.GetLocalizedString(tableKey, localeKey);
		text.SetText(localizedString);
	}

	public static void SetLocalizedText(this TMP_Text text, string tableKey, string localeKey, params object[] args)
	{
		LocalizedStringDatabase stringDatabase = LocalizationSettings.StringDatabase;
		string localizedString = stringDatabase.GetLocalizedString(tableKey, localeKey, args);
		text.SetText(localizedString);
	}

	public static void SetLocalizedText(this TMP_Text text, string tableKey, string localeKey, string format = "{0}")
	{
		LocalizedStringDatabase stringDatabase = LocalizationSettings.StringDatabase;
		string localizedString = stringDatabase.GetLocalizedString(tableKey, localeKey);
		text.SetText(string.Format(format, localizedString));
	}

	public static void SetLocalizedTextFallback(this TMP_Text text, string tableKey, string localeKey, string fallback, string format = "{0}")
	{
		var result = LocalizationSettings.StringDatabase.GetTableEntry(tableKey, localeKey);
		string localizedString = result.Entry != null ? result.Entry.GetLocalizedString() : fallback;
		text.SetText(string.Format(format, localizedString));
	}

	public static Locale SelectByIndex(this ILocalesProvider locales, int index)
	{
		return LocalizationSettings.SelectedLocale = locales.Locales[index];
	}

	public static int GetIndex(this Locale locale)
	{
		return LocalizationSettings.AvailableLocales.Locales.IndexOf(locale);
	}

	public static int GetCurrentLocaleIndex(this ILocalesProvider locales)
	{
		var selected = LocalizationSettings.SelectedLocale;
		return locales.Locales.IndexOf(selected);
	}

	public static string GetNativeName(this Locale locale)
	{
		return locale.Identifier.CultureInfo.NativeName;
	}

	public static void SetByIndex(this ILocalesProvider locales, int index)
	{
		Locale locale = locales.SelectByIndex(index);
		LocalizationSettings.SelectedLocale = locale;
	}

}
#endif