using System.Threading.Tasks;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

public static class TextExtensions
{
	public static void SetLocalizedText(this TMP_Text text, string localeKey, string format = "{0}", string tableKey = "Game")
	{
		LocalizedString localizedString = new LocalizedString(tableKey, localeKey);
		text.SetText(string.Format(format, localizedString.GetLocalizedString()));
	}

	public static void SetLocalizedText(this TMP_Text text, string localeKey, string fallback, string format = "{0}", string tableKey = "Game")
	{
		LocalizedDatabase<StringTable, StringTableEntry>.TableEntryResult entry =
			LocalizationSettings.StringDatabase.GetTableEntry(tableKey, localeKey);
		string name = entry.Entry != null ? entry.Entry.GetLocalizedString() : fallback;
		text.SetText(string.Format(format, name));
	}

	public static void Register(this TMP_Dropdown dropdown, UnityAction<int> func)
	{
		dropdown.onValueChanged.RemoveAllListeners();
		dropdown.onValueChanged.AddListener(func);
	}

	public static void Register(this TMP_InputField input, UnityAction<string> func)
	{
		input.onValueChanged.RemoveAllListeners();
		input.onValueChanged.AddListener(func);
	}

	public static async void AnimateNumber(this TextMeshProUGUI text, float maxScore, int ms = 1000)
	{
		if (text == null) return;
		float increase = maxScore / (ms / 10f);
		for (float score = 0; score <= maxScore; score += increase)
		{
			text.text = score.ToString("0");
			await Task.Delay(10);
		}
		text.text = maxScore.ToString("0");
	}

	public static async void AnimateNumber(this Text text, float maxScore, int ms = 1000)
	{
		float increase = maxScore / (ms / 10f);
		for (float score = 0; score <= maxScore; score += increase)
		{
			if (!text) return;
			text.text = score.ToString("0");
			await Task.Delay(10);
		}
		text.text = maxScore.ToString("0");
	}
}
