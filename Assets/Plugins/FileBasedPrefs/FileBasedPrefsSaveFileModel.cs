using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

[Serializable]
public class FileBasedPrefsSaveFileModel
{
	public StringItem[] StringData = Array.Empty<StringItem>();
	public IntItem[] IntData = Array.Empty<IntItem>();
	public FloatItem[] FloatData = Array.Empty<FloatItem>();
	public BoolItem[] BoolData = Array.Empty<BoolItem>();

	[Serializable]
	public class StringItem
	{
		public string Key;
		public string Value;

		public StringItem(string k, string v)
		{
			Key = k;
			Value = v;
		}
	}

	[Serializable]
	public class IntItem
	{
		public string Key;
		public int Value;

		public IntItem(string k, int v)
		{
			Key = k;
			Value = v;
		}
	}

	[Serializable]
	public class FloatItem
	{
		public string Key;
		public float Value;

		public FloatItem(string k, float v)
		{
			Key = k;
			Value = v;
		}
	}

	[Serializable]
	public class BoolItem
	{
		public string Key;
		public bool Value;

		public BoolItem(string k, bool v)
		{
			Key = k;
			Value = v;
		}
	}

	public object GetValueFromKey(string key, object defaultValue)
	{
		if (defaultValue is string)
		{
			for (int i = 0; i < StringData.Length; i++)
			{
				if (StringData[i].Key.Equals(key))
				{
					return StringData[i].Value;
				}
			}
		}

		if (defaultValue is int)
		{
			for (int i = 0; i < IntData.Length; i++)
			{
				if (IntData[i].Key.Equals(key))
				{
					return IntData[i].Value;
				}
			}
		}

		if (defaultValue is float)
		{
			for (int i = 0; i < FloatData.Length; i++)
			{
				if (FloatData[i].Key.Equals(key))
				{
					return FloatData[i].Value;
				}
			}
		}

		if (defaultValue is bool)
		{
			for (int i = 0; i < BoolData.Length; i++)
			{
				if (BoolData[i].Key.Equals(key))
				{
					return BoolData[i].Value;
				}
			}
		}

		return defaultValue;
	}

	public void UpdateOrAddData(string key, object value)
	{
		if (HasKeyFromObject(key, value))
		{
			SetValueForExistingKey(key, value);
		}
		else
		{
			SetValueForNewKey(key, value);
		}
	}

	private void SetValueForNewKey(string key, object value)
	{
		if (value is string stringValue)
		{
			List<StringItem> dataAsList = StringData.ToList();
			dataAsList.Add(new StringItem(key, stringValue));
			StringData = dataAsList.ToArray();
		}

		if (value is int intValue)
		{
			List<IntItem> dataAsList = IntData.ToList();
			dataAsList.Add(new IntItem(key, intValue));
			IntData = dataAsList.ToArray();
		}

		if (value is float floatValue)
		{
			List<FloatItem> dataAsList = FloatData.ToList();
			dataAsList.Add(new FloatItem(key, floatValue));
			FloatData = dataAsList.ToArray();
		}

		if (value is bool boolValue)
		{
			List<BoolItem> dataAsList = BoolData.ToList();
			dataAsList.Add(new BoolItem(key, boolValue));
			BoolData = dataAsList.ToArray();
		}
	}

	private void SetValueForExistingKey(string key, object value)
	{
		if (value is string stringValue)
		{
			for (int i = 0; i < StringData.Length; i++)
			{
				if (StringData[i].Key.Equals(key))
				{
					StringData[i].Value = stringValue;
				}
			}
		}

		if (value is int intValue)
		{
			for (int i = 0; i < IntData.Length; i++)
			{
				if (IntData[i].Key.Equals(key))
				{
					IntData[i].Value = intValue;
				}
			}
		}

		if (value is float floatValue)
		{
			for (int i = 0; i < FloatData.Length; i++)
			{
				if (FloatData[i].Key.Equals(key))
				{
					FloatData[i].Value = floatValue;
				}
			}
		}

		if (value is bool boolValue)
		{
			for (int i = 0; i < BoolData.Length; i++)
			{
				if (BoolData[i].Key.Equals(key))
				{
					BoolData[i].Value = boolValue;
				}
			}
		}
	}

	public bool HasKeyFromObject(string key, object value)
	{
		if (value is string)
		{
			for (int i = 0; i < StringData.Length; i++)
			{
				if (StringData[i].Key.Equals(key))
				{
					return true;
				}
			}
		}

		if (value is int)
		{
			for (int i = 0; i < IntData.Length; i++)
			{
				if (IntData[i].Key.Equals(key))
				{
					return true;
				}
			}
		}

		if (value is float)
		{
			for (int i = 0; i < FloatData.Length; i++)
			{
				if (FloatData[i].Key.Equals(key))
				{
					return true;
				}
			}
		}

		if (value is bool)
		{
			for (int i = 0; i < BoolData.Length; i++)
			{
				if (BoolData[i].Key.Equals(key))
				{
					return true;
				}
			}
		}

		return false;
	}

	public void DeleteKey(string key)
	{
		for (int i = 0; i < StringData.Length; i++)
		{
			if (StringData[i].Key.Equals(key))
			{
				List<StringItem> dataAsList = StringData.ToList();
				dataAsList.RemoveAt(i);
				StringData = dataAsList.ToArray();
			}
		}

		for (int i = 0; i < IntData.Length; i++)
		{
			if (IntData[i].Key.Equals(key))
			{
				List<IntItem> dataAsList = IntData.ToList();
				dataAsList.RemoveAt(i);
				IntData = dataAsList.ToArray();
			}
		}

		for (int i = 0; i < FloatData.Length; i++)
		{
			if (FloatData[i].Key.Equals(key))
			{
				List<FloatItem> dataAsList = FloatData.ToList();
				dataAsList.RemoveAt(i);
				FloatData = dataAsList.ToArray();
			}
		}

		for (int i = 0; i < BoolData.Length; i++)
		{
			if (BoolData[i].Key.Equals(key))
			{
				List<BoolItem> dataAsList = BoolData.ToList();
				dataAsList.RemoveAt(i);
				BoolData = dataAsList.ToArray();
			}
		}
	}

	public void DeleteString(string key)
	{
		for (int i = 0; i < StringData.Length; i++)
		{
			if (StringData[i].Key.Equals(key))
			{
				List<StringItem> dataAsList = StringData.ToList();
				dataAsList.RemoveAt(i);
				StringData = dataAsList.ToArray();
			}
		}
	}

	public void DeleteInt(string key)
	{
		for (int i = 0; i < IntData.Length; i++)
		{
			if (IntData[i].Key.Equals(key))
			{
				List<IntItem> dataAsList = IntData.ToList();
				dataAsList.RemoveAt(i);
				IntData = dataAsList.ToArray();
			}
		}
	}

	public void DeleteFloat(string key)
	{
		for (int i = 0; i < FloatData.Length; i++)
		{
			if (FloatData[i].Key.Equals(key))
			{
				List<FloatItem> dataAsList = FloatData.ToList();
				dataAsList.RemoveAt(i);
				FloatData = dataAsList.ToArray();
			}
		}
	}

	public void DeleteBool(string key)
	{
		for (int i = 0; i < BoolData.Length; i++)
		{
			if (BoolData[i].Key.Equals(key))
			{
				List<BoolItem> dataAsList = BoolData.ToList();
				dataAsList.RemoveAt(i);
				BoolData = dataAsList.ToArray();
			}
		}
	}

	public bool HasKey(string key)
	{
		for (int i = 0; i < StringData.Length; i++)
		{
			if (StringData[i].Key.Equals(key))
			{
				return true;
			}
		}

		for (int i = 0; i < IntData.Length; i++)
		{
			if (IntData[i].Key.Equals(key))
			{
				return true;
			}
		}

		for (int i = 0; i < FloatData.Length; i++)
		{
			if (FloatData[i].Key.Equals(key))
			{
				return true;
			}
		}

		for (int i = 0; i < BoolData.Length; i++)
		{
			if (BoolData[i].Key.Equals(key))
			{
				return true;
			}
		}

		return false;
	}
}