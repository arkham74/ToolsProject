using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using JD;
using Random = UnityEngine.Random;
using UnityEditor;
using UnityEditorInternal;

namespace JD.Outline.Editor
{
	[CustomEditor(typeof(OutlineFeature))]
	public class OutlineFeatureEditor : UnityEditor.Editor
	{
		private SerializedProperty settingsProp;

		private void OnEnable()
		{
			settingsProp = serializedObject.FindProperty("settings");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			if (GUILayout.Button("Add outline settings"))
			{
				settingsProp.InsertArrayElementAtIndex(settingsProp.arraySize);
			}
			for (int i = 0; i < settingsProp.arraySize; i++)
			{
				SerializedProperty property = settingsProp.GetArrayElementAtIndex(i);
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PropertyField(property, GUIContent.none);
				if (GUILayout.Button("Remove outline settings"))
				{
					settingsProp.DeleteArrayElementAtIndex(i);
				}
				EditorGUILayout.EndHorizontal();
			}
			serializedObject.ApplyModifiedProperties();
		}
	}
}