using System;
using UnityEditor;
using UnityEngine;
// #if UNITY_EDITOR
// using Prefs = UnityEngine.PlayerPrefs;
// #else
using Prefs = FileBasedPrefs;
// #endif

namespace JD
{
	public static class PP
	{
		public static void DeleteKey(string key) => Prefs.DeleteKey(key);

		// #if UNITY_EDITOR
		// 		public static bool GetBool(string key, bool defaultValue) => Convert.ToBoolean(Prefs.GetInt(key, Convert.ToInt32(defaultValue)));
		// 		public static void SetBool(string key, bool value) => Prefs.SetInt(key, Convert.ToInt32(value));
		// #else
		public static bool GetBool(string key, bool defaultValue) => Prefs.GetBool(key, defaultValue);
		public static void SetBool(string key, bool value) => Prefs.SetBool(key, value);
		// #endif

		public static int GetInt(string key, int defaultValue) => Prefs.GetInt(key, defaultValue);
		public static void SetInt(string key, int value) => Prefs.SetInt(key, value);

		public static float GetFloat(string key, float defaultValue) => Prefs.GetFloat(key, defaultValue);
		public static void SetFloat(string key, float value) => Prefs.SetFloat(key, value);

		public static string GetString(string key, string defaultValue) => Prefs.GetString(key, defaultValue);
		public static void SetString(string key, string value) => Prefs.SetString(key, value);
	}

	[Serializable]
	public abstract class SaveVar<T>
	{
		[SerializeField] protected string key;
		[SerializeField] protected T defaultValue;
		public abstract T Value { get; set; }

		protected SaveVar(string key, T defaultValue = default)
		{
			this.key = key;
			this.defaultValue = defaultValue;
		}

		public void Clear()
		{
			PP.DeleteKey(key);
		}

		public static implicit operator T(SaveVar<T> val) => val.Value;
	}

	public class SaveColor : SaveVar<Color>
	{
		private readonly string defaultJson;

		public SaveColor(string key, Color defaultValue = default) : base(key, defaultValue)
		{
			defaultJson = JsonTools.ToJson(defaultValue);
		}

		public override Color Value
		{
			get => JsonTools.FromJson<Color>(PP.GetString(key, defaultJson))[0];
			set => PP.SetString(key, JsonTools.ToJson(value));
		}
	}

	[Serializable]
	public class SaveBool : SaveVar<bool>
	{
		public override bool Value
		{
			get => PP.GetBool(key, defaultValue);
			set => PP.SetBool(key, value);
		}

		public SaveBool(string key, bool defaultValue = default) : base(key, defaultValue)
		{
		}
	}

	public class SaveInt : SaveVar<int>
	{
		public override int Value
		{
			get => PP.GetInt(key, defaultValue);
			set => PP.SetInt(key, value);
		}

		public SaveInt(string key, int defaultValue = default) : base(key, defaultValue)
		{
		}

		public static implicit operator SaveInt((string key, int def) param) => new SaveInt(param.key, param.def);
	}

	public class SaveFloat : SaveVar<float>
	{
		public override float Value
		{
			get => PP.GetFloat(key, defaultValue);
			set => PP.SetFloat(key, value);
		}

		public SaveFloat(string key, float defaultValue = default) : base(key, defaultValue)
		{
		}

		public static implicit operator SaveFloat((string key, float def) param) => new SaveFloat(param.key, param.def);
	}

	public class SaveString : SaveVar<string>
	{
		public override string Value
		{
			get => PP.GetString(key, defaultValue);
			set => PP.SetString(key, value);
		}

		public SaveString(string key, string defaultValue = default) : base(key, defaultValue)
		{
		}
	}
}