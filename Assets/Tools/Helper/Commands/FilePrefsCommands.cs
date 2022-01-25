using System;
using System.Text;
using IngameDebugConsole;
using UnityEngine;

#if UNITY_EDITOR || DEVELOPMENT_BUILD
public static class FilePrefsCommands
{
	[ConsoleMethod("fileprefs.list", "List all keys"), UnityEngine.Scripting.Preserve]
	public static void ListKeys()
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendLine("All keys:");
		string formatBool = "    - <b>{0} [bool]</b>: {1}\n";
		string formatInt = "    - <b>{0} [int]</b>: {1}\n";
		string formatFloat = "    - <b>{0} [float]</b>: {1}\n";
		string formatString = "    - <b>{0} [string]</b>: {1}\n";
		FileBasedPrefsSaveFileModel data = FileBasedPrefs.GetSaveFile();

		foreach (FileBasedPrefsSaveFileModel.BoolItem item in data.boolData)
		{
			sb.AppendFormat(formatBool, item.key, item.value);
		}

		foreach (FileBasedPrefsSaveFileModel.IntItem item in data.intData)
		{
			sb.AppendFormat(formatInt, item.key, item.value);
		}

		foreach (FileBasedPrefsSaveFileModel.FloatItem item in data.floatData)
		{
			sb.AppendFormat(formatFloat, item.key, item.value);
		}

		foreach (FileBasedPrefsSaveFileModel.StringItem item in data.stringData)
		{
			sb.AppendFormat(formatString, item.key, item.value);
		}

		Debug.Log(sb.ToString());

		if (DebugLogManager.Instance)
		{
			DebugLogManager.Instance.ExpandLatestPendingLog();
			DebugLogManager.Instance.StripStackTraceFromLatestPendingLog();
		}
	}

	[ConsoleMethod("fileprefs.set", "Set pref at key"), UnityEngine.Scripting.Preserve]
	public static void GetPref(string key, bool value)
	{
		FileBasedPrefs.SetBool(key, value);
		Debug.Log($"Bool value {value} set at key {key}");
	}

	[ConsoleMethod("fileprefs.set", "Set pref at key"), UnityEngine.Scripting.Preserve]
	public static void GetPref(string key, int value)
	{
		FileBasedPrefs.SetInt(key, value);
		Debug.Log($"Int value {value} set at key {key}");
	}

	[ConsoleMethod("fileprefs.set", "Set pref at key"), UnityEngine.Scripting.Preserve]
	public static void GetPref(string key, float value)
	{
		FileBasedPrefs.SetFloat(key, value);
		Debug.Log($"Float value {value} set at key {key}");
	}

	[ConsoleMethod("fileprefs.set", "Set pref at key"), UnityEngine.Scripting.Preserve]
	public static void GetPref(string key, string value)
	{
		FileBasedPrefs.SetString(key, value);
		Debug.Log($"String value {value} set at key {key}");
	}

	[ConsoleMethod("fileprefs.get", "Get pref at key"), UnityEngine.Scripting.Preserve]
	public static void GetPref(string key)
	{
		if (FileBasedPrefs.HasKeyForBool(key))
		{
			Debug.Log(FileBasedPrefs.GetBool(key));
			return;
		}

		if (FileBasedPrefs.HasKeyForInt(key))
		{
			Debug.Log(FileBasedPrefs.GetInt(key));
			return;
		}

		if (FileBasedPrefs.HasKeyForFloat(key))
		{
			Debug.Log(FileBasedPrefs.GetFloat(key));
			return;
		}

		if (FileBasedPrefs.HasKeyForString(key))
		{
			Debug.Log(FileBasedPrefs.GetString(key));
			return;
		}

		Debug.Log($"No value for key : {key}");
	}

	[ConsoleMethod("fileprefs.reset", "Reset player prefs"), UnityEngine.Scripting.Preserve]
	public static void ResetPlayerPrefs()
	{
		FileBasedPrefs.DeleteAll();
		FileBasedPrefs.ManualSave();
	}
}
#endif