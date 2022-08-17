using UnityEditor;
using UnityEditor.UI;

namespace JD
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(ContentSizeFitterWithLimit))]
	public class ContentSizeFitterWithLimitEditor : ContentSizeFitterEditor
	{
		private SerializedProperty limitWidthProp;
		private SerializedProperty limitHeightProp;
		private SerializedProperty maxWidthProp;
		private SerializedProperty maxHeightProp;

		protected override void OnEnable()
		{
			base.OnEnable();
			limitWidthProp = serializedObject.FindProperty("limitWidth");
			limitHeightProp = serializedObject.FindProperty("limitHeight");
			maxWidthProp = serializedObject.FindProperty("maxWidth");
			maxHeightProp = serializedObject.FindProperty("maxHeight");
		}

		public override void OnInspectorGUI()
		{
			void ForceRebuild()
			{
				(target as ContentSizeFitterWithLimit).ForceRebuild();
			}

			base.OnInspectorGUI();

			serializedObject.Update();

			if (EditorGUILayout.PropertyField(limitWidthProp))
			{
				ForceRebuild();
			}

			if (limitWidthProp.boolValue)
			{
				if (EditorGUILayout.PropertyField(maxWidthProp))
				{
					ForceRebuild();
				}
			}

			if (EditorGUILayout.PropertyField(limitHeightProp))
			{
				ForceRebuild();
			}

			if (limitHeightProp.boolValue)
			{
				if (EditorGUILayout.PropertyField(maxHeightProp))
				{
					ForceRebuild();
				}
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}