using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using Debug = UnityEngine.Debug;

namespace JD.TextureChannelPacker
{
	[ScriptedImporter(1, "texpack")]
	public class TextureChannelPacker : ScriptedImporter
	{
		[BurstCompile]
		public struct PackJob : IJobParallelFor
		{
			[WriteOnly] public NativeArray<Color32> colors;

			[ReadOnly] public NativeArray<Color32> alphaColors;
			[ReadOnly] public NativeArray<Color32> blueColors;
			[ReadOnly] public NativeArray<Color32> greenColors;
			[ReadOnly] public NativeArray<Color32> redColors;

			[ReadOnly] public Channel alphaChannel;
			[ReadOnly] public Channel blueChannel;
			[ReadOnly] public Channel greenChannel;
			[ReadOnly] public Channel redChannel;

			[ReadOnly] public bool alphaInvert;
			[ReadOnly] public bool blueInvert;
			[ReadOnly] public bool greenInvert;
			[ReadOnly] public bool redInvert;

			public void Execute(int i)
			{
				byte r = GetChannel(i, redColors, redChannel, redInvert);
				byte g = GetChannel(i, greenColors, greenChannel, greenInvert);
				byte b = GetChannel(i, blueColors, blueChannel, blueInvert);
				byte a = GetChannel(i, alphaColors, alphaChannel, alphaInvert);
				colors[i] = new Color32(r, g, b, a);
			}

			private static byte GetChannel(int i, NativeArray<Color32> colors, Channel channel, bool invert)
			{
				Color32 color = colors[i];
				byte c = color[(int)channel];
				byte inverted = (byte)(255 - c);
				return invert ? inverted : c;
			}
		}

		public enum Channel
		{
			Red,
			Green,
			Blue,
			Alpha
		}

		[SerializeField] private Vector2Int size = new Vector2Int(512, 512);
		[SerializeField] private Texture2D redTex;
		[SerializeField] private Texture2D greenTex;
		[SerializeField] private Texture2D blueTex;
		[SerializeField] private Texture2D alphaTex;

		[SerializeField] private Channel redChannel = Channel.Red;
		[SerializeField] private Channel greenChannel = Channel.Green;
		[SerializeField] private Channel blueChannel = Channel.Blue;
		[SerializeField] private Channel alphaChannel = Channel.Alpha;

		[SerializeField] private bool redInvert;
		[SerializeField] private bool greenInvert;
		[SerializeField] private bool blueInvert;
		[SerializeField] private bool alphaInvert;

		[SerializeField] private bool generateMips = true;
		[SerializeField] private bool sRGB = true;
		[SerializeField] private TextureWrapMode wrapMode = TextureWrapMode.Repeat;
		[SerializeField] private FilterMode filterMode = FilterMode.Bilinear;
		[SerializeField][Range(0, 16)] private int anisoLevel = 1;

		public override void OnImportAsset(AssetImportContext ctx)
		{
			int sizeX = Mathf.Max(1, size.x);
			int sizeY = Mathf.Max(1, size.y);

			Texture2D texture = new Texture2D(sizeX, sizeY, TextureFormat.RGBA32, generateMips, !sRGB)
			{
				filterMode = filterMode,
				wrapMode = wrapMode,
				anisoLevel = anisoLevel
			};

			Texture2D rTex = GetTexture(redTex, Texture2D.blackTexture);
			Texture2D gTex = GetTexture(greenTex, Texture2D.blackTexture);
			Texture2D bTex = GetTexture(blueTex, Texture2D.blackTexture);
			Texture2D aTex = GetTexture(alphaTex, Texture2D.whiteTexture);

			Texture2D red = Scale(rTex, sizeX, sizeY, filterMode);
			Texture2D green = Scale(gTex, sizeX, sizeY, filterMode);
			Texture2D blue = Scale(bTex, sizeX, sizeY, filterMode);
			Texture2D alpha = Scale(aTex, sizeX, sizeY, filterMode);

			NativeArray<Color32> colors = texture.GetPixelData<Color32>(0);
			NativeArray<Color32> redColors = red.GetPixelData<Color32>(0);
			NativeArray<Color32> greenColors = green.GetPixelData<Color32>(0);
			NativeArray<Color32> blueColors = blue.GetPixelData<Color32>(0);
			NativeArray<Color32> alphaColors = alpha.GetPixelData<Color32>(0);

			PackJob job = new PackJob()
			{
				colors = colors,
				redColors = redColors,
				greenColors = greenColors,
				blueColors = blueColors,
				alphaColors = alphaColors,
				redChannel = redChannel,
				greenChannel = greenChannel,
				blueChannel = blueChannel,
				alphaChannel = alphaChannel,
				redInvert = redInvert,
				greenInvert = greenInvert,
				blueInvert = blueInvert,
				alphaInvert = alphaInvert,
			};

			JobHandle handle = job.Schedule(colors.Length, 32);
			handle.Complete();

			if (generateMips) texture.Apply(true);

			ctx.AddObjectToAsset("texture", texture);
			ctx.SetMainObject(texture);
		}

		private static Texture2D GetTexture(Texture2D tex, Texture2D def)
		{
			if (tex && !tex.isReadable) Debug.LogWarning($"{tex} is not readable");
			return tex && tex.isReadable ? tex : def;
		}

		private static Texture2D Scale(Texture2D src, int width, int height, FilterMode mode = FilterMode.Bilinear)
		{
			src.filterMode = mode;
			src.Apply(true);

			RenderTexture rtt = new RenderTexture(width, height, 0);

			Graphics.SetRenderTarget(rtt);

			GL.LoadPixelMatrix(0, 1, 1, 0);
			GL.Clear(true, true, new Color(0, 0, 0, 0));

			Graphics.DrawTexture(new Rect(0, 0, 1, 1), src);

			Texture2D result = new Texture2D(width, height, TextureFormat.RGBA32, true);

			result.ReadPixels(new Rect(0, 0, width, height), 0, 0, true);
			return result;
		}
	}
}
