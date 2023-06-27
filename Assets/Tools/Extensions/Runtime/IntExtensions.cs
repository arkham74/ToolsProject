using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Freya;
using UnityEngine;

namespace JD
{
	public static class IntExtensions
	{
		public readonly static Dictionary<int, string> intCache = new Dictionary<int, string>(10000);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Clamp01(this int value)
		{
			return (int)Mathf.Clamp01(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Min(this int value, int min)
		{
			return Mathf.Min(value, min);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Max(this int value, int max)
		{
			return Mathf.Max(value, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Mod(this int index, int min, int max)
		{
			return (index - min).Mod(max + 1 - min) + min;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string IntArrayToString(this IReadOnlyList<int> intArray)
		{
			return string.Join(",", intArray);
		}

		// 	public static IOrderedEnumerable<int> Sort(this IEnumerable<int> array)
		// 	{
		// 		return array.OrderBy(e => e);
		// 	}

		/// <summary>
		/// Returns the difference between dates in months.
		/// </summary>
		/// <param name="current">First considered date.</param>
		/// <param name="another">Second considered date.</param>
		/// <returns>The number of full months between the given dates.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DifferenceInMonths(this DateTime current, DateTime another)
		{
			DateTime previous = current;
			DateTime next = another;

			if (current > another)
			{
				previous = another;
				next = current;
			}

			// multiply the difference in years by 12 months
			int yearMonths = (next.Year - previous.Year) * 12;
			// add difference in months
			int months = next.Month - previous.Month;
			// if the day of the next date has not reached the day of the previous one, then the last month has not yet ended
			int lastMonth = (previous.Day <= next.Day ? 0 : -1);

			return yearMonths + months + lastMonth;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LayerToMask(this int layerIndex)
		{
			return Mathf.RoundToInt(Mathf.Pow(2, layerIndex));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Random(this int value)
		{
			return UnityEngine.Random.Range(0, value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PopCount(this int mask)
		{
			return ((uint)mask).PopCount();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PopCount(this uint mask)
		{
			int bitCount = 0;
			while (mask != 0)
			{
				if ((mask & 1) == 1)
				{
					bitCount++;
				}
				mask = mask >> 1;
			}
			return bitCount;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int[] GetPowerOfTwoComponents(this int mask)
		{
			return GetPowerOfTwoComponents((uint)mask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int[] GetPowerOfTwoComponents(this uint mask)
		{
			int bitCount = mask.PopCount();
			int[] setBits = new int[bitCount];
			int bitPosition = 1;
			int index = 0;

			while (mask != 0)
			{
				if ((mask & 1) == 1)
				{
					setBits[index++] = bitPosition;
				}
				mask = mask >> 1;
				bitPosition *= 2;
			}

			return setBits;
		}
	}
}
