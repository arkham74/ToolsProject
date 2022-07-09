using UnityEngine;
using UnityEditor;

namespace CustomTools
{
	[CustomEditor(typeof(PixelCollider2D))]
	public class PixelColider2DEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			PixelCollider2D PC2D = (PixelCollider2D)target;
			if (GUILayout.Button("Regenerate Collider"))
			{
				PC2D.Regenerate();
			}
		}
	}
}