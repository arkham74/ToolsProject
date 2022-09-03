using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace JD.TextureChannelPacker
{
	[ScriptedImporter(1, "texpack")]
	public class TextureChannelPacker : ScriptedImporter
	{
		public enum Channel
		{
			Red,
			Green,
			Blue,
			Alpha
		}

		public Vector2Int size = new Vector2Int(512, 512);
		public Texture2D redTex;
		public Texture2D greenTex;
		public Texture2D blueTex;
		public Texture2D alphaTex;

		public Channel redChannel = Channel.Red;
		public Channel greenChannel = Channel.Green;
		public Channel blueChannel = Channel.Blue;
		public Channel alphaChannel = Channel.Alpha;

		public bool redInvert;
		public bool greenInvert;
		public bool blueInvert;
		public bool alphaInvert;

		public TextureWrapMode wrapMode = TextureWrapMode.Repeat;
		public FilterMode filterMode = FilterMode.Bilinear;
		[Range(0, 16)] public int anisoLevel = 1;

		public override void OnImportAsset(AssetImportContext ctx)
		{
			int sizeX = Mathf.Max(1, size.x);
			int sizeY = Mathf.Max(1, size.y);
			Texture2D texture = new Texture2D(sizeX, sizeY)
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

			Color32[] colors = new Color32[sizeX * sizeY];
			Color32[] redcolors = red.GetPixels32();
			Color32[] greencolors = green.GetPixels32();
			Color32[] bluecolors = blue.GetPixels32();
			Color32[] alphacolors = alpha.GetPixels32();

			for (int i = 0; i < colors.Length; i++)
			{
				byte r = GetChannel(i, redcolors, redChannel, redInvert);
				byte g = GetChannel(i, greencolors, greenChannel, greenInvert);
				byte b = GetChannel(i, bluecolors, blueChannel, blueInvert);
				byte a = GetChannel(i, alphacolors, alphaChannel, alphaInvert);
				colors[i] = new Color32(r, g, b, a);
			}

			texture.SetPixels32(colors);
			texture.Apply();
			ctx.AddObjectToAsset("main tex", texture);
			ctx.SetMainObject(texture);
		}

		private static Texture2D GetTexture(Texture2D tex, Texture2D def)
		{
			if (tex && !tex.isReadable)
			{
				Debug.LogWarning($"{tex} is not readable");
			}

			return tex && tex.isReadable ? tex : def;
		}

		private static byte GetChannel(int i, IReadOnlyList<Color32> colors, Channel channel, bool invert)
		{
			Color32 color = colors[i];
			byte c = color[(int)channel];
			byte inverted = (byte)(255 - c);
			return invert ? inverted : c;
		}

		//https://pastebin.com/qkkhWs2J
		private static Texture2D Scale(Texture2D src, int width, int height, FilterMode mode = FilterMode.Trilinear)
		{
			src.filterMode = mode;
			src.Apply(true);

			RenderTexture rtt = new RenderTexture(width, height, 32);
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
