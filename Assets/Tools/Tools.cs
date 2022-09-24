﻿using System;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using System.Collections.Generic;

#if TOOLS_LOCALIZATION
using UnityEngine.Localization.Settings;
#endif

namespace JD
{
	public static class Tools
	{
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

		public struct ResolutionComparer : IEqualityComparer<Resolution>
		{
			public bool Equals(Resolution x, Resolution y)
			{
				return x.width == y.width && x.height == y.height;
			}

			public int GetHashCode(Resolution obj)
			{
				return obj.GetHashCode();
			}
		}

		public static readonly StringBuilder StringBuilder = new StringBuilder();
		public static readonly StringBuilder StringBuilderCopy = new StringBuilder();

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

		public static int GetResolutionIndex()
		{
			ResolutionComparer comparer = new ResolutionComparer();
			List<Resolution> ress = GetResolutions(comparer);

			for (int i = 0; i < ress.Count; i++)
			{
				Resolution res = ress[i];
				if (comparer.Equals(res, Screen.currentResolution))
				{
					return i;
				}
			}

			return -1;
		}

		public static List<Resolution> GetResolutions()
		{
			return GetResolutions(new ResolutionComparer());
		}

		public static List<Resolution> GetResolutions(ResolutionComparer comparer)
		{
			List<Resolution> list = new List<Resolution>();

			for (int i = 0; i < Screen.resolutions.Length; i++)
			{
				Resolution res = Screen.resolutions[i];
				if (!list.Contains(res, comparer))
				{
					list.Add(res);
				}
			}

			return list;
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

		public static Vector2 GetHexCorner(int i, float angle = 0f)
		{
			float angleRad = Mathf.Deg2Rad * (60f * i + angle);
			float centerX = Mathf.Cos(angleRad);
			float centerY = Mathf.Sin(angleRad);
			return new Vector2(centerX, centerY);
		}

		public enum CustomFullScreenMode
		{
			FullScreen,
			Borderless,
			Window,
		}

		public static CustomFullScreenMode ConvertFullScreenMode(FullScreenMode index) => index switch
		{
			FullScreenMode.ExclusiveFullScreen => CustomFullScreenMode.FullScreen,
			FullScreenMode.Windowed => CustomFullScreenMode.Window,
			_ => CustomFullScreenMode.Borderless,
		};

		public static FullScreenMode ConvertFullScreenMode(CustomFullScreenMode index) => index switch
		{
			CustomFullScreenMode.FullScreen => FullScreenMode.ExclusiveFullScreen,
			CustomFullScreenMode.Window => FullScreenMode.Windowed,
			_ => FullScreenMode.FullScreenWindow,
		};
	}
}
