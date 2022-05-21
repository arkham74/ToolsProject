#if TOOLS_LOCALIZATION
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using TMPro;

public static class LocalizationExtensions
{
	public static void SetLocalizedText(this TMP_Text text, string tableKey, string localeKey, string format = "{0}")
	{
		LocalizedString localizedString = new LocalizedString(tableKey, localeKey);
		localizedString.WaitForCompletion = true;
		text.SetText(string.Format(format, localizedString.GetLocalizedString()));
	}

	public static void SetLocalizedTextFallback(this TMP_Text text, string tableKey, string localeKey, string fallback, string format = "{0}")
	{
		LocalizedDatabase<StringTable, StringTableEntry>.TableEntryResult entry = LocalizationSettings.StringDatabase.GetTableEntry(tableKey, localeKey);
		string name = entry.Entry != null ? entry.Entry.GetLocalizedString() : fallback;
		text.SetText(string.Format(format, name));
	}

	public static Locale SelectByIndex(this ILocalesProvider locales, int index)
	{
		return LocalizationSettings.SelectedLocale = locales.Locales[index];
	}

	public static int GetIndex(this Locale locale)
	{
		return LocalizationSettings.AvailableLocales.Locales.IndexOf(locale);
	}

	public static string GetNativeName(this Locale locale)
	{
		return locale.Identifier.CultureInfo.NativeName;
	}
}
#endif