using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildSettingsQueue", menuName = "ScriptableObject/Build/Queue", order = 0)]
public class BuildSettingsQueue : ScriptableObject
{
	public BuildSettings[] queue;

	public async void BuildAll()
	{
		BuildSettings.IncrementVersion();
		foreach (BuildSettings buildSettings in queue)
		{
			await buildSettings.BuildAsync();
		}
	}
}

[CustomEditor(typeof(BuildSettingsQueue))]
public class BuildSettingsQueueEditor : Editor
{
	private SerializedProperty queueProperty;
	private BuildSettingsQueue buildSettingsTarget;

	private void OnEnable()
	{
		buildSettingsTarget = target as BuildSettingsQueue;
		queueProperty = serializedObject.FindProperty(nameof(buildSettingsTarget.queue));
	}

	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(queueProperty);
		if (GUILayout.Button("Build All")) buildSettingsTarget.BuildAll();

		try
		{
			for (int i = 0; i < queueProperty.arraySize; i++)
			{
				SerializedProperty elem = queueProperty.GetArrayElementAtIndex(i);
				if (!elem.objectReferenceValue) continue;
				SerializedObject so = new SerializedObject(elem.objectReferenceValue);
				so.Update();
				SerializedProperty iterator = so.GetIterator();
				iterator.NextVisible(true);
				while (iterator.NextVisible(false)) EditorGUILayout.PropertyField(iterator, false);
				so.ApplyModifiedProperties();
			}
		}
		catch (NullReferenceException)
		{
		}

		EditorGUI.EndChangeCheck();
	}
}