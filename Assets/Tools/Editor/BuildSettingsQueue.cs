using System;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace JD.Editor
{
	[CreateAssetMenu(fileName = "BuildSettingsQueue", menuName = "ScriptableObject/Build/Queue", order = 0)]
	public class BuildSettingsQueue : ScriptableObject
	{
		[SerializeField] private BuildSettings[] queue;

		private void Reset()
		{
			queue = AssetTools.FindAssetsByType<BuildSettings>();
		}

		public async void BuildAll()
		{
			foreach (BuildSettings buildSettings in queue)
			{
				BuildReport report = await buildSettings.BuildAsync();
				if (report.summary.result != BuildResult.Succeeded)
				{
					return;
				}
			}
		}

		public async void BuildAllDev()
		{
			foreach (BuildSettings buildSettings in queue)
			{
				BuildReport report = await buildSettings.BuildAsync(BuildOptions.Development);
				if (report.summary.result != BuildResult.Succeeded)
				{
					return;
				}
			}
		}
	}

	[CustomEditor(typeof(BuildSettingsQueue))]
	public class BuildSettingsQueueEditor : UnityEditor.Editor
	{
		private SerializedProperty queueProperty;
		private BuildSettingsQueue buildSettingsTarget;

		private void OnEnable()
		{
			buildSettingsTarget = target as BuildSettingsQueue;
			queueProperty = serializedObject.FindProperty("queue");
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			EditorGUI.BeginChangeCheck();
			if (GUILayout.Button("Build All")) buildSettingsTarget.BuildAll();
			if (GUILayout.Button("Build All Dev")) buildSettingsTarget.BuildAllDev();

			try
			{
				for (int i = 0; i < queueProperty.arraySize; i++)
				{
					SerializedProperty elem = queueProperty.GetArrayElementAtIndex(i);
					if (elem.objectReferenceValue)
					{
						SerializedObject so = new SerializedObject(elem.objectReferenceValue);
						so.Update();
						SerializedProperty iterator = so.GetIterator();
						iterator.NextVisible(true);
						while (iterator.NextVisible(false))
						{
							EditorGUILayout.PropertyField(iterator, true);
						}
						so.ApplyModifiedProperties();
					}
				}
			}
			catch (NullReferenceException)
			{
			}

			EditorGUI.EndChangeCheck();
		}
	}
}