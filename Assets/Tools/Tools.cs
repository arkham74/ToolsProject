using System;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

#if TOOLS_LOCALIZATION
using UnityEngine.Localization.Settings;
#endif

public static class Tools
{
	public static readonly StringBuilder StringBuilder = new StringBuilder();

#if TOOLS_LOCALIZATION
	public static string GetLocalizedString(string key)
	{
		return LocalizationSettings.StringDatabase.GetLocalizedString(key);
	}
	public static string GetLocalizedString(string key, params object[] args)
	{
		return LocalizationSettings.StringDatabase.GetLocalizedString(key, args);
	}
#endif

	public static void Quit()
	{
#if UNITY_EDITOR
		EditorApplication.ExitPlaymode();
#else
		Application.Quit();
#endif
	}

	public static bool IsSceneLoaded(string sceneName)
	{
		return SceneManager.GetSceneByName(sceneName).name == sceneName;
	}

	public static void LoadIfNotLoaded(string sceneName, LoadSceneMode loadSceneMode)
	{
		if (!IsSceneLoaded(sceneName))
			SceneManager.LoadScene(sceneName, loadSceneMode);
	}

	public static bool TryGetArg(string name, out string output)
	{
		output = string.Empty;
		string[] args = Environment.GetCommandLineArgs();
		for (int i = 0; i < args.Length; i++)
		{
			if (args[i] == name && args.Length > i + 1)
			{
				output = args[i + 1];
				return true;
			}
		}
		return false;
	}

	public static Vector2 GenerateCircle(float t, float radius = 1f)
	{
		float rad = t * Mathf.PI * 2;
		float x = Mathf.Sin(rad) * radius;
		float y = Mathf.Cos(rad) * radius;
		return new Vector2(x, y);
	}

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
		int flagsValue = (int)(object)flags;
		int flagValue = (int)(object)flag;

		return (flagsValue & flagValue) != 0;
	}

	public static T Add<T>(T a, T b) where T : struct
	{
		int flagsValue = (int)(object)a;
		int flagValue = (int)(object)b;

		return (T)(object)(flagsValue | flagValue);
	}

	public static T Sub<T>(T a, T b) where T : struct
	{
		int flagsValue = (int)(object)a;
		int flagValue = (int)(object)b;

		return (T)(object)(flagsValue & (~flagValue));
	}

	public static Texture2D CreateTexture(int width, int height, Color color, int mipCount = 1, bool linear = true,
		TextureFormat tf = TextureFormat.RGBA32, FilterMode fm = FilterMode.Point)
	{
		Color32 color32 = color.ToColor32();
		Color32[] pixels = Enumerable.Repeat(color32, width * height).ToArray();
		Texture2D tex = new Texture2D(width, height, tf, mipCount, linear)
		{
			filterMode = fm,
			name = $"Tex_{width}x{height}_{color.ToHtml()}"
		};
		tex.SetPixels32(pixels);
		tex.Apply();
		return tex;
	}

	public static Vector3 GetHexCorner(Vector3 center, float size, int i)
	{
		float angleDeg = 60f * i;
		float angleRad = Mathf.Deg2Rad * angleDeg;
		float centerX = center.x + size * Mathf.Cos(angleRad);
		float centerZ = center.z + size * Mathf.Sin(angleRad);
		return new Vector3(centerX, 0, centerZ);
	}
}