using UnityEditor;

namespace UIExtensions.Editor
{
	[CustomEditor(typeof(FlexibleGridLayout))]
	public class FlexibleGridLayoutEditor : UnityEditor.Editor
	{
		private SerializedProperty rowsProperty;
		private SerializedProperty columnsProperty;
		private SerializedProperty fitTypeProperty;
		private SerializedProperty spacingProperty;
		private SerializedProperty paddingProperty;

		private void OnEnable()
		{
			serializedObject.Update();

			rowsProperty = serializedObject.FindProperty("rows");
			columnsProperty = serializedObject.FindProperty("columns");
			fitTypeProperty = serializedObject.FindProperty("fitType");
			spacingProperty = serializedObject.FindProperty("spacing");
			paddingProperty = serializedObject.FindProperty("m_Padding");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUILayout.PropertyField(paddingProperty);
			EditorGUILayout.PropertyField(spacingProperty);
			EditorGUILayout.PropertyField(fitTypeProperty);

			switch (fitTypeProperty.enumValueIndex)
			{
				case (int)FlexibleGridLayout.FitType.FIXED_ROWS:
					EditorGUILayout.PropertyField(rowsProperty);
					break;
				case (int)FlexibleGridLayout.FitType.FIXED_COLUMNS:
					EditorGUILayout.PropertyField(columnsProperty);
					break;
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}