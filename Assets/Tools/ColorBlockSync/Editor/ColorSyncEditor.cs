using UnityEngine;
using UnityEditor;

namespace JD.Editor
{
	[CustomEditor(typeof(ColorSync), true)]
	[CanEditMultipleObjects]
	public class ColorSyncEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			if (GUILayout.Button("Apply"))
			{
				foreach (Object o in targets)
				{
					if (o is ColorSync tar)
					{
						tar.Apply();
					}
				}
			}
		}
	}
}