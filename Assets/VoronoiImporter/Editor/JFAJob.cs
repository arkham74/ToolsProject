using System;
using System.Text;
using Freya;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace JD.VoronoiImporter
{
	[BurstCompile]
	public readonly struct JFAJob : IJobFor
	{
		private readonly NativeArray<Color32> pixels;
		[ReadOnly] private readonly NativeArray<Color32> readBuffer;
		private readonly int width;
		private readonly int offset;
		private readonly int height;

		public JFAJob(NativeArray<Color32> pixels, int width, NativeArray<Color32> readBuffer, int offset)
		{
			this.pixels = pixels;
			this.width = width;
			this.readBuffer = readBuffer;
			this.offset = offset;

			height = pixels.Length / width;
		}

		public void Execute(int index)
		{
			int2 pos = ConvertToPosition(index);
			Color32 color = default;
			float distance = float.MaxValue;

			for (int u = -1; u <= 1; u++)
			{
				for (int v = -1; v <= 1; v++)
				{
					int2 offsetPos = pos + new int2(u, v) * offset;
					Color32 offsetValue = ReadAt(readBuffer, offsetPos);
					float dist = math.distancesq(Decode(offsetValue), pos);

					if (offsetValue.a != 0 && dist < distance)
					{
						distance = dist;
						color = offsetValue;
					}
				}
			}

			WriteAt(pixels, pos, color);
		}

		private float2 Decode(Color32 color32)
		{
			Color color = color32;
			return new float2(color.r * (width - 1), color.g * (height - 1));
		}

		private T ReadAt<T>(NativeArray<T> array, int2 pos) where T : struct
		{
			return array[ConvertToIndex(ClampToTexture(pos))];
		}

		private void WriteAt<T>(NativeArray<T> array, int2 pos, T color) where T : struct
		{
			array[ConvertToIndex(ClampToTexture(pos))] = color;
		}

		private int2 ClampToTexture(int2 pos)
		{
			return math.clamp(pos, new int2(0, 0), new int2(width - 1, height - 1));
		}

		private int ConvertToIndex(int2 pos)
		{
			return pos.x + pos.y * width;
		}

		private int2 ConvertToPosition(int index)
		{
			return new int2(index % width, index / width);
		}
	}
}