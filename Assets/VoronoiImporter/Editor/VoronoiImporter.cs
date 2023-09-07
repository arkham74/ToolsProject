using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace JD.VoronoiImporter
{
	[ScriptedImporter(1, "voronoi")]
	public class VoronoiImporter : ScriptedImporter
	{
		public enum TextureSize
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

		private const int INNER_LOOP_BATCH_COUNT = 32;
		private const TextureFormat TEXTURE_FORMAT = TextureFormat.RGBA32;

		[SerializeField] private TextureSize textureSize = TextureSize._256;
		[SerializeField] private int points = 10;

		private int Size => (int)textureSize;

		public override void OnImportAsset(AssetImportContext ctx)
		{
			Texture2D texture = new Texture2D(Size, Size, TEXTURE_FORMAT, false);
			for (int i = 0; i < points; i++)
			{
				int x = Random.Range(0, Size - 1);
				int y = Random.Range(0, Size - 1);
				texture.SetPixel(x, y, new Color(Random.value, Random.value, Random.value, 1));
			}

			GenerateTexture(texture);
			ctx.AddObjectToAsset("texture", texture);
			ctx.SetMainObject(texture);
		}

		private void GenerateTexture(Texture2D texture)
		{
			NativeArray<Color32> pixels = texture.GetPixelData<Color32>(0);
			VoronoiInitJob initJob = new VoronoiInitJob(Size, pixels);
			// long start = Stopwatch.GetTimestamp();
			initJob.Schedule(pixels.Length, INNER_LOOP_BATCH_COUNT).Complete();
			// long end = Stopwatch.GetTimestamp();
			// Debug.LogWarning(TimeSpan.FromTicks(end - start).TotalMilliseconds);
			pixels.Dispose();
		}
	}
}