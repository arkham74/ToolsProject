using UnityEngine;
using UnityEditor;

namespace JD.Editor
{
	public abstract class ShapeGraphicEditor : UnityEditor.Editor
	{
		protected abstract void OnShapeGUI();

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("sourceImage"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Color"));
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
			EditorGUILayout.Separator();
			OnShapeGUI();
			serializedObject.ApplyModifiedProperties();
		}
	}
}