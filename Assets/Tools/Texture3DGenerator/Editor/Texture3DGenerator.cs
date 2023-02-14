using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;
using Unity.Burst;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace JD.Texture3DGenerator
{
	[ScriptedImporter(1, "tex3d")]
	public class Texture3DGenerator : ScriptedImporter
	{
#if TOOLS_BURST
		[BurstCompile]
#endif
		private struct NoiseJob : IJobParallelFor
		{
			public int size;
			public float scale;
			public Vector3 offset;
			[WriteOnly] public NativeArray<Color32> result;

			public void Execute(int index)
			{
				result[index] = CalculateNoise(index, this);
			}

			private static Color32 CalculateNoise(int index, NoiseJob data)
			{
				int x = index % data.size;
				int y = index / data.size % data.size;
				int z = index / (data.size * data.size);
				float size = data.size;
				float scale = data.scale;
				Vector3 offset = data.offset;
				return Texture3DGenerator.CalculateNoise(x, y, z, size, scale, offset);
			}
		}

		public enum Mode
		{
			GPU,
			MULTI_THREADED,
			SINGLE_THREADED
		}

		private const string PATH = "Tex3DCompute";

		public int size = 32;
		public float scale = 10f;
		public Vector3 offset;
		public TextureFormat format = TextureFormat.RGBA32;
		public TextureWrapMode wrapMode = TextureWrapMode.Mirror;
		public Mode mode;
		private readonly Stopwatch watch = new Stopwatch();

		private static readonly int NoiseId = Shader.PropertyToID("_Noise"),
			SizeId = Shader.PropertyToID("_Size"),
			ScaleId = Shader.PropertyToID("_Scale"),
			IscId = Shader.PropertyToID("_InvertedSizeScale"),
			OffsetId = Shader.PropertyToID("_Offset");


		public override void OnImportAsset(AssetImportContext ctx)
		{
			Texture3D texture = new Texture3D(size, size, size, format, false) { wrapMode = wrapMode };

			Color32[] colors;

			watch.Restart();

			switch (mode)
			{
				case Mode.MULTI_THREADED:
					NativeArray<Color32> nativea = ThreadedJob();
					colors = nativea.ToArray();
					nativea.Dispose();
					break;
				case Mode.SINGLE_THREADED:
					colors = SingleJob();
					break;
				case Mode.GPU:
					colors = GpuJob();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			watch.Stop();

			Debug.LogWarning(watch.Elapsed.TotalSeconds);

			texture.SetPixels32(colors);
			texture.Apply();
			ctx.AddObjectToAsset("main tex", texture);
			ctx.SetMainObject(texture);
		}

		private Color32[] GpuJob()
		{
			string[] assets = AssetDatabase.FindAssets(PATH);
			string path = AssetDatabase.GUIDToAssetPath(assets[0]);
			ComputeShader computeShader = AssetDatabase.LoadAssetAtPath<ComputeShader>(path);
			Color32[] colors = new Color32[size * size * size];
			ComputeBuffer noiseBuffer = new ComputeBuffer(colors.Length, 4 * sizeof(float));

			computeShader.SetFloat(SizeId, size);
			computeShader.SetFloat(ScaleId, scale);
			computeShader.SetFloat(IscId, 1f / size * scale);
			computeShader.SetVector(OffsetId, offset);
			computeShader.SetBuffer(0, NoiseId, noiseBuffer);

			int groups = Mathf.CeilToInt(size / 4f);

			computeShader.Dispatch(0, groups, groups, groups);
			noiseBuffer.GetData(colors);
			noiseBuffer.Release();
			return colors;
		}

		private Color32[] SingleJob()
		{
			Color32[] colors = new Color32[size * size * size];
			for (int x = 0; x < size; x++)
			{
				for (int y = 0; y < size; y++)
				{
					for (int z = 0; z < size; z++)
					{
						int index = x + size * (y + size * z);
						colors[index] = CalculateNoise(x, y, z, size, scale, offset);
					}
				}
			}

			return colors;
		}

		private NativeArray<Color32> ThreadedJob()
		{
			NativeArray<Color32> result = new NativeArray<Color32>(size * size * size, Allocator.TempJob);
			NoiseJob job = new NoiseJob
			{
				result = result,
				offset = offset,
				scale = scale,
				size = size
			};
			JobHandle handle = job.Schedule(result.Length, 32);
			handle.Complete();
			return result;
		}

		private static Color32 CalculateNoise(int x, int y, int z, float size, float scale, Vector3 offset)
		{
			Vector3 pos = new Vector3(x, y, z);
			pos = pos / size * scale;
			pos += offset;
			float noise = Perlin.Noise(pos);
			return Color.white * noise;
		}
	}
}