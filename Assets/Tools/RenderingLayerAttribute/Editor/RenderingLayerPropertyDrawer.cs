using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

namespace JD.Editor
{
	[CustomPropertyDrawer(typeof(RenderingLayerAttribute))]
	public class RenderingLayerPropertyDrawer : PropertyDrawer
	{
		private const string msg = " must be an uint";

		// public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		// {
		// 	bool isInt = property.propertyType == SerializedPropertyType.Integer;
		// 	float height = base.GetPropertyHeight(property, label);
		// 	return isInt ? height : height * 2;
		// }

		public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(rect, label, property);

			if (property.propertyType == SerializedPropertyType.Integer)
			{
				DrawPropertyForInt(rect, property, label);
			}
			else
			{
				EditorGUI.HelpBox(rect, property.name + msg, MessageType.Warning);
			}

			EditorGUI.EndProperty();
		}

		private static void DrawPropertyForInt(Rect rect, SerializedProperty property, GUIContent label)
		{
			string[] layers = GraphicsSettings.defaultRenderPipeline.renderingLayerMaskNames;
			long mask = property.longValue;
			uint value = (uint)EditorGUI.MaskField(rect, label, (int)mask, layers);

			if (property.longValue != value)
			{
				property.longValue = value;
			}
		}
	}
}
