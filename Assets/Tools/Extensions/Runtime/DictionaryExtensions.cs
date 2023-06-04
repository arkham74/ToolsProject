using System.Collections.Generic;

namespace JD
{
	public static class DictionaryExtensions
	{
		public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue add)
		{
			if (dictionary.TryGetValue(key, out TValue value))
			{
				return value;
			}
			dictionary.Add(key, add);
			return add;
		}
	}
}