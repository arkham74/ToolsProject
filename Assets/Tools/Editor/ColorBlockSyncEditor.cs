using UnityEngine;
using UnityEditor;

namespace CustomTools
{
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
}