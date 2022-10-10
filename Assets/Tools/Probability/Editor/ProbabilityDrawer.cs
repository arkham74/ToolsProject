using UnityEngine;
using UnityEditor;

namespace JD.Editor
{
	[CustomPropertyDrawer(typeof(Probability<>))]
	public class ProbabilityDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			SerializedProperty sp = property.FindPropertyRelative("elements");
			EditorGUI.PropertyField(position, sp, label);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("elements"));
		}
	}
}
