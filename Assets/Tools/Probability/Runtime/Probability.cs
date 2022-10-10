using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JD
{
	[Serializable]
	public struct Probability<T>
	{
		[Serializable]
		public struct Percent : IComparable<Percent>
		{
			public T item;
			public float chance;

			public int CompareTo(Percent other)
			{
				return -chance.CompareTo(other.chance);
			}
		}

		[SerializeField] private List<Percent> elements;

		public T GetElement()
		{
			float total = elements.Sum(e => e.chance);
			float random = Random.value * total;
			float sum = 0;
			elements.Sort();
			foreach (Percent element in elements)
			{
				sum += element.chance;
				if (sum > random) return element.item;
			}

			throw new Exception("Did not find element in probability range");
		}
	}
}
