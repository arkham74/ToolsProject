#if TOOLS_DOTWEEN
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(SmoothScrollRect), true)]
[CanEditMultipleObjects]
public class SmoothScrollRectEditor : ScrollRectEditor
{
	private SerializedProperty smoothEaseProp;
	private SerializedProperty smoothScrollProp;
	private SerializedProperty smoothTimeProp;

	protected override void OnEnable()
	{
		base.OnEnable();
		smoothScrollProp = serializedObject.FindProperty("smoothScroll");
		smoothTimeProp = serializedObject.FindProperty("smoothTime");
		smoothEaseProp = serializedObject.FindProperty("smoothEase");
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.PropertyField(smoothScrollProp);
		if (smoothScrollProp.boolValue)
		{
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(smoothTimeProp);
			EditorGUILayout.PropertyField(smoothEaseProp);
			EditorGUI.indentLevel--;
		}

		EditorGUILayout.Separator();
		serializedObject.ApplyModifiedProperties();
		base.OnInspectorGUI();
	}
}
#endif