using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace JD.VoronoiImporter
{
	public struct InitJob : IJobFor
	{
		private NativeArray<Color32> pixels;
		private readonly Color32 background;
		private readonly int width;
		private readonly int height;

		public InitJob(NativeArray<Color32> pixels, Color32 background, int width, int height)
		{
			this.pixels = pixels;
			this.background = background;
			this.width = width;
			this.height = height;
		}

		public void Execute(int index)
		{
			int x = index / width;
			int y = index % width;

			float nx = (float)x / width;
			float ny = (float)y / height;

			if (pixels[index].EqualRGBA(background) || pixels[index].a == 0)
			{
				pixels[index] = Color.clear;
			}
			else
			{
				pixels[index] = new Color(nx, ny, 0, 1);
			}
		}
	}
}