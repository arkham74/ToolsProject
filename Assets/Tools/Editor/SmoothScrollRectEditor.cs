#if TOOLS_DOTWEEN
[CustomEditor(typeof(SmoothScrollRect), true)]
[CanEditMultipleObjects]
public class SmoothScrollRectEditor : ScrollRectEditor
{
	private SerializedProperty smoothScrollProp;
	private SerializedProperty smoothTimeProp;

	protected override void OnEnable()
	{
		base.OnEnable();
		smoothScrollProp = serializedObject.FindProperty("smoothScroll");
		smoothTimeProp = serializedObject.FindProperty("smoothTime");
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.PropertyField(smoothScrollProp);
		if (smoothScrollProp.boolValue)
			EditorGUILayout.PropertyField(smoothTimeProp);
		serializedObject.ApplyModifiedProperties();
		base.OnInspectorGUI();
	}
}
#endif