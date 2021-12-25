using UnityEditor;
using UnityEngine;

public static class EditorTools
{
	public static T ObjectField<T>(string label, Object obj, bool allowSceneObjects = false,
		params GUILayoutOption[] options) where T : Object
	{
		return (T) EditorGUILayout.ObjectField(label, obj, typeof(T), allowSceneObjects, options);
	}
}