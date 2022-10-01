using UnityEngine;
using UnityEditor;

namespace JD.Editor
{
	[CustomEditor(typeof(PixelCollider2D))]
	public class PixelColider2DEditor : UnityEditor.Editor
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