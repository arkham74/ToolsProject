using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using UnityEditor.AssetImporters;
using UnityEditor;

namespace GradientTexture
{
	[ScriptedImporter(1, "gradtex")]
	public class GradientTexture : ScriptedImporter
	{
		public Gradient gradient = new Gradient();
		public Vector2Int size = new Vector2Int(512, 32);
		public TextureFormat format = TextureFormat.RGBA32;
		public TextureWrapMode wrapMode = TextureWrapMode.Clamp;
		public FilterMode filterMode = FilterMode.Bilinear;
		[Range(0, 16)] public int anisoLevel = 1;

		public override void OnImportAsset(AssetImportContext ctx)
		{
			Texture2D texture = new Texture2D(size.x, size.y, format, false, true)
			{
				wrapMode = wrapMode,
				filterMode = filterMode,
				anisoLevel = anisoLevel
			};
			Color[] pixels = new Color[size.x * size.y];

			for (int x = 0; x < size.x; x++)
			{
				Color pixel = gradient.Evaluate((float) x / size.x);
				for (int y = 0; y < size.y; y++)
				{
					pixels[y * size.x + x] = pixel;
				}
			}

			texture.SetPixels(pixels);
			ctx.AddObjectToAsset("Gradient Texture", texture);
			ctx.SetMainObject(texture);
		}
	}
}