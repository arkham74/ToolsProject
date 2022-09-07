using System;
using System.Collections.Generic;
using System.Linq;
using JD;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

namespace SAR.Editor
{
	[CustomPropertyDrawer(typeof(SerializedType<>))]
	public class SerializedTypeDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			Type argument = fieldInfo.FieldType.GetGenericArguments()[0];
			TypeCache.TypeCollection collection = TypeCache.GetTypesDerivedFrom(argument);
			string[] select = collection.Where(e => !e.IsAbstract).Select(e => e.FullName).ToArray();
			property.NextVisible(true);
			int index = select.IndexOf(property.stringValue);
			index = EditorGUI.Popup(position, label.text, index, select);
			property.stringValue = select[Mathf.Max(index, 0)];
		}
	}
}
