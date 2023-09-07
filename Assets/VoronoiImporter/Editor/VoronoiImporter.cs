using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Collections;
using Unity.Jobs;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

namespace JD.VoronoiImporter
{
	[ScriptedImporter(1, "voronoi")]
	internal class VoronoiImporter : ScriptedImporter
	{
		internal enum TextureSize
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

		private const int INNER_LOOP_BATCH_COUNT = 32;
		private const TextureFormat TEXTURE_FORMAT = TextureFormat.RGBA32;

		[SerializeField] private Vector3 radius = Vector3.one * 0.5f;
		[SerializeField] private TextureSize textureSize = TextureSize._256;

		private int Size => (int)textureSize;

		public override void OnImportAsset(AssetImportContext ctx)
		{
			Texture3D texture = new Texture3D(Size, Size, Size, TEXTURE_FORMAT, false);
			GenerateTexture(texture);
			ctx.AddObjectToAsset("texture", texture);
			ctx.SetMainObject(texture);
		}

		private void GenerateTexture(Texture3D texture)
		{
			NativeArray<Color32> pixels = texture.GetPixelData<Color32>(0);

			VoronoiJob job = new VoronoiJob()
			{
				Radius = radius,
				Size = Size,
				Pixels = pixels,
			};

			// long start = Stopwatch.GetTimestamp();

			job.Schedule(pixels.Length, INNER_LOOP_BATCH_COUNT).Complete();

			// long end = Stopwatch.GetTimestamp();
			// Debug.LogWarning(TimeSpan.FromTicks(end - start).TotalMilliseconds);

			pixels.Dispose();
		}
	}
}