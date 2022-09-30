using System;
using System.Collections.Generic;
using UnityEngine;

namespace JD
{
	public class PriorityQueue<T, P> where P : IComparable
	{
		private class Cell : IComparable<Cell>
		{
			public T item;
			private P priority;

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
			list.Add(new Cell(item, priority));
			list.Sort();
		}

		public T Dequeue()
		{
			Debug.Assert(list.Count > 0);

			Cell first = list[0];
			list.Remove(first);
			return first.item;
		}
	}
}