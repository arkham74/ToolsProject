using UnityEngine;
using UnityEditor;

namespace JD.Editor
{
	[CustomEditor(typeof(TriangleGraphic))]
	public class TriangleGraphicEditor : ShapeGraphicEditor
	{
		protected override void OnShapeGUI()
		{
			EditorGUILayout.PropertyField(serializedObject.FindProperty("fill"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("round"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("radius"));
		}
	}
}