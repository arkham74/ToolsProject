// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// Credit Slipp Douglas Thompson
// Sourced from - https://gist.github.com/capnslipp/349c18283f2fea316369

using UnityEditor;
using UnityEditor.UI;

namespace UIExtensions.Editor
{
	[CanEditMultipleObjects, CustomEditor(typeof(NoneGraphic), false)]
	public class NoneGraphicEditor : GraphicEditor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			// EditorGUILayout.PropertyField(m_Script);
			RaycastControlsGUI();
			serializedObject.ApplyModifiedProperties();
		}
	}
}