using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace JD
{
	public class PriorityQueue<T, P> : IEnumerable<T> where P : IComparable
	{
		private class Cell : IComparable<Cell>
		{
			public T item;
			public P priority;

			public Cell(T item, P priority)
			{
				this.item = item;
				this.priority = priority;
			}

			public int CompareTo(Cell other)
			{
				return priority.CompareTo(other.priority);
			}
		}

		private List<Cell> list = new List<Cell>();
		public int Count => list.Count;
		public int Length => list.Count;
		public bool IsEmpty => list.Count == 0;

		public void Enqueue(T item, P priority)
		{
			Cell cell = new Cell(item, priority);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].priority.CompareTo(priority) > 0)
				{
					list.Insert(i, cell);
					return;
				}
			}
			list.Add(cell);
		}

		public T Dequeue()
		{
			Debug.Assert(list.Count > 0);
			Cell first = list[0];
			list.RemoveAt(0);
			return first.item;
		}

		public void Clear()
		{
			list.Clear();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return list.Select(e => e.item).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}