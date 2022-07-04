using System.Linq;
using UnityEditor;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class DeviceAttribute : PropertyAttribute
{
}

#if UNITY_EDITOR && ENABLE_INPUT_SYSTEM
[CustomPropertyDrawer(typeof(DeviceAttribute))]
public class DeviceAttributePropertyDrawer : PropertyDrawer
{
	private string[] list;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		if (property.propertyType == SerializedPropertyType.String)
		{
			list ??= TypeCache.GetTypesDerivedFrom<InputDevice>().Select(e => e.Name).ToArray();
			int index = list.IndexOf(property.stringValue);
			index = EditorGUI.Popup(position, label.text, index, list);
			property.stringValue = list[index.Mod(list.Length)];
		}
		else
		{
			EditorGUI.PropertyField(position, property, label);
			EditorGUILayout.HelpBox("Device attribute only supported on string types", MessageType.Warning);
		}
	}
}
#endif