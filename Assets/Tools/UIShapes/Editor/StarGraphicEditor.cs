using UnityEngine;
using UnityEditor;

namespace JD.Editor
{
	[CustomEditor(typeof(StarGraphic))]
	public class StarGraphicEditor : ShapeGraphicEditor
	{
		protected override void OnShapeGUI()
		{
			EditorGUILayout.PropertyField(serializedObject.FindProperty("sides"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("star"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("fill"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("round"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("radius"));
		}
	}
}