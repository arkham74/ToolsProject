using System;
using System.Collections;
using System.Collections.Generic;

namespace JD.Pathfind
{
	internal class AStarDictionary<TKey, TValue>
	{
		private readonly List<TKey> keys = new List<TKey>();
		private readonly List<TValue> values = new List<TValue>();

		TValue this[TKey key]
		{
			get => Get(key);
			set => Add(key, value);
		}

		public TValue Get(TKey key)
		{
			return values[keys.IndexOf(key)];
		}

		public void Clear()
		{
			keys.Clear();
			values.Clear();
		}

		public void Add(TKey key, TValue value)
		{
			keys.Add(key);
			values.Add(value);
		}

		public bool ContainsKey(TKey key)
		{
			return keys.Contains(key);
		}
	}
}