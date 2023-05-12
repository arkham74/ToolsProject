using UnityEngine;
using UnityEditor;

namespace JD.Editor
{
	[CustomEditor(typeof(StarGraphic))]
	[CanEditMultipleObjects]
	public class StarGraphicEditor : ShapeGraphicEditor
	{
		protected override void OnShapeGUI()
		{
			EditorGUILayout.PropertyField(serializedObject.FindProperty("sides"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("star"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("round"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("radius"));
			SerializedProperty property = serializedObject.FindProperty("empty");
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(property, new GUIContent("Fill"));
			if (property.boolValue)
				EditorGUILayout.PropertyField(serializedObject.FindProperty("fill"), GUIContent.none);
			EditorGUILayout.EndHorizontal();
		}
	}
}