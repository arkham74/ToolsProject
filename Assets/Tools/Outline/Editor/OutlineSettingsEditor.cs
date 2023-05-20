// using System;
// using System.Linq;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.Audio;
// using UnityEngine.Events;
// using UnityEngine.SceneManagement;
// using UnityEngine.Serialization;
// using TMPro;
// using JD;
// using Freya;
// using Random = UnityEngine.Random;
// using Text = TMPro.TextMeshProUGUI;
// using Tools = JD.Tools;
// using UnityEditor;
// using UnityEditorInternal;

// #if ENABLE_INPUT_SYSTEM
// using UnityEngine.InputSystem;
// #endif

// namespace JD.Outline.Editor
// {
// 	[CustomEditor(typeof(OutlineSettings))]
// 	public class OutlineSettingsEditor : UnityEditor.Editor
// 	{
// 		private SerializedProperty settingsList;
// 		private ReorderableList reorderableList;

// 		private void OnEnable()
// 		{
// 			settingsList = serializedObject.FindProperty("settings");
// 			reorderableList = new ReorderableList(serializedObject, settingsList, false, true, true, true);
// 			reorderableList.drawElementCallback += DrawElementCallback;
// 			reorderableList.elementHeightCallback += ElementHeightCallback;
// 			reorderableList.drawHeaderCallback += DrawHeaderCallback;
// 		}

// 		private void OnDisable()
// 		{
// 			reorderableList.drawElementCallback -= DrawElementCallback;
// 			reorderableList.elementHeightCallback -= ElementHeightCallback;
// 			reorderableList.drawHeaderCallback -= DrawHeaderCallback;
// 		}

// 		private void DrawHeaderCallback(Rect rect)
// 		{
// 			EditorGUI.LabelField(rect, "Settings");
// 		}

// 		private float ElementHeightCallback(int index)
// 		{
// 			float size = 0;
// 			if (settingsList.arraySize > 0)
// 			{
// 				SerializedProperty property = settingsList.GetArrayElementAtIndex(index);
// 				foreach (SerializedProperty item in property)
// 				{
// 					size += EditorGUI.GetPropertyHeight(item) + EditorGUIUtility.standardVerticalSpacing;
// 				}
// 			}
// 			return size;
// 		}

// 		private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
// 		{
// 			SerializedProperty property = settingsList.GetArrayElementAtIndex(index);
// 			float offset = EditorGUIUtility.standardVerticalSpacing;
// 			foreach (SerializedProperty item in property)
// 			{
// 				var r = rect;
// 				r.y += offset;
// 				r.height = EditorGUI.GetPropertyHeight(item);
// 				EditorGUI.PropertyField(r, item);
// 				offset += r.height + EditorGUIUtility.standardVerticalSpacing;
// 			}
// 		}

// 		public override void OnInspectorGUI()
// 		{
// 			// DrawDefaultInspector();
// 			serializedObject.Update();
// 			reorderableList.DoLayoutList();
// 			serializedObject.ApplyModifiedProperties();
// 		}
// 	}
// }