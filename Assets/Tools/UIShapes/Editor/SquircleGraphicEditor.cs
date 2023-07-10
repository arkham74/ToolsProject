using UnityEngine;
using UnityEditor;

namespace JD.Editor
{
	[CustomEditor(typeof(SquircleGraphic))]
	[CanEditMultipleObjects]
	public class SquircleGraphicEditor : ShapeGraphicEditor
	{
		protected override void OnShapeGUI()
		{
			EditorGUILayout.PropertyField(serializedObject.FindProperty("radius"));
			SerializedProperty property = serializedObject.FindProperty("fill");
			EditorGUILayout.PropertyField(property);
			if (!property.boolValue)
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty("width"));
			}
			EditorGUILayout.PropertyField(serializedObject.FindProperty("scale"));
		}
	}
}