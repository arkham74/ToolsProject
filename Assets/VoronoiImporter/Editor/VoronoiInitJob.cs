using System.Web.UI.WebControls;
using Freya;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace JD.VoronoiImporter
{
	[BurstCompile]
	public struct VoronoiInitJob : IJobParallelForBurstSchedulable
	{
		private readonly int size;
		[WriteOnly] private NativeArray<Color32> pixels;

		public VoronoiInitJob(int size, NativeArray<Color32> pixels)
		{
			this.size = size;
			this.pixels = pixels;
		}

		public void Execute(int index)
		{
			int x = index / size;
			int y = index % size;
			float nx = (float)x / size;
			float ny = (float)y / size;
			pixels[index] = new Color(nx, ny, 0, 1);
		}
	}
}