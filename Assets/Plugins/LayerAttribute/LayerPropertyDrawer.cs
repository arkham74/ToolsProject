using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LayerAttribute))]
public class LayerPropertyDrawer : PropertyDrawer
{
	public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
	{
		switch (property.propertyType)
		{
			case SerializedPropertyType.String:
				List<string> layersList = UnityEditorInternal.InternalEditorUtility.layers.ToList();
				int index = layersList.IndexOf(property.stringValue);
				index = Mathf.Max(index, 0);
				index = EditorGUI.Popup(rect, label.text, index, layersList.ToArray());
				property.stringValue = layersList[index];
				break;
			default:
				EditorGUI.PropertyField(rect, property, label);
				EditorGUILayout.HelpBox("Layer attribute only supports string fields", MessageType.Error);
				break;
		}
	}
}