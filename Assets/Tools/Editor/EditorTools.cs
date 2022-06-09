using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public static class EditorTools
{
	public static T ObjectField<T>(string label, T obj, bool allowSceneObjects = false, params GUILayoutOption[] options) where T : Object
	{
		return (T)EditorGUILayout.ObjectField(label, obj, typeof(T), allowSceneObjects, options);
	}

	public static T ObjectField<T>(GUIContent label, T obj, bool allowSceneObjects = false, params GUILayoutOption[] options) where T : Object
	{
		return (T)EditorGUILayout.ObjectField(label, obj, typeof(T), allowSceneObjects, options);
	}

	public static T ObjectField<T>(T obj, bool allowSceneObjects = false, params GUILayoutOption[] options) where T : Object
	{
		return (T)EditorGUILayout.ObjectField(obj, typeof(T), allowSceneObjects, options);
	}
}