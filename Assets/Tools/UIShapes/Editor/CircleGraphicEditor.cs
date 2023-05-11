using UnityEngine;
using UnityEditor;

namespace JD.Editor
{
	[CustomEditor(typeof(CircleGraphic))]
	public class CircleGraphicEditor : ShapeGraphicEditor
	{
		protected override void OnShapeGUI()
		{
			EditorGUILayout.PropertyField(serializedObject.FindProperty("radius"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("fill"));
		}
	}
}