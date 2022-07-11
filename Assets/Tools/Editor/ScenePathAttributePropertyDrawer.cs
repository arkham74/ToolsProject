using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ScenePathAttribute))]
public class ScenePathAttributePropertyDrawer : PropertyDrawer
{
	private static readonly string[] Folders = { "Assets/Scenes" };
	private static string[] scenePaths;
	private static string[] sceneNames;
	private static string[] guids;

	public override void OnGUI(Rect rect, SerializedProperty serializedProperty, GUIContent label)
	{
		if (serializedProperty.propertyType != SerializedPropertyType.String) return;
		guids ??= AssetDatabase.FindAssets("t:scene", Folders);
		scenePaths ??= GetPaths(guids);
		sceneNames ??= GetNames(scenePaths);
		int index = Mathf.Max(scenePaths.IndexOf(serializedProperty.stringValue), 0);
		int newIndex = EditorGUI.Popup(rect, index, sceneNames);
		serializedProperty.stringValue = scenePaths.ElementAtOrDefault(newIndex);
	}

	private static string[] GetPaths(IEnumerable<string> guidsParam)
	{
		return guidsParam.Select(AssetDatabase.GUIDToAssetPath).ToArray();
	}

	private static string[] GetNames(IReadOnlyList<string> paths)
	{
		int len = paths.Count;
		string[] names = new string[len];
		for (int i = 0; i < len; i++) names[i] = Path.GetFileNameWithoutExtension(paths[i]);
		return names;
	}
}