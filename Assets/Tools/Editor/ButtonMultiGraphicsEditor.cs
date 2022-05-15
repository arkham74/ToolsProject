using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(ButtonMultiGraphics))]
public class ButtonMultiGraphicsEditor : ButtonEditor
{
	private SerializedProperty targetsProp;

	protected override void OnEnable()
	{
		base.OnEnable();
		targetsProp = serializedObject.FindProperty("targets");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		EditorGUILayout.PropertyField(targetsProp);
		serializedObject.ApplyModifiedProperties();
		EditorGUILayout.Space();
		base.OnInspectorGUI();
	}
}
