using UnityEditor;
using UnityEngine;

namespace UIExtensions.Editor
{
	[CustomEditor(typeof(FlexibleGridLayout))]
	[CanEditMultipleObjects]
	public class FlexibleGridLayoutEditor : UnityEditor.Editor
	{
		private SerializedProperty paddingProperty;
		private SerializedProperty cornerProperty;
		private SerializedProperty axisProperty;
		private SerializedProperty spacingProperty;
		private SerializedProperty fitTypeProperty;
		private SerializedProperty rowsColumnsProperty;


		private void OnEnable()
		{
			paddingProperty = serializedObject.FindProperty("m_Padding");
			cornerProperty = serializedObject.FindProperty("startCorner");
			axisProperty = serializedObject.FindProperty("startAxis");
			spacingProperty = serializedObject.FindProperty("spacing");
			fitTypeProperty = serializedObject.FindProperty("fitType");
			rowsColumnsProperty = serializedObject.FindProperty("rowsColumns");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUILayout.PropertyField(paddingProperty);
			EditorGUILayout.PropertyField(spacingProperty);
			EditorGUILayout.PropertyField(cornerProperty);
			EditorGUILayout.PropertyField(axisProperty);
			EditorGUILayout.PropertyField(fitTypeProperty, new GUIContent("Constraint"));
			switch (fitTypeProperty.enumValueIndex)
			{
				case (int)FlexibleGridLayout.FitType.FixedRows:
					EditorGUILayout.PropertyField(rowsColumnsProperty, new GUIContent("Rows"));
					break;
				case (int)FlexibleGridLayout.FitType.FixedColumns:
					EditorGUILayout.PropertyField(rowsColumnsProperty, new GUIContent("Columns"));
					break;
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}