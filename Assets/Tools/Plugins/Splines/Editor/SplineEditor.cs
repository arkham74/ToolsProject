using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Splines
{
	[CustomEditor(typeof(Spline))]
	public class SplineEditor : Editor
	{
		private const float lineSize = 2f;
		private const float ballSize = 0.2f;

		private SerializedProperty listProp;
		private SerializedProperty mirrorProp;
		private SerializedProperty loopProp;
		private Transform transform;
		private int selectedIndex = -1;
		private ReorderableList reorderableList;

		private int ParentIndex
		{
			get => Mathf.FloorToInt(selectedIndex / 3f);
			set => selectedIndex = value * 3;
		}

		private void OnEnable()
		{
			listProp = serializedObject.FindProperty("segments");
			mirrorProp = serializedObject.FindProperty("mirrorTangents");
			loopProp = serializedObject.FindProperty("loop");
			transform = (target as Component).transform;
			reorderableList = new ReorderableList(serializedObject, listProp);
			reorderableList.drawHeaderCallback += DrawListHeader;
			reorderableList.drawElementCallback += DrawListElement;
			reorderableList.elementHeightCallback += ElementHeight;
			reorderableList.onSelectCallback += OnSelect;
			reorderableList.onAddCallback += OnAdd;
		}

		private void OnDisable()
		{
			reorderableList.drawHeaderCallback -= DrawListHeader;
			reorderableList.drawElementCallback -= DrawListElement;
			reorderableList.elementHeightCallback -= ElementHeight;
			reorderableList.onSelectCallback -= OnSelect;
			reorderableList.onAddCallback -= OnAdd;
		}

		private void OnAdd(ReorderableList list)
		{
			int index = listProp.arraySize;
			listProp.InsertArrayElementAtIndex(index);

			SerializedProperty elemProp = listProp.GetArrayElementAtIndex(index);
			SerializedProperty pointProp = elemProp.FindPropertyRelative("point");
			SerializedProperty leftProp = elemProp.FindPropertyRelative("left");
			SerializedProperty rightProp = elemProp.FindPropertyRelative("right");

			if (index > 0)
			{
				SerializedProperty prevProp = listProp.GetArrayElementAtIndex(index - 1);
				// SerializedProperty pointPrevProp = prevProp.FindPropertyRelative("point");
				SerializedProperty leftPrevProp = prevProp.FindPropertyRelative("left");
				// SerializedProperty rightPrevProp = prevProp.FindPropertyRelative("right");

				pointProp.vector3Value += leftPrevProp.vector3Value * 3;
			}
			else
			{
				pointProp.vector3Value = new Vector3(1, 0, 0);
				leftProp.vector3Value = new Vector3(0.5f, 0, 0);
				rightProp.vector3Value = new Vector3(-0.5f, 0, 0);
			}
		}

		private void OnSelect(ReorderableList list)
		{
			ParentIndex = list.index;
		}

		private float ElementHeight(int index)
		{
			if (listProp.arraySize > 0)
			{
				SerializedProperty elemProp = listProp.GetArrayElementAtIndex(index);
				SerializedProperty pointProp = elemProp.FindPropertyRelative("point");
				float lineHeight = EditorGUI.GetPropertyHeight(pointProp) + EditorGUIUtility.standardVerticalSpacing;
				return lineHeight * 4;
			}
			return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
		}

		private void DrawListElement(Rect rect, int index, bool isActive, bool isFocused)
		{
			SerializedProperty elemProp = listProp.GetArrayElementAtIndex(index);
			SerializedProperty pointProp = elemProp.FindPropertyRelative("point");
			SerializedProperty leftProp = elemProp.FindPropertyRelative("left");
			SerializedProperty rightProp = elemProp.FindPropertyRelative("right");

			float height = rect.height / 4f;
			rect.height = height;
			EditorGUI.LabelField(rect, $"Node {index + 1}", EditorStyles.boldLabel);
			rect.y += height;
			EditorGUI.PropertyField(rect, pointProp);
			rect.y += height;
			EditorGUI.PropertyField(rect, leftProp);
			rect.y += height;
			EditorGUI.PropertyField(rect, rightProp);
		}

		private void DrawListHeader(Rect rect)
		{
			EditorGUI.LabelField(rect, "Spline nodes");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUILayout.Separator();
			// EditorGUILayout.BeginVertical(EditorStyles.helpBox);
			// EditorGUILayout.BeginVertical(EditorStyles.inspectorDefaultMargins);
			// EditorGUILayout.BeginVertical(EditorStyles.inspectorFullWidthMargins);
			EditorGUILayout.PropertyField(mirrorProp);
			EditorGUILayout.PropertyField(loopProp);
			// EditorGUILayout.EndVertical();
			// EditorGUILayout.EndVertical();
			EditorGUILayout.Separator();

			reorderableList.DoLayoutList();
			serializedObject.ApplyModifiedProperties();
		}

		private void OnSceneGUI()
		{
			serializedObject.Update();
			Handles.matrix = transform.localToWorldMatrix;
			bool applyChanges = false;

			if (listProp.arraySize > 0)
			{
				for (int i = 1; i < listProp.arraySize; i++)
				{
					SerializedProperty elemProp1 = listProp.GetArrayElementAtIndex(i - 1);
					SerializedProperty elemProp2 = listProp.GetArrayElementAtIndex(i);

					SerializedProperty pointProp1 = elemProp1.FindPropertyRelative("point");
					SerializedProperty leftProp1 = elemProp1.FindPropertyRelative("left");
					// SerializedProperty rightProp1 = elemProp1.FindPropertyRelative("right");

					SerializedProperty pointProp2 = elemProp2.FindPropertyRelative("point");
					// SerializedProperty leftProp2 = elemProp2.FindPropertyRelative("left");
					SerializedProperty rightProp2 = elemProp2.FindPropertyRelative("right");

					Vector3 pos1 = pointProp1.vector3Value;
					Vector3 left1 = leftProp1.vector3Value + pos1;
					// Vector3 right1 = rightProp1.vector3Value + pos1;

					Vector3 pos2 = pointProp2.vector3Value;
					// Vector3 left2 = leftProp2.vector3Value + pos2;
					Vector3 right2 = rightProp2.vector3Value + pos2;

					Handles.DrawBezier(pos1, pos2, left1, right2, Color.green, null, lineSize);

					// if (Event.current.type == EventType.Repaint)
					// {
					// 	Spline sp = target as Spline;
					// 	const int samples = 5;
					// 	for (int j = 0; j < samples; j++)
					// 	{
					// 		float t = j / (samples - 1f);
					// 		Vector3 center = sp.Evaluate(t);
					// 		Vector3 normal = sp.EvaluateNormal(t);
					// 		Quaternion rot = Quaternion.LookRotation(normal);
					// 		Handles.DrawWireDisc(center, normal, ballSize / 2f);
					// 		Handles.color = Color.white;
					// 		Handles.ArrowHandleCap(
					// 				0,
					// 				center,
					// 				rot,
					// 				ballSize * 2,
					// 				EventType.Repaint
					// 		);
					// 	}
					// }

					if (DrawHandles(i * 3, elemProp2))
					{
						applyChanges = true;
					};
				}

				DrawLoop();

				if (DrawHandles(0, listProp.GetArrayElementAtIndex(0)))
				{
					applyChanges = true;
				};
			}

			if (applyChanges)
			{
				serializedObject.ApplyModifiedProperties();
			}
		}

		private void DrawLoop()
		{
			if (loopProp.boolValue)
			{
				SerializedProperty firstProp = listProp.GetArrayElementAtIndex(0);
				SerializedProperty lastProp = listProp.GetArrayElementAtIndex(listProp.arraySize - 1);

				SerializedProperty pointProp1 = firstProp.FindPropertyRelative("point");
				SerializedProperty rightProp1 = firstProp.FindPropertyRelative("right");

				SerializedProperty pointProp2 = lastProp.FindPropertyRelative("point");
				SerializedProperty leftProp2 = lastProp.FindPropertyRelative("left");

				Vector3 pos1 = pointProp1.vector3Value;
				Vector3 right1 = rightProp1.vector3Value + pos1;

				Vector3 pos2 = pointProp2.vector3Value;
				Vector3 left2 = leftProp2.vector3Value + pos2;

				Handles.DrawBezier(pos1, pos2, right1, left2, Color.green, null, lineSize);
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

			Handles.color = Color.red;
			Handles.DrawLine(pos, left, lineSize);
			Handles.DrawLine(pos, right, lineSize);

			if (DrawPoint(index + 0, pointProp, null, Vector3.zero, pos))
			{
				return true;
			}

			if (DrawPoint(index + 1, leftProp, rightProp, pos, left))
			{
				return true;
			}

			if (DrawPoint(index + 2, rightProp, leftProp, pos, right))
			{
				return true;
			}

			return false;
		}

		private bool DrawPoint(int index, SerializedProperty prop, SerializedProperty opProp, Vector3 offset, Vector3 pos)
		{
			Handles.color = Color.white;
			float s = HandleUtility.GetHandleSize(pos) * ballSize;

			if (selectedIndex == index)
			{
				EditorGUI.BeginChangeCheck();
				Vector3 newPos = Handles.PositionHandle(pos, Quaternion.identity);

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
				reorderableList.Select(ParentIndex);
			}

			return false;
		}
	}
}