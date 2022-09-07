using JD;
using UnityEditor;
using UnityEngine;

namespace JD
{
	[CustomPropertyDrawer(typeof(TypeAttribute))]
	public class TypeAttributeDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType == SerializedPropertyType.String)
			{
				string[] types = (attribute as TypeAttribute).Types;
				int index = types.IndexOf(property.stringValue);
				index = EditorGUI.Popup(position, label.text, index, types);
				property.stringValue = types[index];
			}
			else
			{
				EditorGUI.PropertyField(position, property, label);
			}
		}
	}
}
