using UnityEngine;
using UnityEditor;
using System.Collections;

namespace JD.Editor
{
	[CustomPropertyDrawer(typeof(ContactFilter2D))]
	public class ContactFilter2DDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			SerializedProperty useTriggers = property.FindPropertyRelative("useTriggers");
			SerializedProperty useLayerMask = property.FindPropertyRelative("useLayerMask");
			SerializedProperty useDepth = property.FindPropertyRelative("useDepth");
			SerializedProperty useOutsideDepth = property.FindPropertyRelative("useOutsideDepth");

			SerializedProperty useNormalAngle = property.FindPropertyRelative("useNormalAngle");
			SerializedProperty useOutsideNormalAngle = property.FindPropertyRelative("useOutsideNormalAngle");
			SerializedProperty layerMask = property.FindPropertyRelative("layerMask");

			SerializedProperty minDepth = property.FindPropertyRelative("minDepth");
			SerializedProperty maxDepth = property.FindPropertyRelative("maxDepth");
			SerializedProperty minNormalAngle = property.FindPropertyRelative("minNormalAngle");
			SerializedProperty maxNormalAngle = property.FindPropertyRelative("maxNormalAngle");

			EditorGUILayout.PropertyField(useTriggers);

			EditorGUILayout.PropertyField(useLayerMask);
			GUI.enabled = useLayerMask.boolValue;
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(layerMask);
			EditorGUI.indentLevel--;
			EditorGUILayout.Separator();
			GUI.enabled = true;

			EditorGUILayout.PropertyField(useDepth);
			GUI.enabled = useDepth.boolValue;
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(minDepth);
			EditorGUILayout.PropertyField(maxDepth);
			EditorGUILayout.PropertyField(useOutsideDepth);
			EditorGUI.indentLevel--;
			EditorGUILayout.Separator();
			GUI.enabled = true;

			EditorGUILayout.PropertyField(useNormalAngle);
			GUI.enabled = useNormalAngle.boolValue;
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(minNormalAngle);
			EditorGUILayout.PropertyField(maxNormalAngle);
			EditorGUILayout.PropertyField(useOutsideNormalAngle);
			EditorGUI.indentLevel--;
			EditorGUILayout.Separator();
			GUI.enabled = true;
		}
	}
}