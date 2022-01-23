using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Texture3DGenerator
{
	[ScriptedImporter(1, "tex3d")]
	public class Texture3DGenerator : ScriptedImporter
	{
		private struct NoiseData
		{
			public int size;
			public float scale;
			public Vector3 offset;
		}

		private struct NoiseJob : IJobParallelFor
		{
			[ReadOnly] public NoiseData data;
			public NativeArray<Color> result;

			public void Execute(int index)
			{
				result[index] = CalculateNoise(index, data);
			}

			private static Color CalculateNoise(int index, NoiseData data)
			{
				int x = index % data.size;
				int y = (index / data.size) % data.size;
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

		private static readonly int NoiseId = Shader.PropertyToID("_Noise"),
			SizeId = Shader.PropertyToID("_Size"),
			ScaleId = Shader.PropertyToID("_Scale"),
			IscId = Shader.PropertyToID("_InvertedSizeScale"),
			OffsetId = Shader.PropertyToID("_Offset");


		public override void OnImportAsset(AssetImportContext ctx)
		{
			Texture3D texture = new Texture3D(size, size, size, format, false) {wrapMode = wrapMode};

			Color[] colors;

			switch (mode)
			{
				case Mode.MULTI_THREADED:
					NativeArray<Color> nativea = ThreadedJob();
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

			texture.SetPixels(colors);
			texture.Apply();
			ctx.AddObjectToAsset("main tex", texture);
			ctx.SetMainObject(texture);
		}

		private Color[] GpuJob()
		{
			string[] assets = AssetDatabase.FindAssets(PATH);
			string path = AssetDatabase.GUIDToAssetPath(assets[0]);
			ComputeShader computeShader = AssetDatabase.LoadAssetAtPath<ComputeShader>(path);
			Color[] colors = new Color[size * size * size];
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

		private Color[] SingleJob()
		{
			Color[] colors = new Color[size * size * size];
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

		private NativeArray<Color> ThreadedJob()
		{
			NoiseData data = new NoiseData {offset = offset, scale = scale, size = size};
			NativeArray<Color> result = new NativeArray<Color>(size * size * size, Allocator.TempJob);
			NoiseJob job = new NoiseJob {data = data, result = result};
			JobHandle handle = job.Schedule(result.Length, 1);
			handle.Complete();
			return result;
		}

		private static Color CalculateNoise(int x, int y, int z, float size, float scale, Vector3 offset)
		{
			Vector3 pos = new Vector3(x, y, z);
			pos = pos / size * scale;
			pos += offset;
			float noise = Perlin.Noise(pos);
			return Color.white * noise;
		}
	}
}