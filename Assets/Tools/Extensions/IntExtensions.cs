using System.Collections.Generic;
using System.Linq;
using Freya;
using UnityEngine;

public static class IntExtensions
{
	public static string IntArrayToString(this IReadOnlyList<int> intArray)
	{
		return string.Join(",", intArray);
	}

	// 	public static IOrderedEnumerable<int> Sort(this IEnumerable<int> array)
	// 	{
	// 		return array.OrderBy(e => e);
	// 	}

	// 	// public static int Abs(this int value)
	// 	// {
	// 	// 	return Mathf.Abs(value);
	// 	// }

	// 	// public static int Clamp(this int value, int min, int max)
	// 	// {
	// 	// 	return Mathf.Clamp(value, min, max);
	// 	// }

	public static int LayerToMask(this int layerIndex)
	{
		return Mathf.RoundToInt(Mathf.Pow(2, layerIndex));
	}
}
