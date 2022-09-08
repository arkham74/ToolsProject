using UnityEditor;
using UnityEngine;

namespace SerializableDictionary.Editor
{
	[CustomPropertyDrawer(typeof(SerializableDictionary<,>.KeyValuePair))]
	public class KeyValuePairDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			position = EditorGUI.PrefixLabel(position, label);

			Vector2 size = position.size;
			size.x /= 2f;
			size.x -= EditorGUIUtility.standardVerticalSpacing * 0.5f;
			position.size = size;

			EditorGUI.PropertyField(position, property.FindPropertyRelative("key"), GUIContent.none);
			position.x += position.size.x + EditorGUIUtility.standardVerticalSpacing;
			EditorGUI.PropertyField(position, property.FindPropertyRelative("value"), GUIContent.none);

			EditorGUI.EndProperty();
		}
	}
}
