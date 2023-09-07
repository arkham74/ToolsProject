using Freya;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace JD.VoronoiImporter
{
	[BurstCompile]
	internal struct VoronoiJob : IJobParallelForBurstSchedulable
	{
		internal Vector3 Radius;
		internal float Size;
		[WriteOnly] internal NativeArray<Color32> Pixels;

		public void Execute(int index)
		{
			int x = (int)(index % Size);
			int y = (int)(index / Size % Size);
			int z = (int)(index / (Size * Size));
			Vector3 uvw = new Vector3(x / Size, y / Size, z / Size);
			Vector3 center = uvw.Remap(0, 1, -1, 1);
			Color color = Color.white.WithAlpha(-SDTorus(center, Radius));
			Pixels[index] = color;
		}

		private static float SDTorus(Vector3 p, Vector2 t)
		{
			Vector2 q = new Vector2(p.XZtoXY().magnitude - t.x, p.y);
			return q.magnitude - t.y;
		}
	}
}