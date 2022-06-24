using System.Text;
using System.IO;
using UnityEngine;
using System;
using UnityEngine.Events;

public static class FileBasedPrefs
{
	private class FBPPInitException : Exception
	{
		public FBPPInitException(string message) : base(message) { }
	}

	private const string INIT_EXCEPTION_MESSAGE = "Error, you must call FBPP.Start(FBPPConfig config) before trying to get or set saved data.";
	public static bool ShowInitWarning = true;

	private static FileBasedPrefsConfig _config;
	private static FileBasedPrefsSaveFileModel _latestData;
	private static readonly StringBuilder _sb = new StringBuilder();
	private const string String_Empty = "";

	public static bool AutoSaveData
	{
		get => _config.AutoSaveData;
		set => _config.AutoSaveData = value;
	}

	#region Init

	public static void Init(string name, string secret, string user)
	{
		string path = Path.Combine(Application.persistentDataPath, user);
		Directory.CreateDirectory(path);
		FileBasedPrefsConfig config = new FileBasedPrefsConfig()
		{
			SaveFileName = name,
			EncryptionSecret = secret,
			SaveFilePath = path,
		};
		Start(config);
	}

	public static void Start(FileBasedPrefsConfig config)
	{
		_config = config;
		_latestData = GetSaveFile();
	}

	public static bool IsInit()
	{
		return _config != null;
	}

	private static void CheckForInit()
	{
		if (_config == null)
		{
			throw new FBPPInitException(INIT_EXCEPTION_MESSAGE);
		}
	}

	#endregion


	#region Public Get, Set and util

	public static void SetString(string key, string value = String_Empty)
	{
		AddDataToSaveFile(key, value);
	}

	public static string GetString(string key, string defaultValue = String_Empty)
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
		WriteToSaveFile(JsonUtility.ToJson(new FileBasedPrefsSaveFileModel()));
		_latestData = new FileBasedPrefsSaveFileModel();
	}

	public static void OverwriteLocalSaveFile(string data)
	{
		WriteToSaveFile(data);
		_latestData = null;
		_latestData = GetSaveFile();
	}


	#endregion



	#region Read data

	public static FileBasedPrefsSaveFileModel GetSaveFile()
	{
		CheckForInit();
		CheckSaveFileExists();
		if (_latestData == null)
		{
			var path = GetSaveFilePath();

			File.Copy(path, $"{path}_backup_{DateTime.Now:yyyy_MM_dd}", true);

			var saveFileText = File.ReadAllText(path);

			if (_config.ScrambleSaveData)
			{
				saveFileText = DataScrambler(saveFileText);
			}
			try
			{
				_latestData = JsonUtility.FromJson<FileBasedPrefsSaveFileModel>(saveFileText);
			}
			catch (ArgumentException e)
			{
				Debug.LogException(new Exception("FBPP Error loading save file: " + e.Message));
				DeleteAll();
			}
		}
		return _latestData;
	}

	public static string GetSaveFilePath()
	{
		CheckForInit();
		return Path.Combine(_config.GetSaveFilePath(), _config.SaveFileName);
	}

	public static string GetSteamUserPath()
	{
		CheckForInit();
		return _config.GetSaveFilePath();
	}

	public static string GetSaveFileAsJson()
	{
		CheckForInit();
		CheckSaveFileExists();
		return JsonUtility.ToJson(GetSaveFile());
	}

	private static object GetDataFromSaveFile(string key, object defaultValue)
	{
		return GetSaveFile().GetValueForKey(key, defaultValue);
	}

	#endregion


	#region write data

	private static void AddDataToSaveFile(string key, object value)
	{
		CheckForInit();
		GetSaveFile().UpdateOrAddData(key, value);
		SaveSaveFile();
	}

	public static void Save()
	{
		CheckForInit();
		SaveSaveFile(true);
	}

	private static void SaveSaveFile(bool manualSave = false)
	{
		if (_config.AutoSaveData || manualSave)
		{
			WriteToSaveFile(JsonUtility.ToJson(GetSaveFile()));
		}
	}
	private static void WriteToSaveFile(string data)
	{
		var tw = new StreamWriter(GetSaveFilePath());
		if (_config.ScrambleSaveData)
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
		_sb.Clear();

		for (int i = 0; i < data.Length; i++)
		{
			_sb.Append((char)(data[i] ^ _config.EncryptionSecret[i % _config.EncryptionSecret.Length]));
		}
		return _sb.ToString();
	}

	#endregion
}


