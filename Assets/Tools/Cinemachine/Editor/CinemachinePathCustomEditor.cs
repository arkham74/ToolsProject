using System;
using Cinemachine;
using UnityEditor;
using UnityEngine;

namespace JD.Editor
{
	[CustomEditor(typeof(CinemachinePathCustom), true)]
	public class CinemachinePathCustomEditor : UnityEditor.Editor
	{
		private SerializedProperty waypointsProperty;
		private SerializedProperty widthProperty;
		private SerializedProperty pathColorProperty;
		private SerializedProperty resolutionProperty;

		private int selected;

		private void OnEnable()
		{
			SerializedProperty property = serializedObject.FindProperty("m_Appearance");
			widthProperty = property.FindPropertyRelative("width");
			pathColorProperty = property.FindPropertyRelative("pathColor");
			waypointsProperty = serializedObject.FindProperty("waypoints");
			resolutionProperty = serializedObject.FindProperty("m_Resolution");
		}

		private void OnSceneGUI()
		{
			serializedObject.Update();

			const CinemachinePathBase.PositionUnits units = CinemachinePathBase.PositionUnits.Normalized;

			Handles.color = pathColorProperty.colorValue;
			CinemachinePathCustom path = (CinemachinePathCustom)target;
			int res = resolutionProperty.intValue;
			float thickness = widthProperty.floatValue;

			for (int i = 0; i < res; i++)
			{
				float t1 = (i + 0f) / res;
				float t2 = (i + 1f) / res;
				Vector3 p1 = path.EvaluatePositionAtUnit(t1, units);
				Vector3 p2 = path.EvaluatePositionAtUnit(t2, units);
				Handles.DrawLine(p1, p2, thickness);
			}

			Handles.matrix = path.transform.localToWorldMatrix;
			Handles.color = Color.white;
			for (int i = 0; i < waypointsProperty.arraySize; i++)
			{
				SerializedProperty elem = waypointsProperty.GetArrayElementAtIndex(i);
				Vector3 pos = elem.vector3Value;
				const float BALL_SIZE = 0.2f;
				float s = HandleUtility.GetHandleSize(pos) * BALL_SIZE;

				if (selected == i)
				{
					EditorGUI.BeginChangeCheck();
					Vector3 newPos = Handles.PositionHandle(pos, Quaternion.identity);
					if (EditorGUI.EndChangeCheck())
					{
						elem.vector3Value = newPos;
						serializedObject.ApplyModifiedProperties();
					}
				}
				else
				{
					if (Handles.Button(pos, Quaternion.identity, s, s * 1.5f, Handles.SphereHandleCap))
					{
						selected = i;
					}
				}
			}
		}
	}
}
