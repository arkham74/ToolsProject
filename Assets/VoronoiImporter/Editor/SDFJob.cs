using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace JD.VoronoiImporter
{
	[BurstCompile]
	public struct SDFJob : IJobFor
	{
		private readonly NativeArray<Color32> pixels;

		public SDFJob(NativeArray<Color32> pixels)
		{
			this.pixels = pixels;
		}

		public void Execute(int index)
		{
			// pixels[index];
		}
	}
}