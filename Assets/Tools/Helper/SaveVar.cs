using System;
using UnityEditor;
using UnityEngine;
using PP = FileBasedPrefs;

// ReSharper disable MemberCanBePrivate.Global

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
		defaultJson = JsonHelper.ToJson(defaultValue);
	}

	public override Color Value
	{
		get => JsonHelper.FromJson<Color>(PP.GetString(key, defaultJson))[0];
		set => PP.SetString(key, JsonHelper.ToJson(value));
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