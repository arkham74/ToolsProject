using UnityEngine;
using UnityEditor;

namespace JD.Editor
{
	[CustomEditor(typeof(ColorBlockSync))]
	[CanEditMultipleObjects]
	public class ColorBlockSyncEditor : UnityEditor.Editor
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