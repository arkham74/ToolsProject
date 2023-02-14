using System;
using System.Collections.Generic;
using System.Diagnostics;
using Freya;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using Debug = UnityEngine.Debug;

namespace JD.TextureImporter
{
	internal enum Size
	{
		// _2 = 2,
		// _4 = 4,
		// _8 = 8,
		_16 = 16,
		_32 = 32,
		_64 = 64,
		_128 = 128,
		_256 = 256,
		// _512 = 512,
		// _1024 = 1024,
		// _2048 = 2048,
		// _4096 = 4096,
		// _8192 = 8192,
	}

	[BurstCompile]
	internal struct TextureJob : IJobParallelForBurstSchedulable
	{
		internal Vector3 radius;
		internal float size;
		[WriteOnly] internal NativeArray<Color32> pixels;

		public void Execute(int index)
		{
			int x = (int)(index % size);
			int y = (int)(index / size % size);
			int z = (int)(index / (size * size));
			Vector3 uvw = new Vector3(x / size, y / size, z / size);
			Vector3 center = uvw.Remap(0, 1, -1, 1);
			Color color = Color.white.WithAlpha(-sdTorus(center, radius));
			pixels[index] = color;
		}

		private float sdfSphere(Vector3 center, float radius)
		{
			return center.sqrMagnitude - radius * radius;
		}

		private float sdBox(Vector3 p, Vector3 b)
		{
			Vector3 q = Mathfs.Abs(p) - b;
			Vector3 max = Vector3.Max(q, Vector3.zero);
			float max1 = Mathfs.Max(q.y, q.z);
			float max2 = Mathfs.Max(q.x, max1);
			float min1 = Mathfs.Min(max2, 0.0f);
			return max.magnitude + min1;
		}

		private float sdTorus(Vector3 p, Vector2 t)
		{
			Vector2 q = new Vector2(p.XZtoXY().magnitude - t.x, p.y);
			return q.magnitude - t.y;
		}
	}

	[ScriptedImporter(1, "proctex")]
	internal class TextureImporter : ScriptedImporter
	{
		private const int INNER_LOOP_BATCH_COUNT = 32;
		private const TextureFormat TEXTURE_FORMAT = TextureFormat.RGBA32;

		[SerializeField] private Vector3 radius = Vector3.one * 0.5f;
		[SerializeField] private Size _size = Size._256;

		private int size => (int)_size;

		public override void OnImportAsset(AssetImportContext ctx)
		{
			Texture3D texture = new Texture3D(size, size, size, TEXTURE_FORMAT, false);
			GenerateTexture(texture);
			ctx.AddObjectToAsset("texture", texture);
			ctx.SetMainObject(texture);
		}

		private void GenerateTexture(Texture3D texture)
		{
			NativeArray<Color32> pixels = texture.GetPixelData<Color32>(0);

			TextureJob job = new TextureJob()
			{
				radius = radius,
				size = size,
				pixels = pixels,
			};

			// long start = Stopwatch.GetTimestamp();

			job.Schedule(pixels.Length, INNER_LOOP_BATCH_COUNT).Complete();

			// long end = Stopwatch.GetTimestamp();
			// Debug.LogWarning(TimeSpan.FromTicks(end - start).TotalMilliseconds);

			pixels.Dispose();
		}
	}
}
