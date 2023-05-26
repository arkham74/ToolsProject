using UnityEditor;
using UnityEngine;

namespace JD.Editor
{
	/// <summary>
	/// Draws the generic dictionary a bit nicer than Unity would natively (not as many expand-arrows
	/// and better spacing between KeyValue pairs). Also renders a warning-box if there are duplicate
	/// keys in the dictionary.
	/// </summary>
	[CustomPropertyDrawer(typeof(SerializableDictionary<,>))]
	public class SerializableDictionaryPropertyDrawer : PropertyDrawer
	{
		private static readonly float LineHeight = EditorGUIUtility.singleLineHeight;
		private static readonly float VertSpace = EditorGUIUtility.standardVerticalSpacing;

		public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(pos, label, property);
			// Draw list.
			SerializedProperty list = property.FindPropertyRelative("list");
			Rect currentPos = new Rect(LineHeight, pos.y, pos.width, LineHeight);
			EditorGUI.PropertyField(currentPos, list, label, true);

			// Draw key collision warning.
			bool keyCollision = property.FindPropertyRelative("keyCollision").boolValue;
			if (keyCollision)
			{
				currentPos.y += EditorGUI.GetPropertyHeight(list, true) + VertSpace;
				Rect entryPos = new Rect(LineHeight, currentPos.y, pos.width, LineHeight * 2f);
				EditorGUI.HelpBox(entryPos, "Duplicate keys will not be serialized.", MessageType.Warning);
			}

			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			float totHeight = 0f;

			// Height of KeyValue list.
			SerializedProperty listProp = property.FindPropertyRelative("list");
			totHeight += EditorGUI.GetPropertyHeight(listProp, true);

			// Height of key collision warning.
			bool keyCollision = property.FindPropertyRelative("keyCollision").boolValue;
			if (keyCollision)
			{
				totHeight += LineHeight * 2f + VertSpace;
			}

			return totHeight;
		}
	}
}
