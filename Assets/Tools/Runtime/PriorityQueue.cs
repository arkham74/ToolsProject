using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions;

namespace JD
{
	public class PriorityQueue<T> : IEnumerable<T>
	{
		private List<T> items = new List<T>();
		private List<float> priorities = new List<float>();

		public int Count => items.Count;
		public int Length => items.Count;
		public bool IsEmpty => items.Count == 0;

		public void Enqueue(T item, float priority)
		{
			for (int i = 0; i < priorities.Count; i++)
			{
				if (priorities[i].CompareTo(priority) > 0)
				{
					items.Insert(i, item);
					priorities.Insert(i, priority);
					return;
				}
			}
			items.Add(item);
			priorities.Add(priority);
		}

		public T Dequeue()
		{
			Debug.Assert(items.Count > 0);
			Debug.Assert(priorities.Count > 0);
			T item = items[0];
			items.RemoveAt(0);
			priorities.RemoveAt(0);
			return item;
		}

		public void Clear()
		{
			items.Clear();
			priorities.Clear();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}