using System;
using UnityEditor;
using UnityEngine;
using PP = FileBasedPrefs;

// ReSharper disable MemberCanBePrivate.Global

[Serializable]
public abstract class SaveVar<T>
{
	[SerializeField] protected string key;

	protected T defaultValue;
	protected bool loaded;
	protected T internalValue;
	public abstract T Value { get; set; }

	protected SaveVar(string key, T defaultValue = default)
	{
		this.key = key;
		this.defaultValue = defaultValue;
	}

	public void Clear()
	{
		PP.DeleteKey(key);
		loaded = false;
	}
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
		set => PP.SetString(key, JsonHelper.ToJson(internalValue = value));
	}
}

[Serializable]
public class SaveBool : SaveVar<bool>
{
	public override bool Value
	{
		get
		{
			if (loaded == false)
			{
				internalValue = PP.GetBool(key, defaultValue);
				loaded = true;
			}

			return internalValue;
		}
		set => PP.SetBool(key, internalValue = value);
	}

	public SaveBool(string key, bool defaultValue = default) : base(key, defaultValue)
	{
	}
}

public class SaveInt : SaveVar<int>
{
	public override int Value
	{
		get
		{
			if (loaded == false)
			{
				internalValue = PP.GetInt(key, defaultValue);
				loaded = true;
			}

			return internalValue;
		}
		set => PP.SetInt(key, internalValue = value);
	}

	public SaveInt(string key, int defaultValue = default) : base(key, defaultValue)
	{
	}
}

public class SaveFloat : SaveVar<float>
{
	public override float Value
	{
		get
		{
			if (loaded == false)
			{
				internalValue = PP.GetFloat(key, defaultValue);
				loaded = true;
			}

			return internalValue;
		}
		set => PP.SetFloat(key, internalValue = value);
	}

	public SaveFloat(string key, float defaultValue = default) : base(key, defaultValue)
	{
	}
}

public class SaveString : SaveVar<string>
{
	public override string Value
	{
		get
		{
			if (loaded == false)
			{
				internalValue = PP.GetString(key, defaultValue);
				loaded = true;
			}

			return internalValue;
		}
		set => PP.SetString(key, internalValue = value);
	}

	public SaveString(string key, string defaultValue = default) : base(key, defaultValue)
	{
	}
}