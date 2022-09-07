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
			IEnumerable<string> select = collection.Where(e => !e.IsAbstract).Select(e => e.FullName);
			string[] types = select.ToArray();
			property.NextVisible(true);
			SerializedProperty relative = property.Copy();
			int index = types.IndexOf(relative.stringValue);
			index = EditorGUI.Popup(position, label.text, index, types);
			relative.stringValue = types[Mathf.Max(index, 0)];
		}
	}
}
