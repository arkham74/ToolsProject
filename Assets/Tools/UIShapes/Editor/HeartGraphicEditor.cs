using UnityEngine;
using UnityEditor;

namespace JD.Editor
{
	[CustomEditor(typeof(HeartGraphic))]
	[CanEditMultipleObjects]
	public class HeartGraphicEditor : ShapeGraphicEditor
	{
		protected override void OnShapeGUI()
		{
			// EditorGUILayout.PropertyField(serializedObject.FindProperty("fill"));
			// EditorGUILayout.PropertyField(serializedObject.FindProperty("round"));
		}
	}
}