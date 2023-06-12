using System;
using System.Runtime.CompilerServices;
using Freya;
using UnityEngine;

namespace JD
{
	public static class SpanExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Loop<T>(this Span<T> array, int index)
		{
			return array[Mathfs.Mod(index, array.Length)];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Random<T>(this Span<T> list)
		{
			return list[UnityEngine.Random.Range(0, list.Length)];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Shuffle<T>(this Span<T> list)
		{
			int n = list.Length;
			while (n > 1)
			{
				n--;
				int k = UnityEngine.Random.Range(0, n + 1);
				(list[n], list[k]) = (list[k], list[n]);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Average(this Span<float> span)
		{
			return span.Sum() / span.Length;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sum(this Span<float> span)
		{
			float sum = 0;
			for (int i = 0; i < span.Length; i++)
			{
				sum += span[i];
			}
			return sum;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Max(this Span<float> span)
		{
			float max = 0;
			for (int i = 0; i < span.Length; i++)
			{
				max = Mathf.Max(max, span[i]);
			}
			return max;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Min(this Span<float> span)
		{
			float min = 0;
			for (int i = 0; i < span.Length; i++)
			{
				min = Mathf.Min(min, span[i]);
			}
			return min;
		}
	}
}