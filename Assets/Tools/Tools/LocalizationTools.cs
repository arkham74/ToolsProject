#if TOOLS_LOCALIZATION
using UnityEngine.Localization.Settings;

namespace JD
{
	public static class LocalizationTools
	{
		public static string GetLocalizedString(string key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedString(key);
		}

		public static string GetLocalizedString(string key, params object[] args)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedString(key, arguments: args);
		}
	}
}
#endif