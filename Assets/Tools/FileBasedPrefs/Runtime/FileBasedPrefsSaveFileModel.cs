﻿using System;
using System.Linq;


[Serializable]
public class FileBasedPrefsSaveFileModel
{
	public StringItem[] StringData = new StringItem[0];
	public IntItem[] IntData = new IntItem[0];
	public FloatItem[] FloatData = new FloatItem[0];
	public BoolItem[] BoolData = new BoolItem[0];

	[Serializable]
	public class StringItem
	{
		public string Key;
		public string Value;

		public StringItem(string K, string V)
		{
			Key = K;
			Value = V;
		}
	}

	[Serializable]
	public class IntItem
	{
		public string Key;
		public int Value;

		public IntItem(string K, int V)
		{
			Key = K;
			Value = V;
		}
	}

	[Serializable]
	public class FloatItem
	{
		public string Key;
		public float Value;

		public FloatItem(string K, float V)
		{
			Key = K;
			Value = V;
		}
	}

	[Serializable]
	public class BoolItem
	{
		public string Key;
		public bool Value;

		public BoolItem(string K, bool V)
		{
			Key = K;
			Value = V;
		}
	}

	public object GetValueForKey(string key, object defaultValue)
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
		if (value is string @string)
		{
			var dataAsList = StringData.ToList();
			dataAsList.Add(new StringItem(key, @string));
			StringData = dataAsList.ToArray();
		}
		if (value is int @int)
		{
			var dataAsList = IntData.ToList();
			dataAsList.Add(new IntItem(key, @int));
			IntData = dataAsList.ToArray();
		}
		if (value is float single)
		{
			var dataAsList = FloatData.ToList();
			dataAsList.Add(new FloatItem(key, single));
			FloatData = dataAsList.ToArray();
		}
		if (value is bool boolean)
		{
			var dataAsList = BoolData.ToList();
			dataAsList.Add(new BoolItem(key, boolean));
			BoolData = dataAsList.ToArray();
		}
	}

	private void SetValueForExistingKey(string key, object value)
	{
		if (value is string @string)
		{
			for (int i = 0; i < StringData.Length; i++)
			{
				if (StringData[i].Key.Equals(key))
				{
					StringData[i].Value = @string;
				}
			}
		}
		if (value is int @int)
		{
			for (int i = 0; i < IntData.Length; i++)
			{
				if (IntData[i].Key.Equals(key))
				{
					IntData[i].Value = @int;
				}
			}
		}
		if (value is float single)
		{
			for (int i = 0; i < FloatData.Length; i++)
			{
				if (FloatData[i].Key.Equals(key))
				{
					FloatData[i].Value = single;
				}
			}
		}
		if (value is bool boolean)
		{
			for (int i = 0; i < BoolData.Length; i++)
			{
				if (BoolData[i].Key.Equals(key))
				{
					BoolData[i].Value = boolean;
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
				var dataAsList = StringData.ToList();
				dataAsList.RemoveAt(i);
				StringData = dataAsList.ToArray();
			}
		}
		for (int i = 0; i < IntData.Length; i++)
		{
			if (IntData[i].Key.Equals(key))
			{
				var dataAsList = IntData.ToList();
				dataAsList.RemoveAt(i);
				IntData = dataAsList.ToArray();
			}
		}
		for (int i = 0; i < FloatData.Length; i++)
		{
			if (FloatData[i].Key.Equals(key))
			{
				var dataAsList = FloatData.ToList();
				dataAsList.RemoveAt(i);
				FloatData = dataAsList.ToArray();
			}
		}
		for (int i = 0; i < BoolData.Length; i++)
		{
			if (BoolData[i].Key.Equals(key))
			{
				var dataAsList = BoolData.ToList();
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
				var dataAsList = StringData.ToList();
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
				var dataAsList = IntData.ToList();
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
				var dataAsList = FloatData.ToList();
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
				var dataAsList = BoolData.ToList();
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