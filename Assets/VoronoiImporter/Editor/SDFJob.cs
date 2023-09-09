using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace JD.VoronoiImporter
{
	[BurstCompile]
	public struct SDFJob : IJobFor
	{
		private NativeArray<Color32> pixels;
		private readonly int width;
		private readonly int height;
		private readonly int min;

		public SDFJob(NativeArray<Color32> pixels, int width)
		{
			this.pixels = pixels;
			this.width = width;
			height = pixels.Length / width;
			min = math.min(width, height);
		}

		public void Execute(int index)
		{
			int2 pos = index.ConvertToPosition(width);
			float2 pos2 = pixels[index].Decode(width, height);
			float distance = math.distance(pos, pos2) / (min * 0.1f);
			pixels[index] = new Color(distance, distance, distance, 1);
		}
	}
}