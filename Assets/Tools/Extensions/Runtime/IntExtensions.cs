using System.Collections.Generic;
using System.Linq;
using Freya;
using UnityEngine;

namespace JD
{
	public static class IntExtensions
	{
		public readonly static Dictionary<int, string> intCache = new Dictionary<int, string>(10000);

		public static int Mod(this int index, int min, int max)
		{
			return (index - min).Mod(max + 1 - min) + min;
		}

		public static string IntArrayToString(this IReadOnlyList<int> intArray)
		{
			return string.Join(",", intArray);
		}

		// 	public static IOrderedEnumerable<int> Sort(this IEnumerable<int> array)
		// 	{
		// 		return array.OrderBy(e => e);
		// 	}

		public static int LayerToMask(this int layerIndex)
		{
			return Mathf.RoundToInt(Mathf.Pow(2, layerIndex));
		}

		public static int Random(this int value)
		{
			return UnityEngine.Random.Range(0, value);
		}

		public static string ToStringNonAllocation(this int value)
		{
			if (intCache.TryGetValue(value, out string str))
			{
				return str;
			}

			str = value.ToString();
			intCache.Add(value, str);
			return str;
		}
	}
}
