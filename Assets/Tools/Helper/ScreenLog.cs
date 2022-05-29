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

	private static readonly GUIStyle headStyle = new GUIStyle("Label") { fontSize = 24 };

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

		foreach (KeyValuePair<string, object> item in dict)
		{
			sb.Append(item.Value);
			sb.AppendLine();
		}

		GUILayout.Label(sb.ToString(), headStyle);
	}
}
#else
public static class ScreenLog
{
	public static void Log(string key, object value) { }
}
#endif