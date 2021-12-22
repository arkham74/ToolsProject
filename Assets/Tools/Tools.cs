using System.Linq;
using UnityEditor;
using UnityEngine;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
public static class Tools
{
#if UNITY_EDITOR
	public static T[] FindAssetsByType<T>() where T : Object
	{
		string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
		T[] assets = new T[guids.Length];
		for (int i = 0; i < guids.Length; i++)
		{
			string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
			assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
		}

		return assets;
	}
#endif

	public static int RandomPosNeg()
	{
		return Random.Range(0, 2) * 2 - 1;
	}

	public static int Map2DTo1D(int x, int y, int width)
	{
		return x + width * y;
	}

	public static (int, int) Map1DTo2D(int i, int width)
	{
		return (i % width, i / width);
	}

	public static bool IsSet<T>(T flags, T flag) where T : struct
	{
		int flagsValue = (int) (object) flags;
		int flagValue = (int) (object) flag;

		return (flagsValue & flagValue) != 0;
	}

	public static T Add<T>(T a, T b) where T : struct
	{
		int flagsValue = (int) (object) a;
		int flagValue = (int) (object) b;

		return (T) (object) (flagsValue | flagValue);
	}

	public static T Sub<T>(T a, T b) where T : struct
	{
		int flagsValue = (int) (object) a;
		int flagValue = (int) (object) b;

		return (T) (object) (flagsValue & (~flagValue));
	}

	public static Texture2D CreateTexture(int width, int height, Color color, int mipCount = 1, bool linear = true,
		TextureFormat tf = TextureFormat.RGBA32, FilterMode fm = FilterMode.Point)
	{
		Color32 color32 = color.ToColor32();
		Color32[] pixels = Enumerable.Repeat(color32, width * height).ToArray();
		Texture2D tex =
			new Texture2D(width, height, tf, mipCount, linear)
			{
				filterMode = fm, name = $"Tex_{width}x{height}_{color.ToHtml()}"
			};
		tex.SetPixels32(pixels);
		tex.Apply();
		return tex;
	}
}