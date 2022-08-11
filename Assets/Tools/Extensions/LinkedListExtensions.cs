using System.Collections.Generic;

namespace JD
{
	public static class LinkedListExtensions
	{
		public static T Peek<T>(this LinkedList<T> list)
		{
			return list.First.Value;
		}

		public static void Enqueue<T>(this LinkedList<T> list, T element)
		{
			list.AddLast(element);
		}

		public static T Dequeue<T>(this LinkedList<T> list)
		{
			T element = list.First.Value;
			list.RemoveFirst();
			return element;
		}
	}
}