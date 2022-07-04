using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
public class FileBasedPrefsEditorWindow : EditorWindow
{
	private int pageBools = 0;
	private int pageInts = 0;
	private int pageFloats = 0;
	private int pageStrings = 0;
	private int pageSize = 10;

	private string key;
	private bool valueBool;
	private int valueInt;
	private float valueFloat;
	private string valueString;

	private FileBasedPrefsSaveFileModel data;

	[MenuItem("Tools/File Based Prefs")]
	private static void ShowWindow()
	{
		GetWindow<FileBasedPrefsEditorWindow>("File Based Prefs");
	}

	private void OnEnable()
	{
		if (FileBasedPrefs.IsInit())
		{
			data = FileBasedPrefs.GetSaveFile();
		}
	}

	private void OnGUI()
	{
		if (FileBasedPrefs.IsInit())
		{
			DrawBools();
			EditorGUILayout.Separator();
			DrawInts();
			EditorGUILayout.Separator();
			DrawFloats();
			EditorGUILayout.Separator();
			DrawStrings();
			EditorGUILayout.Separator();

			EditorGUILayout.LabelField("Add", EditorStyles.boldLabel);
			EditorGUI.indentLevel++;
			key = EditorGUILayout.TextField("Key", key);
			GUI.enabled = key != string.Empty && !FileBasedPrefs.HasKey(key);
			EditorGUILayout.Separator();
			DrawAddItemBool();
			DrawAddItemInt();
			DrawAddItemFloat();
			DrawAddItemString();
			EditorGUI.indentLevel--;
		}
	}

	private void DrawStrings()
	{
		int pages = data.StringData.Length / pageSize;
		pageStrings = EditorGUILayout.IntSlider("Strings page", pageStrings, 0, pages);

		EditorGUI.indentLevel++;

		var page = data.StringData.Skip(pageStrings * pageSize).Take(pageSize);
		foreach (FileBasedPrefsSaveFileModel.StringItem item in page)
		{
			DrawString(item);
		}

		EditorGUI.indentLevel--;
	}

	private void DrawFloats()
	{
		int pages = data.FloatData.Length / pageSize;
		pageFloats = EditorGUILayout.IntSlider("Floats page", pageFloats, 0, pages);

		EditorGUI.indentLevel++;

		var page = data.FloatData.Skip(pageFloats * pageSize).Take(pageSize);
		foreach (FileBasedPrefsSaveFileModel.FloatItem item in page)
		{
			DrawFloat(item);
		}

		EditorGUI.indentLevel--;
	}

	private void DrawInts()
	{
		int pages = data.IntData.Length / pageSize;
		pageInts = EditorGUILayout.IntSlider("Ints page", pageInts, 0, pages);

		EditorGUI.indentLevel++;

		var page = data.IntData.Skip(pageInts * pageSize).Take(pageSize);
		foreach (FileBasedPrefsSaveFileModel.IntItem item in page)
		{
			DrawInt(item);
		}

		EditorGUI.indentLevel--;
	}

	private void DrawBools()
	{
		int pages = data.BoolData.Length / pageSize;
		pageBools = EditorGUILayout.IntSlider("Bools page", pageBools, 0, pages);

		EditorGUI.indentLevel++;

		var page = data.BoolData.Skip(pageBools * pageSize).Take(pageSize);
		foreach (FileBasedPrefsSaveFileModel.BoolItem item in page)
		{
			DrawBool(item);
		}

		EditorGUI.indentLevel--;
	}

	private void DrawString(FileBasedPrefsSaveFileModel.StringItem item)
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(item.Key);
		string newValue = EditorGUILayout.DelayedTextField(item.Value);
		FileBasedPrefs.SetString(item.Key, newValue);
		if (GUILayout.Button("Delete")) Remove(item.Key);
		EditorGUILayout.EndHorizontal();
	}

	private void DrawFloat(FileBasedPrefsSaveFileModel.FloatItem item)
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(item.Key);
		float newValue = EditorGUILayout.DelayedFloatField(item.Value);
		FileBasedPrefs.SetFloat(item.Key, newValue);
		if (GUILayout.Button("Delete")) Remove(item.Key);
		EditorGUILayout.EndHorizontal();
	}

	private void DrawInt(FileBasedPrefsSaveFileModel.IntItem item)
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(item.Key);
		int newValue = EditorGUILayout.DelayedIntField(item.Value);
		FileBasedPrefs.SetInt(item.Key, newValue);
		if (GUILayout.Button("Delete")) Remove(item.Key);
		EditorGUILayout.EndHorizontal();
	}

	private void DrawBool(FileBasedPrefsSaveFileModel.BoolItem item)
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(item.Key);
		bool newValue = EditorGUILayout.Toggle(item.Value);
		FileBasedPrefs.SetBool(item.Key, newValue);
		if (GUILayout.Button("Delete")) Remove(item.Key);
		EditorGUILayout.EndHorizontal();
	}

	private void Remove(string itemKey)
	{
		FileBasedPrefs.DeleteKey(itemKey);
		data = FileBasedPrefs.GetSaveFile();
	}

	private void DrawAddItemBool()
	{
		EditorGUILayout.BeginHorizontal();
		valueBool = EditorGUILayout.Toggle("Bool", valueBool);
		if (GUILayout.Button("Add"))
		{
			FileBasedPrefs.SetBool(key, valueBool);
			data = FileBasedPrefs.GetSaveFile();
		}

		EditorGUILayout.EndHorizontal();
	}

	private void DrawAddItemInt()
	{
		EditorGUILayout.BeginHorizontal();
		valueInt = EditorGUILayout.IntField("Int", valueInt);
		if (GUILayout.Button("Add"))
		{
			FileBasedPrefs.SetInt(key, valueInt);
			data = FileBasedPrefs.GetSaveFile();
		}

		EditorGUILayout.EndHorizontal();
	}

	private void DrawAddItemFloat()
	{
		EditorGUILayout.BeginHorizontal();
		valueFloat = EditorGUILayout.FloatField("Float", valueFloat);
		if (GUILayout.Button("Add"))
		{
			FileBasedPrefs.SetFloat(key, valueFloat);
			data = FileBasedPrefs.GetSaveFile();
		}

		EditorGUILayout.EndHorizontal();
	}

	private void DrawAddItemString()
	{
		EditorGUILayout.BeginHorizontal();
		valueString = EditorGUILayout.TextField("String", valueString);
		if (GUILayout.Button("Add"))
		{
			FileBasedPrefs.SetString(key, valueString);
			data = FileBasedPrefs.GetSaveFile();
		}

		EditorGUILayout.EndHorizontal();
	}
}
#endif