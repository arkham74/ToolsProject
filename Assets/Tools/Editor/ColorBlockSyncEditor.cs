using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColorBlockSync))]
[CanEditMultipleObjects]
public class ColorBlockSyncEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if (GUILayout.Button("Sync"))
		{
			foreach (ColorBlockSync tar in targets)
			{
				tar.Sync();
			}
		}
	}
}
