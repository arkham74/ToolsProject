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
			int2 pos = index.ConvertToPosition(width);
			Color32 color = default;
			float distance = float.MaxValue;

			for (int u = -1; u <= 1; u++)
			{
				for (int v = -1; v <= 1; v++)
				{
					int2 offsetPos = pos + new int2(u, v) * offset;
					Color32 offsetValue = readBuffer.ReadAt(offsetPos, width, height);
					float dist = math.distancesq(offsetValue.Decode(width, height), pos);

					if (offsetValue.a != 0 && dist < distance)
					{
						distance = dist;
						color = offsetValue;
					}
				}
			}

			pixels.WriteAt(pos, color, width, height);
		}
	}
}