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
		_1 = 1,
		_2 = 2,
		_4 = 4,
		_8 = 8,
		_16 = 16,
		_32 = 32,
		_64 = 64,
		_128 = 128,
		_256 = 256,
		_512 = 512,
		_1024 = 1024,
		_2048 = 2048,
		_4096 = 4096,
		_8192 = 8192,
	}

	[BurstCompile]
	internal struct TextureJob : IJobParallelForBurstSchedulable
	{
		internal Vector2 size;
		internal NativeArray<Color32> pixels;

		public void Execute(int index)
		{
			int x = (int)(index % size.x);
			int y = (int)(index / size.y);
			Vector2 uv = new Vector2(x / size.x, y / size.y);
			Color color = default;
			color.r = uv.x;
			color.g = uv.y;
			color.b = 0;
			color.a = 1;
			pixels[index] = color;
		}
	}

	[ScriptedImporter(1, "proctex")]
	internal class TextureImporter : ScriptedImporter
	{
		private const int INNER_LOOP_BATCH_COUNT = 32;
		private const TextureFormat TEXTURE_FORMAT = TextureFormat.RGBA32;

		[SerializeField] private Size width = (Size)1024;
		[SerializeField] private Size height = (Size)1024;
		[SerializeField] private bool mips = false;
		[SerializeField] private bool sRGB = true;

		public override void OnImportAsset(AssetImportContext ctx)
		{
			Texture2D texture = new Texture2D((int)width, (int)height, TEXTURE_FORMAT, mips, !sRGB);
			GenerateTexture(texture);
			ctx.AddObjectToAsset("texture", texture);
			ctx.SetMainObject(texture);
		}

		private void GenerateTexture(Texture2D texture)
		{
			NativeArray<Color32> pixels = texture.GetPixelData<Color32>(0);
			TextureJob job = new TextureJob();
			job.size = new Vector2((float)width, (float)height);
			job.pixels = pixels;
			// long start = Stopwatch.GetTimestamp();
			job.Schedule(pixels.Length, INNER_LOOP_BATCH_COUNT).Complete();
			// long end = Stopwatch.GetTimestamp();
			// Debug.LogWarning(TimeSpan.FromTicks(end - start).TotalMilliseconds);
			texture.Apply();
		}
	}
}
