using UnityEngine;
using UnityEditor;

namespace JD.Editor
{
	[CustomEditor(typeof(SliderGraphic))]
	[CanEditMultipleObjects]
	public class SliderGraphicEditor : ShapeGraphicEditor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("sourceImage"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Color"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("fillColor"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("emission"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Maskable"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("m_RaycastTarget"));
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(serializedObject.FindProperty("m_RaycastPadding").displayName);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("m_RaycastPadding.x"), GUIContent.none);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("m_RaycastPadding.y"), GUIContent.none);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("m_RaycastPadding.z"), GUIContent.none);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("m_RaycastPadding.w"), GUIContent.none);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("direction"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("fill"));
			EditorGUILayout.Separator();
			serializedObject.ApplyModifiedProperties();
		}

		protected override void OnShapeGUI()
		{
		}
	}
}