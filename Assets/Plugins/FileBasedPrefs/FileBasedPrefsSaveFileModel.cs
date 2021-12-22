using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

[Serializable]
public class FileBasedPrefsSaveFileModel
{
	public StringItem[] stringData = Array.Empty<StringItem>();
	public IntItem[] intData = Array.Empty<IntItem>();
	public FloatItem[] floatData = Array.Empty<FloatItem>();
	public BoolItem[] boolData = Array.Empty<BoolItem>();

	[Serializable]
	public class StringItem
	{
		public string key;
		public string value;

		public StringItem(string k, string v)
		{
			key = k;
			value = v;
		}
	}

	[Serializable]
	public class IntItem
	{
		[FormerlySerializedAs("Key")] public string key;
		[FormerlySerializedAs("Value")] public int value;

		public IntItem(string k, int v)
		{
			key = k;
			value = v;
		}
	}

	[Serializable]
	public class FloatItem
	{
		[FormerlySerializedAs("Key")] public string key;
		[FormerlySerializedAs("Value")] public float value;

		public FloatItem(string k, float v)
		{
			key = k;
			value = v;
		}
	}

	[Serializable]
	public class BoolItem
	{
		[FormerlySerializedAs("Key")] public string key;
		[FormerlySerializedAs("Value")] public bool value;

		public BoolItem(string k, bool v)
		{
			key = k;
			value = v;
		}
	}

	public object GetValueFromKey(string key, object defaultValue)
	{
		if (defaultValue is string)
		{
			for (int i = 0; i < stringData.Length; i++)
			{
				if (stringData[i].key.Equals(key))
				{
					return stringData[i].value;
				}
			}
		}

		if (defaultValue is int)
		{
			for (int i = 0; i < intData.Length; i++)
			{
				if (intData[i].key.Equals(key))
				{
					return intData[i].value;
				}
			}
		}

		if (defaultValue is float)
		{
			for (int i = 0; i < floatData.Length; i++)
			{
				if (floatData[i].key.Equals(key))
				{
					return floatData[i].value;
				}
			}
		}

		if (defaultValue is bool)
		{
			for (int i = 0; i < boolData.Length; i++)
			{
				if (boolData[i].key.Equals(key))
				{
					return boolData[i].value;
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
			List<StringItem> dataAsList = stringData.ToList();
			dataAsList.Add(new StringItem(key, stringValue));
			stringData = dataAsList.ToArray();
		}

		if (value is int intValue)
		{
			List<IntItem> dataAsList = intData.ToList();
			dataAsList.Add(new IntItem(key, intValue));
			intData = dataAsList.ToArray();
		}

		if (value is float floatValue)
		{
			List<FloatItem> dataAsList = floatData.ToList();
			dataAsList.Add(new FloatItem(key, floatValue));
			floatData = dataAsList.ToArray();
		}

		if (value is bool boolValue)
		{
			List<BoolItem> dataAsList = boolData.ToList();
			dataAsList.Add(new BoolItem(key, boolValue));
			boolData = dataAsList.ToArray();
		}
	}

	private void SetValueForExistingKey(string key, object value)
	{
		if (value is string stringValue)
		{
			for (int i = 0; i < stringData.Length; i++)
			{
				if (stringData[i].key.Equals(key))
				{
					stringData[i].value = stringValue;
				}
			}
		}

		if (value is int intValue)
		{
			for (int i = 0; i < intData.Length; i++)
			{
				if (intData[i].key.Equals(key))
				{
					intData[i].value = intValue;
				}
			}
		}

		if (value is float floatValue)
		{
			for (int i = 0; i < floatData.Length; i++)
			{
				if (floatData[i].key.Equals(key))
				{
					floatData[i].value = floatValue;
				}
			}
		}

		if (value is bool boolValue)
		{
			for (int i = 0; i < boolData.Length; i++)
			{
				if (boolData[i].key.Equals(key))
				{
					boolData[i].value = boolValue;
				}
			}
		}
	}

	public bool HasKeyFromObject(string key, object value)
	{
		if (value is string)
		{
			for (int i = 0; i < stringData.Length; i++)
			{
				if (stringData[i].key.Equals(key))
				{
					return true;
				}
			}
		}

		if (value is int)
		{
			for (int i = 0; i < intData.Length; i++)
			{
				if (intData[i].key.Equals(key))
				{
					return true;
				}
			}
		}

		if (value is float)
		{
			for (int i = 0; i < floatData.Length; i++)
			{
				if (floatData[i].key.Equals(key))
				{
					return true;
				}
			}
		}

		if (value is bool)
		{
			for (int i = 0; i < boolData.Length; i++)
			{
				if (boolData[i].key.Equals(key))
				{
					return true;
				}
			}
		}

		return false;
	}

	public void DeleteKey(string key)
	{
		for (int i = 0; i < stringData.Length; i++)
		{
			if (stringData[i].key.Equals(key))
			{
				List<StringItem> dataAsList = stringData.ToList();
				dataAsList.RemoveAt(i);
				stringData = dataAsList.ToArray();
			}
		}

		for (int i = 0; i < intData.Length; i++)
		{
			if (intData[i].key.Equals(key))
			{
				List<IntItem> dataAsList = intData.ToList();
				dataAsList.RemoveAt(i);
				intData = dataAsList.ToArray();
			}
		}

		for (int i = 0; i < floatData.Length; i++)
		{
			if (floatData[i].key.Equals(key))
			{
				List<FloatItem> dataAsList = floatData.ToList();
				dataAsList.RemoveAt(i);
				floatData = dataAsList.ToArray();
			}
		}

		for (int i = 0; i < boolData.Length; i++)
		{
			if (boolData[i].key.Equals(key))
			{
				List<BoolItem> dataAsList = boolData.ToList();
				dataAsList.RemoveAt(i);
				boolData = dataAsList.ToArray();
			}
		}
	}

	public void DeleteString(string key)
	{
		for (int i = 0; i < stringData.Length; i++)
		{
			if (stringData[i].key.Equals(key))
			{
				List<StringItem> dataAsList = stringData.ToList();
				dataAsList.RemoveAt(i);
				stringData = dataAsList.ToArray();
			}
		}
	}

	public void DeleteInt(string key)
	{
		for (int i = 0; i < intData.Length; i++)
		{
			if (intData[i].key.Equals(key))
			{
				List<IntItem> dataAsList = intData.ToList();
				dataAsList.RemoveAt(i);
				intData = dataAsList.ToArray();
			}
		}
	}

	public void DeleteFloat(string key)
	{
		for (int i = 0; i < floatData.Length; i++)
		{
			if (floatData[i].key.Equals(key))
			{
				List<FloatItem> dataAsList = floatData.ToList();
				dataAsList.RemoveAt(i);
				floatData = dataAsList.ToArray();
			}
		}
	}

	public void DeleteBool(string key)
	{
		for (int i = 0; i < boolData.Length; i++)
		{
			if (boolData[i].key.Equals(key))
			{
				List<BoolItem> dataAsList = boolData.ToList();
				dataAsList.RemoveAt(i);
				boolData = dataAsList.ToArray();
			}
		}
	}

	public bool HasKey(string key)
	{
		for (int i = 0; i < stringData.Length; i++)
		{
			if (stringData[i].key.Equals(key))
			{
				return true;
			}
		}

		for (int i = 0; i < intData.Length; i++)
		{
			if (intData[i].key.Equals(key))
			{
				return true;
			}
		}

		for (int i = 0; i < floatData.Length; i++)
		{
			if (floatData[i].key.Equals(key))
			{
				return true;
			}
		}

		for (int i = 0; i < boolData.Length; i++)
		{
			if (boolData[i].key.Equals(key))
			{
				return true;
			}
		}

		return false;
	}
}