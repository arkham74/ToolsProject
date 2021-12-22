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

public class FileBasedPrefsEditorWindow : EditorWindow
{
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
		data = FileBasedPrefs.GetSaveFile();
	}

	private void OnGUI()
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

	private void DrawStrings()
	{
		if (data.stringData.Length > 0) EditorGUILayout.LabelField("String", EditorStyles.boldLabel);
		EditorGUI.indentLevel++;
		foreach (FileBasedPrefsSaveFileModel.StringItem item in data.stringData)
		{
			DrawString(item);
		}

		EditorGUI.indentLevel--;
	}

	private void DrawFloats()
	{
		if (data.floatData.Length > 0) EditorGUILayout.LabelField("Float", EditorStyles.boldLabel);
		EditorGUI.indentLevel++;
		foreach (FileBasedPrefsSaveFileModel.FloatItem item in data.floatData)
		{
			DrawFloat(item);
		}

		EditorGUI.indentLevel--;
	}

	private void DrawInts()
	{
		if (data.intData.Length > 0) EditorGUILayout.LabelField("Int", EditorStyles.boldLabel);
		EditorGUI.indentLevel++;
		foreach (FileBasedPrefsSaveFileModel.IntItem item in data.intData)
		{
			DrawInt(item);
		}

		EditorGUI.indentLevel--;
	}

	private void DrawBools()
	{
		if (data.boolData.Length > 0) EditorGUILayout.LabelField("Bool", EditorStyles.boldLabel);
		EditorGUI.indentLevel++;
		foreach (FileBasedPrefsSaveFileModel.BoolItem item in data.boolData)
		{
			DrawBool(item);
		}

		EditorGUI.indentLevel--;
	}

	private void DrawString(FileBasedPrefsSaveFileModel.StringItem item)
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(item.key);
		string newValue = EditorGUILayout.DelayedTextField(item.value);
		FileBasedPrefs.SetString(item.key, newValue);
		if (GUILayout.Button("Delete")) Remove(item.key);
		EditorGUILayout.EndHorizontal();
	}

	private void DrawFloat(FileBasedPrefsSaveFileModel.FloatItem item)
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(item.key);
		float newValue = EditorGUILayout.DelayedFloatField(item.value);
		FileBasedPrefs.SetFloat(item.key, newValue);
		if (GUILayout.Button("Delete")) Remove(item.key);
		EditorGUILayout.EndHorizontal();
	}

	private void DrawInt(FileBasedPrefsSaveFileModel.IntItem item)
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(item.key);
		int newValue = EditorGUILayout.DelayedIntField(item.value);
		FileBasedPrefs.SetInt(item.key, newValue);
		if (GUILayout.Button("Delete")) Remove(item.key);
		EditorGUILayout.EndHorizontal();
	}

	private void DrawBool(FileBasedPrefsSaveFileModel.BoolItem item)
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(item.key);
		bool newValue = EditorGUILayout.Toggle(item.value);
		FileBasedPrefs.SetBool(item.key, newValue);
		if (GUILayout.Button("Delete")) Remove(item.key);
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