#if UNITY_EDITOR || DEVELOPMENT_BUILD
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ScreenLog : MonoBehaviour
{
	private static readonly StringBuilder sb = new StringBuilder();
	private static readonly Dictionary<string, object> dict = new Dictionary<string, object>();
	private static ScreenLog instance;

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

		GUIStyle headStyle = new GUIStyle("Label");
		headStyle.normal.background = Resources.Load<Texture2D>("transparent_1x1");
		headStyle.stretchHeight = true;
		headStyle.stretchWidth = true;
		headStyle.padding = new RectOffset(7, 7, 7, 7);
		headStyle.fontSize = (int)(24f * Screen.height / 1080f);

		GUILayout.Label(sb.ToString(), headStyle);
	}
}
#endif