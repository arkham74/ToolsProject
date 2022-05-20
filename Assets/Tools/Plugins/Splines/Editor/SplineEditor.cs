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
using NaughtyAttributes;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tag = NaughtyAttributes.TagAttribute;
using UnityEditor;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

[CustomEditor(typeof(Spline))]
public class SplineEditor : Editor
{
	private SerializedProperty segmentsProp;
	private SerializedProperty mirrorProp;
	private Transform transform;
	private int selectedIndex = -1;

	private void OnEnable()
	{
		segmentsProp = serializedObject.FindProperty("segments");
		mirrorProp = serializedObject.FindProperty("mirrorTangents");
		transform = (target as Component).transform;
	}

	// private void OnDisable()
	// {
	// }

	// public override void OnInspectorGUI()
	// {
	// 	DrawDefaultInspector();
	// }

	private void OnSceneGUI()
	{
		serializedObject.Update();
		Handles.matrix = transform.localToWorldMatrix;
		bool applyChanges = false;

		if (segmentsProp.arraySize > 0)
		{
			for (int i = 1; i < segmentsProp.arraySize; i++)
			{
				SerializedProperty elemProp1 = segmentsProp.GetArrayElementAtIndex(i - 1);
				SerializedProperty elemProp2 = segmentsProp.GetArrayElementAtIndex(i);

				SerializedProperty pointProp1 = elemProp1.FindPropertyRelative("point");
				SerializedProperty leftProp1 = elemProp1.FindPropertyRelative("left");
				SerializedProperty rightProp1 = elemProp1.FindPropertyRelative("right");

				SerializedProperty pointProp2 = elemProp2.FindPropertyRelative("point");
				SerializedProperty leftProp2 = elemProp2.FindPropertyRelative("left");
				SerializedProperty rightProp2 = elemProp2.FindPropertyRelative("right");

				Vector3 pos1 = pointProp1.vector3Value;
				Vector3 wPos1 = pos1;

				Vector3 left1 = leftProp1.vector3Value + wPos1;
				Vector3 right1 = rightProp1.vector3Value + wPos1;

				Vector3 pos2 = pointProp2.vector3Value;
				Vector3 wPos2 = pos2;

				Vector3 left2 = leftProp2.vector3Value + wPos2;
				Vector3 right2 = rightProp2.vector3Value + wPos2;

				const float size = 2f;
				Handles.DrawBezier(pos1, pos2, left1, right2, Color.green, null, size);

				if (DrawHandles(i * 3, elemProp2))
				{
					applyChanges = true;
				};
			}

			if (DrawHandles(0, segmentsProp.GetArrayElementAtIndex(0)))
			{
				applyChanges = true;
			};
		}

		if (applyChanges)
		{
			// EditorUtility.SetDirty(target);
			serializedObject.ApplyModifiedProperties();
		}
	}

	private bool DrawHandles(int index, SerializedProperty elemProp)
	{
		SerializedProperty pointProp = elemProp.FindPropertyRelative("point");
		SerializedProperty leftProp = elemProp.FindPropertyRelative("left");
		SerializedProperty rightProp = elemProp.FindPropertyRelative("right");

		Vector3 pos = pointProp.vector3Value;
		Vector3 left = leftProp.vector3Value + pos;
		Vector3 right = rightProp.vector3Value + pos;

		float size = 2f;

		Handles.color = Color.red;
		Handles.DrawLine(pos, left, size);
		Handles.DrawLine(pos, right, size);

		if (DrawPoint(index + 0, pointProp, null, Vector3.zero, pos, 0.25f))
		{
			return true;
		}

		if (DrawPoint(index + 1, leftProp, rightProp, pos, left, 0.25f))
		{
			return true;
		}

		if (DrawPoint(index + 2, rightProp, leftProp, pos, right, 0.25f))
		{
			return true;
		}

		return false;
	}

	private bool DrawPoint(int index, SerializedProperty prop, SerializedProperty opProp, Vector3 offset, Vector3 pos, float size)
	{
		Handles.color = Color.white;
		float s = HandleUtility.GetHandleSize(pos) * size;
		if (selectedIndex == index)
		{
			EditorGUI.BeginChangeCheck();
			Vector3 newPos = Handles.PositionHandle(pos, Quaternion.identity);
			// UnityEditor.Tools.current = Tool.None;
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(target, "Segment change");
				prop.vector3Value = newPos - offset;
				if (opProp != null && mirrorProp.boolValue)
					opProp.vector3Value = -(newPos - offset);
				return true;
			}
		}
		else if (Handles.Button(pos, Quaternion.identity, s, s * 1.5f, Handles.SphereHandleCap))
		{
			selectedIndex = index;
		}

		return false;
	}
}