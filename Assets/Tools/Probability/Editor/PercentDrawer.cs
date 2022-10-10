using UnityEngine;
using UnityEditor;

namespace JD.Editor
{
	[CustomPropertyDrawer(typeof(Probability<>.Percent))]
	public class PercentDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			SerializedProperty sp1 = property.FindPropertyRelative("item");
			SerializedProperty sp2 = property.FindPropertyRelative("chance");
			position.width /= 2f;
			position.width -= EditorGUIUtility.standardVerticalSpacing;
			position.height -= EditorGUIUtility.standardVerticalSpacing;
			EditorGUI.PropertyField(position, sp1, GUIContent.none);
			position.x += position.width + EditorGUIUtility.standardVerticalSpacing;
			EditorGUI.PropertyField(position, sp2, GUIContent.none);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 1;
		}
	}
}
