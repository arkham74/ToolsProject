using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace JD.VoronoiImporter
{
	public static class VoronoiExtensions
	{
		public static float2 Decode(this Color32 color32, int width, int height)
		{
			Color color = color32;
			return new float2(color.r * (width - 1), color.g * (height - 1));
		}

		public static T ReadAt<T>(this NativeArray<T> array, int2 pos, int width, int height) where T : struct
		{
			return array[ConvertToIndex(ClampToTexture(pos, width, height), width)];
		}

		public static void WriteAt<T>(this NativeArray<T> array, int2 pos, T color, int width, int height) where T : struct
		{
			array[ConvertToIndex(ClampToTexture(pos, width, height), width)] = color;
		}

		public static int2 ClampToTexture(this int2 pos, int width, int height)
		{
			return math.clamp(pos, new int2(0, 0), new int2(width - 1, height - 1));
		}

		public static int ConvertToIndex(this int2 pos, int width)
		{
			return pos.x + pos.y * width;
		}

		public static int2 ConvertToPosition(this int index, int width)
		{
			return new int2(index % width, index / width);
		}
	}
}