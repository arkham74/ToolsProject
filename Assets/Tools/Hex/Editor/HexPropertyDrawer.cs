using UnityEditor;
using UnityEngine;

namespace JD
{
	[CustomPropertyDrawer(typeof(Hex))]
	public class HexPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
		{
			EditorGUI.PropertyField(rect, property.FindPropertyRelative("qr"), label);
		}
	}
}