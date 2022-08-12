using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace JD.Editor
{
	[CustomPropertyDrawer(typeof(RenderingLayerAttribute))]
	public class RenderingLayerPropertyDrawer : PropertyDrawer
	{
		private const string LAYER = "Layer{0}";
		private static string[] layerNames;

		public override void OnGUI(Rect rect, SerializedProperty serializedProperty, GUIContent label)
		{
			layerNames ??= Enumerable.Range(1, 32).Select(i => string.Format(LAYER, i)).ToArray();
			RenderPipelineAsset srpAsset = GraphicsSettings.currentRenderPipeline;
			if (srpAsset != null && srpAsset.renderingLayerMaskNames != null) layerNames = srpAsset.renderingLayerMaskNames;
			serializedProperty.longValue =
				(uint)EditorGUI.MaskField(rect, label, (int)serializedProperty.longValue, layerNames);
			GUI.enabled = false;
			EditorGUILayout.LongField("Value", serializedProperty.longValue);
			GUI.enabled = true;
		}
	}
}