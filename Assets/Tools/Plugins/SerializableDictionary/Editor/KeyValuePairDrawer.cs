using UnityEditor;
using UnityEngine;

namespace SerializableDictionary.Editor
{
	[CustomPropertyDrawer(typeof(SerializableDictionary<,>.KeyValuePair))]
	public class KeyValuePairDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			Vector2 size = position.size;
			size.x /= 3f;
			size.x -= EditorGUIUtility.standardVerticalSpacing / 2f;
			position.size = size;

			EditorGUI.LabelField(position, label);
			position.x += position.size.x + EditorGUIUtility.standardVerticalSpacing;
			EditorGUI.PropertyField(position, property.FindPropertyRelative("key"), GUIContent.none);
			position.x += position.size.x + EditorGUIUtility.standardVerticalSpacing;
			EditorGUI.PropertyField(position, property.FindPropertyRelative("value"), GUIContent.none);
		}
	}
}
