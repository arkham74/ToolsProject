using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;
#endif

namespace JD
{
	public class ButtonHold : Selectable
	{
		public float interval = 0.5f;
		public UnityEvent onPress = new UnityEvent();
		public UnityEvent onHold = new UnityEvent();
		public UnityEvent onRelease = new UnityEvent();

		public override void OnPointerDown(PointerEventData eventData)
		{
			base.OnPointerDown(eventData);
			onPress.Invoke();
			StartCoroutine(InvokeLoop());
		}

		public override void OnPointerUp(PointerEventData eventData)
		{
			base.OnPointerUp(eventData);
			Release();
		}

		public override void OnPointerExit(PointerEventData eventData)
		{
			base.OnPointerExit(eventData);
			Release();
		}

		protected override void OnDisable()
		{
			Release();
		}

		private void Release()
		{
			StopAllCoroutines();
			onRelease.Invoke();
		}

		private IEnumerator InvokeLoop()
		{
			while (true)
			{
				onHold.Invoke();
				yield return new WaitForSecondsRealtime(interval);
			}
		}
	}

#if UNITY_EDITOR
	[CanEditMultipleObjects]
	[CustomEditor(typeof(ButtonHold))]
	public class ButtonHoldEditor : SelectableEditor
	{
		private SerializedProperty onPressProperty;
		private SerializedProperty onHoldProperty;
		private SerializedProperty onReleaseProperty;
		private SerializedProperty intervalProperty;

		protected override void OnEnable()
		{
			base.OnEnable();
			intervalProperty = serializedObject.FindProperty("interval");
			onPressProperty = serializedObject.FindProperty("onPress");
			onHoldProperty = serializedObject.FindProperty("onHold");
			onReleaseProperty = serializedObject.FindProperty("onRelease");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.PropertyField(intervalProperty);
			serializedObject.ApplyModifiedProperties();
			base.OnInspectorGUI();
			serializedObject.Update();
			EditorGUILayout.PropertyField(onPressProperty);
			EditorGUILayout.PropertyField(onHoldProperty);
			EditorGUILayout.PropertyField(onReleaseProperty);
			serializedObject.ApplyModifiedProperties();
		}
	}
#endif
}