using System;
using System.Collections.Generic;
using System.Linq;
using JD;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

namespace JD.Editor
{
	[CustomPropertyDrawer(typeof(SerializedType<>))]
	public class SerializedTypeDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			Type elementType = fieldInfo.FieldType;

			if (elementType.IsArray)
			{
				elementType = elementType.GetElementType();
			}

			Type argument = elementType.GetGenericArguments()[0];

			TypeCache.TypeCollection collection = TypeCache.GetTypesDerivedFrom(argument);

			if (collection.Count <= 0)
			{
				collection = TypeCache.GetTypesDerivedFrom(argument.GetGenericArguments()[0]);
			}

			IEnumerable<Type> enumerable = collection.Where(e => !e.IsAbstract);
			string[] displayName = enumerable.Select(e => e.Name).ToArray();
			string[] typeName = enumerable.Select(e => e.AssemblyQualifiedName).ToArray();
			property.NextVisible(true);

			int indexType = Array.IndexOf(typeName, property.stringValue);
			int indexDisplay = Array.IndexOf(displayName, property.stringValue);
			int index = Mathf.Max(indexType, indexDisplay);

			index = EditorGUI.Popup(position, label.text, index, displayName);
			int v = Mathf.Clamp(index, 0, typeName.Length - 1);
			property.stringValue = typeName[v];

			EditorGUI.EndProperty();
		}
	}
}
