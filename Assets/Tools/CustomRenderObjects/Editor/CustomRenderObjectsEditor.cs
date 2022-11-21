#if TOOLS_URP
using UnityEditor;

namespace JD.CustomRenderObjects.Editor
{
	[CustomEditor(typeof(CustomRenderObjects))]
	public class CustomRenderObjectsEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			SerializedProperty prop = serializedObject.FindProperty("settings");
			foreach (SerializedProperty item in prop)
			{
				EditorGUILayout.PropertyField(item);
			}
		}
	}
}
#endif