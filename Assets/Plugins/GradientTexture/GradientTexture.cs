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
		public int size = 32;
		public TextureFormat format = TextureFormat.RGBA32;
		public TextureWrapMode wrapMode = TextureWrapMode.Clamp;
		public FilterMode filterMode = FilterMode.Bilinear;
		[Range(0, 16)] public int anisoLevel = 1;

		public override void OnImportAsset(AssetImportContext ctx)
		{
			Texture2D texture = new Texture2D(size, 1, format, false, true)
			{
				wrapMode = wrapMode,
				filterMode = filterMode,
				anisoLevel = anisoLevel
			};
			Color[] pixels = Enumerable.Range(0, size).Select(i => gradient.Evaluate((float) i / size)).ToArray();
			texture.SetPixels(pixels);
			ctx.AddObjectToAsset("Gradient Texture", texture);
			ctx.SetMainObject(texture);
		}
	}
}