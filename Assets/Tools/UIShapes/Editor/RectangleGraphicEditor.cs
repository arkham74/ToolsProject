using UnityEngine;
using UnityEditor;

namespace JD.Editor
{
	[CustomEditor(typeof(RectangleGraphic))]
	public class RectangleGraphicEditor : ShapeGraphicEditor
	{
		protected override void OnShapeGUI()
		{
			EditorGUILayout.PropertyField(serializedObject.FindProperty("width"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("height"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("radius1"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("radius2"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("radius3"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("radius4"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("fill"));
		}
	}
}