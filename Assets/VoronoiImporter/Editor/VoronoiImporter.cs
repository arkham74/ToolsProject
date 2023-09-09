using System;
using System.Collections.Generic;
using System.Diagnostics;
using Freya;
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
		private const int INNERLOOP_BATCH_COUNT = 32;

		[SerializeField] private Texture2D source;
		[SerializeField] private Color32 background = Color.white;

		public override void OnImportAsset(AssetImportContext ctx)
		{
			if (source)
			{
				Texture2D texture = GenerateTexture();
				ctx.AddObjectToAsset("texture", texture);
				ctx.SetMainObject(texture);
			}
		}

		private Texture2D GenerateTexture()
		{
			Texture2D texture = source.CloneNonReadable(TextureFormat.RGBA32);
			NativeArray<Color32> pixels = texture.GetPixelData<Color32>(0);

			InitJob initJob = new InitJob(pixels, background, texture.width, texture.height);
			JobHandle handle = initJob.ScheduleParallel(pixels.Length, INNERLOOP_BATCH_COUNT, default);
			handle.Complete();

			int larger = Math.Max(texture.width, texture.height);
			int passCount = Mathf.Log(larger, 2).CeilToInt();

			for (int i = 0; i < passCount; i++)
			{
				int offset = Mathf.Pow(2, passCount - i - 1).CeilToInt();
				NativeArray<Color32> readBuffer = new NativeArray<Color32>(pixels, Allocator.TempJob);
				JFAJob jfaJob = new JFAJob(pixels, texture.width, readBuffer, offset);
				handle = jfaJob.ScheduleParallel(pixels.Length, INNERLOOP_BATCH_COUNT, default);
				handle.Complete();
				readBuffer.Dispose();
			}

			SDFJob sdfJob = new SDFJob(pixels);
			handle = sdfJob.ScheduleParallel(pixels.Length, INNERLOOP_BATCH_COUNT, default);
			handle.Complete();

			texture.Apply();
			return texture;
		}
	}
}