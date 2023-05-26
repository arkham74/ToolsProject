using UnityEditor;
using UnityEngine;

namespace JD.Editor
{
	[CustomPropertyDrawer(typeof(SerializableDictionary<,>.KeyValuePair))]
	public class KeyValuePairDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			// position = EditorGUI.PrefixLabel(position, label);

			Vector2 size = position.size;
			size.x /= 2f;
			size.x -= EditorGUIUtility.standardVerticalSpacing * 0.5f;
			position.size = size;

			EditorGUI.PropertyField(position, property.FindPropertyRelative("key"), GUIContent.none, true);
			position.x += position.size.x + EditorGUIUtility.standardVerticalSpacing;
			EditorGUI.PropertyField(position, property.FindPropertyRelative("value"), GUIContent.none, true);

			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			float height1 = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("key"), true);
			float height2 = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("value"), true);
			return Mathf.Max(height1, height2);
		}
	}
}
