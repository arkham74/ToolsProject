// #define HUNT_ADDRESSABLES

using System.Reflection;
using UnityEditor;
#if UNITY_2021_2_OR_NEWER
using UnityEditor.Build;
#endif
#if HUNT_ADDRESSABLES
using UnityEditor.AddressableAssets;
#endif
using UnityEngine;
using UnityEngine.U2D;

// ReSharper disable once CheckNamespace
namespace DependenciesHunter
{
	public static class GUIUtilities
	{
		private static void HorizontalLine(
				int marginTop,
				int marginBottom,
				int height,
				Color color
		)
		{
			EditorGUILayout.BeginHorizontal();
			var rect = EditorGUILayout.GetControlRect(
					false,
					height,
					new GUIStyle { margin = new RectOffset(0, 0, marginTop, marginBottom) }
			);

			EditorGUI.DrawRect(rect, color);
			EditorGUILayout.EndHorizontal();
		}

		public static void HorizontalLine(
				int marginTop = 5,
				int marginBottom = 5,
				int height = 2
		)
		{
			HorizontalLine(marginTop, marginBottom, height, new Color(0.5f, 0.5f, 0.5f, 1));
		}
	}
}