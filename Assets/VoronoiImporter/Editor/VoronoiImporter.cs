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
			VoronoiInitJob initJob = new VoronoiInitJob(pixels, background, texture.width, texture.height);
			JobHandle handle = initJob.ScheduleParallel(pixels.Length, INNERLOOP_BATCH_COUNT, default);
			handle.Complete();
			texture.Apply();
			return texture;
		}
	}

	public struct VoronoiInitJob : IJobFor
	{
		private NativeArray<Color32> pixels;
		private readonly Color32 background;
		private readonly int width;
		private readonly int height;

		public VoronoiInitJob(NativeArray<Color32> pixels, Color32 background, int width, int height)
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