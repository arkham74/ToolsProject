using UnityEditor;
using UnityEngine;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PossibleMultipleEnumeration
public static class ToolsEditor
{
	public static T ObjectField<T>(string label, Object obj, bool allowSceneObjects = false,
		params GUILayoutOption[] options) where T : Object
	{
		return (T) EditorGUILayout.ObjectField(label, obj, typeof(T), allowSceneObjects, options);
	}

	public static void DrawWireSphere(Vector3 position, Quaternion rotation, float radius)
	{
		Handles.DrawWireDisc(position, rotation * Vector3.up, radius);
		Handles.DrawWireDisc(position, rotation * Vector3.right, radius);
		Handles.DrawWireDisc(position, rotation * Vector3.forward, radius);
	}

	public static void DrawWireCapsule(Vector3 position, Quaternion rotation, float radius, float height)
	{
		Matrix4x4 angleMatrix = Matrix4x4.TRS(position, rotation, Handles.matrix.lossyScale);
		using (new Handles.DrawingScope(angleMatrix))
		{
			float pointOffset = (height - radius * 2) / 2;

			//draw sideways
			Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.left, Vector3.back, -180, radius);
			Handles.DrawLine(new Vector3(0, pointOffset, -radius), new Vector3(0, -pointOffset, -radius));
			Handles.DrawLine(new Vector3(0, pointOffset, radius), new Vector3(0, -pointOffset, radius));
			Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.left, Vector3.back, 180, radius);
			//draw front
			Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.back, Vector3.left, 180, radius);
			Handles.DrawLine(new Vector3(-radius, pointOffset, 0), new Vector3(-radius, -pointOffset, 0));
			Handles.DrawLine(new Vector3(radius, pointOffset, 0), new Vector3(radius, -pointOffset, 0));
			Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.back, Vector3.left, -180, radius);
			//draw center
			Handles.DrawWireDisc(Vector3.up * pointOffset, Vector3.up, radius);
			Handles.DrawWireDisc(Vector3.down * pointOffset, Vector3.up, radius);
		}
	}
}