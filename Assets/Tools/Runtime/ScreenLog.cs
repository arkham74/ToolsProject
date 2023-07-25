#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Pool;

namespace JD
{
	public class ScreenLog : MonoBehaviour
	{
		private const char SEPARATOR = '\n';
		private static readonly Dictionary<string, object> dict = new Dictionary<string, object>();
		private static ScreenLog instance;
		private static GUIStyle headStyle;

		private static void CreateLog()
		{
			if (!Application.isPlaying) return;
			if (instance != null) return;

			instance = FindObjectOfType<ScreenLog>(true);
			if (instance == null)
			{
				var go = new GameObject("ScreenLog");
				instance = go.AddComponent<ScreenLog>();
			}
		}

		private static void CreateStyle()
		{
			if (headStyle == null)
			{
				headStyle = new GUIStyle("Label")
				{
					stretchHeight = true,
					stretchWidth = true,
					padding = new RectOffset(7, 7, 7, 7),
				};

				headStyle.normal.background = Resources.Load<Texture2D>("transparent_1x1");
			}
		}

		public static void Log(string key, object value)
		{
			CreateLog();
			if (dict.ContainsKey(key))
			{
				dict[key] = value;
			}
			else
			{
				dict.Add(key, value);
			}
		}

		private void OnGUI()
		{
			CreateStyle();
			StringBuilder sb = Pools.GetStringBuilder();
			sb.AppendJoin(SEPARATOR, dict.Values);
			string text = sb.ToString();
			headStyle.fontSize = (int)(24f * Screen.height / 1080f);
			GUILayout.Label(text, headStyle);
			Pools.Release(sb);
		}
	}
}
#else
using System.Diagnostics;
namespace JD
{
	public static class ScreenLog
	{
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void Log(string key, object value) { }
	}
}
#endif
