#if UNITY_EDITOR
using UnityEngine;
using System.Collections.Generic;
using System.Text;

namespace JD
{
	public class ScreenLog : MonoBehaviour
	{
		private static readonly StringBuilder sb = new StringBuilder();
		private static readonly Dictionary<string, object> dict = new Dictionary<string, object>();
		private static ScreenLog instance;
		private static GUIStyle headStyle;

		private static void CreateLog()
		{
			if (instance == null)
			{
				instance = FindObjectOfType<ScreenLog>(true);

				if (instance == null)
				{
					var go = new GameObject("ScreenLog");
					instance = go.AddComponent<ScreenLog>();
				}
			}

			if (headStyle == null)
			{
				headStyle = new GUIStyle("Label")
				{
					stretchHeight = true,
					stretchWidth = true,
					padding = new RectOffset(7, 7, 7, 7),
					fontSize = (int)(24f * Screen.height / 1080f),
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
			sb.Clear();
			sb.AppendJoin('\n', dict.Values);
			GUILayout.Label(sb.ToString(), headStyle);
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
		public static void Log(string key, object value) { }
	}
}
#endif