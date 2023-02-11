using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using Object = UnityEngine.Object;

#if TOOLS_LOCALIZATION
using UnityEngine.Localization.Settings;
#endif

namespace JD
{
	public static class Tools
	{
		public enum CustomScreenMode
		{
			ExclusiveFullScreen,
			FullScreenWindow,
			Windowed
		}

		public static FullScreenMode ConvertScreenMode(CustomScreenMode mode) => mode switch
		{
			CustomScreenMode.ExclusiveFullScreen => FullScreenMode.ExclusiveFullScreen,
			CustomScreenMode.Windowed => FullScreenMode.Windowed,
			_ => FullScreenMode.FullScreenWindow,
		};

		public static CustomScreenMode ConvertScreenMode(FullScreenMode mode) => mode switch
		{
			FullScreenMode.ExclusiveFullScreen => CustomScreenMode.ExclusiveFullScreen,
			FullScreenMode.Windowed => CustomScreenMode.Windowed,
			_ => CustomScreenMode.FullScreenWindow,
		};

		public const float MPS2_KPH = 3.6f;

#if UNITY_EDITOR
		public static bool Test
		{
			get => EditorPrefs.GetBool("test", false);
			set => EditorPrefs.SetBool("test", value);
		}

#else
		public const bool Test = false;
#endif

#if TOOLS_LOCALIZATION
		public static string GetLocalizedString(string key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedString(key);
		}

		public static bool Roll(int sides)
		{
			return Random.Range(0, sides) == 0;
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
			{
				SceneManager.LoadScene(sceneName, loadSceneMode);
			}
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

		public static Color RandomColor()
		{
			return new Color(Random.value, Random.value, Random.value);
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

		public static T Instantiate<T>(T prefab, Transform transform) where T : Object
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				return PrefabUtility.InstantiatePrefab(prefab, transform) as T;
			}
			else
#endif
			{
				return Object.Instantiate(prefab, transform);
			}
		}

		public static Vector2 GetHexCorner(int i, float angle = 0f)
		{
			float angleRad = Mathf.Deg2Rad * (60f * i + angle);
			float centerX = Mathf.Cos(angleRad);
			float centerY = Mathf.Sin(angleRad);
			return new Vector2(centerX, centerY);
		}
	}
}
