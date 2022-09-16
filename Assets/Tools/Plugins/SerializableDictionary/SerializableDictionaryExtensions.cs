using System;
using System.Collections.Generic;

namespace SD
{
	public static class SerializableDictionaryExtensions
	{
		public static void ForEach<T1, T2>(this SerializableDictionary<T1, T2> array, Action<T1, T2> action)
		{
			foreach (KeyValuePair<T1, T2> item in array)
			{
				action(item.Key, item.Value);
			}
		}

		public static void ForEach<T1, T2>(this SerializableDictionary<T1, T2> array, Action<(T1 key, T2 value)> action)
		{
			foreach (KeyValuePair<T1, T2> item in array)
			{
				action((item.Key, item.Value));
			}
		}

		public static void ForEachKey<T1, T2>(this SerializableDictionary<T1, T2> array, Action<T1> action)
		{
			foreach (KeyValuePair<T1, T2> item in array)
			{
				action(item.Key);
			}
		}

		public static void ForEachValue<T1, T2>(this SerializableDictionary<T1, T2> array, Action<T2> action)
		{
			foreach (KeyValuePair<T1, T2> item in array)
			{
				action(item.Value);
			}
		}
	}
}