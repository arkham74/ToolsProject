﻿using System;
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

			string[] select = collection.Where(e => !e.IsAbstract).Select(e => e.FullName).ToArray();
			property.NextVisible(true);
			int index = select.IndexOf(property.stringValue);
			index = EditorGUI.Popup(position, label.text, index, select);
			property.stringValue = select.AtIndexClamp(index);

			EditorGUI.EndProperty();
		}
	}
}