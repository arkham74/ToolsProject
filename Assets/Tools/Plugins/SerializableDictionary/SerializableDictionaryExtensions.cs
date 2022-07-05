using System;

namespace SerializableDictionary
{
	public static class SerializableDictionaryExtensions
	{
		public static void ForEach<K, V>(this SerializableDictionary<K, V> array, Action<K, V> action)
		{
			foreach ((K k, V v) in array)
			{
				action(k, v);
			}
		}
	}
}