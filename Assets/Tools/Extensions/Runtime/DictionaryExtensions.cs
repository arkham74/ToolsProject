using System.Collections.Generic;

namespace JD
{
	public static class DictionaryExtensions
	{
		public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
		{
			if (dictionary.TryGetValue(key, out TValue value))
			{
				return value;
			}
			dictionary.Add(key, defaultValue);
			return defaultValue;
		}
	}
}