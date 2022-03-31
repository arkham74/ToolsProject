using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Steamworks;
using UnityEngine;

public static class FileBasedPrefs
{
	private const string SAVE_FILE_NAME = "profile.save";
	private const bool SCRAMBLE_SAVE_DATA = true;
	private const string ENCRYPTION_CODEWORD = "UJG2x0JetphqSmaPIPOKrKl";
	private const bool AUTO_SAVE_DATA = true;
	private const string STRING_EMPTY = "";

	private static FileBasedPrefsSaveFileModel latestData;
	private static readonly StringBuilder StringBuilder = new StringBuilder();

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Reload()
	{
		latestData = null;
	}

	#region Public Get, Set and util

	public static void SetString(string key, string value = STRING_EMPTY)
	{
		AddDataToSaveFile(key, value);
	}

	public static string GetString(string key, string defaultValue = STRING_EMPTY)
	{
		return (string)GetDataFromSaveFile(key, defaultValue);
	}

	public static void SetInt(string key, int value = default)
	{
		AddDataToSaveFile(key, value);
	}

	public static int GetInt(string key, int defaultValue = default)
	{
		return (int)GetDataFromSaveFile(key, defaultValue);
	}

	public static void SetFloat(string key, float value = default)
	{
		AddDataToSaveFile(key, value);
	}

	public static float GetFloat(string key, float defaultValue = default)
	{
		return (float)GetDataFromSaveFile(key, defaultValue);
	}

	public static string GetSteamUserPath()
	{
		string playerId = "UnknownPlayer";

		if (SteamClient.IsValid) playerId = SteamClient.SteamId.ToString();

		string path = Path.Combine(Application.persistentDataPath, playerId);

		if (!Directory.Exists(path)) Directory.CreateDirectory(path);

		return path;
	}

	public static void SetBool(string key, bool value = default)
	{
		AddDataToSaveFile(key, value);
	}

	public static bool GetBool(string key, bool defaultValue = default)
	{
		return (bool)GetDataFromSaveFile(key, defaultValue);
	}

	public static bool HasKey(string key)
	{
		return GetSaveFile().HasKey(key);
	}

	public static bool HasKeyForString(string key)
	{
		return GetSaveFile().HasKeyFromObject(key, string.Empty);
	}

	public static bool HasKeyForInt(string key)
	{
		return GetSaveFile().HasKeyFromObject(key, default(int));
	}

	public static bool HasKeyForFloat(string key)
	{
		return GetSaveFile().HasKeyFromObject(key, default(float));
	}

	public static bool HasKeyForBool(string key)
	{
		return GetSaveFile().HasKeyFromObject(key, default(bool));
	}

	public static void DeleteKey(string key)
	{
		GetSaveFile().DeleteKey(key);
		SaveSaveFile();
	}

	public static void DeleteString(string key)
	{
		GetSaveFile().DeleteString(key);
		SaveSaveFile();
	}

	public static void DeleteInt(string key)
	{
		GetSaveFile().DeleteInt(key);
		SaveSaveFile();
	}

	public static void DeleteFloat(string key)
	{
		GetSaveFile().DeleteFloat(key);
		SaveSaveFile();
	}

	public static void DeleteBool(string key)
	{
		GetSaveFile().DeleteBool(key);
		SaveSaveFile();
	}

	public static void DeleteAll()
	{
		latestData = new FileBasedPrefsSaveFileModel();
		WriteToSaveFile(JsonUtility.ToJson(latestData));
	}

	public static void OverwriteLocalSaveFile(string data)
	{
		WriteToSaveFile(data);
		latestData = null;
	}

	#endregion

	#region Read data

	public static FileBasedPrefsSaveFileModel GetSaveFile()
	{
		CheckSaveFileExists();
		if (latestData != null) return latestData;
		string saveFileText = File.ReadAllText(GetSaveFilePath());

		if (SCRAMBLE_SAVE_DATA) saveFileText = DataScrambler(saveFileText);

		try
		{
			latestData = JsonUtility.FromJson<FileBasedPrefsSaveFileModel>(saveFileText);
		}
		catch (ArgumentException e)
		{
			Debug.LogException(new Exception("SAVE FILE IN WRONG FORMAT, CREATING NEW SAVE FILE : " + e.Message));
			DeleteAll();
		}

		return latestData;
	}

	public static string GetSaveFilePath()
	{
		return Path.Combine(GetSteamUserPath(), SAVE_FILE_NAME);
	}

	public static string GetSaveFileAsJson()
	{
		CheckSaveFileExists();
		return File.ReadAllText(GetSaveFilePath());
	}

	private static object GetDataFromSaveFile(string key, object defaultValue)
	{
		return GetSaveFile().GetValueFromKey(key, defaultValue);
	}

	#endregion

	#region write data

	private static void AddDataToSaveFile(string key, object value)
	{
		GetSaveFile().UpdateOrAddData(key, value);
		SaveSaveFile();
	}

	public static void ManualSave()
	{
		SaveSaveFile(true);
	}

	private static void SaveSaveFile(bool manualSave = false)
	{
		if (AUTO_SAVE_DATA || manualSave)
		{
			WriteToSaveFile(JsonUtility.ToJson(GetSaveFile()));
		}
	}

	private static void WriteToSaveFile(string data)
	{
		StreamWriter tw = new StreamWriter(GetSaveFilePath());
		if (SCRAMBLE_SAVE_DATA)
		{
			data = DataScrambler(data);
		}

		tw.Write(data);
		tw.Close();
	}

	#endregion

	#region File Utils

	private static void CheckSaveFileExists()
	{
		if (!DoesSaveFileExist())
		{
			CreateNewSaveFile();
		}
	}

	private static bool DoesSaveFileExist()
	{
		return File.Exists(GetSaveFilePath());
	}

	private static void CreateNewSaveFile()
	{
		WriteToSaveFile(JsonUtility.ToJson(new FileBasedPrefsSaveFileModel()));
	}

	public static string DataScrambler(string data)
	{
		StringBuilder.Clear();

		for (int i = 0; i < data.Length; i++)
		{
			StringBuilder.Append((char)(data[i] ^ ENCRYPTION_CODEWORD[i % ENCRYPTION_CODEWORD.Length]));
		}

		return StringBuilder.ToString();
	}

	#endregion
}