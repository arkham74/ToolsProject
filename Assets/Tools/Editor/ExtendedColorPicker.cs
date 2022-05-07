using System;
using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace Michsky.UI.Shift
{
	[CustomPropertyDrawer(typeof(Color))]
	[CustomPropertyDrawer(typeof(Color32))]
	public class ExtendedColorPicker : PropertyDrawer
	{
		public static Color colorValue = Color.white;
		private const float hexFW = 70f;
		private const float alphaFW = 35f;
		private const float spacing = 5f;

		// public static string ColorToString(Color32 color)
		// {
		// return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2") + color.a.ToString("X2");
		// }

		// public static Color32 StringToColor(string colorStringValue)
		// {
		// int number = int.Parse(colorStringValue, NumberStyles.HexNumber);

		// Color32 colorResult;

		// if (colorStringValue.Length == 8)
		// 	colorResult = new Color32((byte)(number >> 24 & 255), (byte)(number >> 16 & 255), (byte)(number >> 8 & 255), (byte)(number & 255));

		// else
		// {
		// 	if (colorStringValue.Length == 6)
		// 		colorResult = new Color32((byte)(number >> 16 & 255), (byte)(number >> 8 & 255), (byte)(number & 255), 255);

		// 	else
		// 	{
		// 		if (colorStringValue.Length == 4)
		// 			colorResult = new Color32((byte)((number >> 12 & 15) * 17), (byte)((number >> 8 & 15) * 17), (byte)((number >> 4 & 15) * 17), (byte)((number & 15) * 17));

		// 		else
		// 		{
		// 			if (colorStringValue.Length != 3)
		// 				throw new FormatException("Supports only RRGGBBAA, RRGGBB, RGBA, RGB formats");

		// 			colorResult = new Color32((byte)((number >> 8 & 15) * 17), (byte)((number >> 4 & 15) * 17), (byte)((number & 15) * 17), 255);
		// 		}
		// 	}
		// }
		// return colorResult;
		// }

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent title)
		{
			title = EditorGUI.BeginProperty(position, title, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), title);

			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			float colorWidth = position.width - hexFW - spacing - alphaFW - spacing;

			Color32 color = property.colorValue;
			Color color2 = EditorGUI.ColorField(new Rect(position.x, position.y, colorWidth, position.height), property.colorValue);

			if (!color2.Equals(color))
				property.colorValue = color = color2;

			string colorStringValue = EditorGUI.TextField(new Rect(position.x + colorWidth + spacing, position.y, hexFW, position.height), ColorUtility.ToHtmlStringRGBA(color));

			if (ColorUtility.TryParseHtmlString(colorStringValue, out color2))
			{
				if (!color2.Equals(color))
					property.colorValue = color2;
			}
			if (ColorUtility.TryParseHtmlString("#" + colorStringValue, out color2))
			{
				if (!color2.Equals(color))
					property.colorValue = color2;
			}

			float a = property.colorValue.a.RoundToDecimal(2);
			float newAlpha = EditorGUI.FloatField(new Rect(position.x + colorWidth + hexFW + (spacing * 2f), position.y, alphaFW, position.height), a);
			//, 0f, 1f

			if (!newAlpha.Equals(property.colorValue.a))
				property.colorValue = new Color(property.colorValue.r, property.colorValue.g, property.colorValue.b, newAlpha);

			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}
	}
}