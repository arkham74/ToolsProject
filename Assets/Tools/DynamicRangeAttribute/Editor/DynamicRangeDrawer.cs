using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using TMPro;
using JD;
using Freya;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tools = JD.Tools;
using UnityEditor;

namespace JD.Editor
{
	[CustomPropertyDrawer(typeof(DynamicRangeAttribute))]
	public class DynamicRangeDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			DynamicRangeAttribute dynamicRangeAttribute = attribute as DynamicRangeAttribute;
			SerializedProperty lowProp = property.serializedObject.FindProperty(dynamicRangeAttribute.lowProperty);
			SerializedProperty highProp = property.serializedObject.FindProperty(dynamicRangeAttribute.highProperty);

			if (property.propertyType == SerializedPropertyType.Integer)
			{
				int min = lowProp.intValue;
				int max = highProp.intValue;
				property.intValue = EditorGUI.IntSlider(position, label, property.intValue, min, max);
				property.intValue = Mathf.Clamp(property.intValue, min, max);
			}
			else if (property.propertyType == SerializedPropertyType.Float)
			{
				float min = lowProp.floatValue;
				float max = highProp.floatValue;
				property.floatValue = EditorGUI.Slider(position, label, property.floatValue, min, max);
				property.floatValue = Mathf.Clamp(property.floatValue, min, max);
			}
		}
	}
}