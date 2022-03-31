using System;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[CustomPropertyDrawer(typeof(RenderingLayerMaskAttribute))]
public class RenderingLayerMaskPropertyDrawer : PropertyDrawer
{
	private static string[] _defaultNames;
	private static string[] DefaultNames
	{
		get
		{
			if (_defaultNames == null)
				_defaultNames = Enumerable.Range(0, 32).Select(i => $"Layer{i + 1}").ToArray();
			return _defaultNames;
		}
	}

	public override void OnGUI(Rect rect, SerializedProperty serializedProperty, GUIContent label)
	{
		RenderPipelineAsset srpAsset = GraphicsSettings.currentRenderPipeline;
		if (srpAsset)
		{
			var layerNames = srpAsset.renderingLayerMaskNames;
			if (layerNames == null)
				layerNames = DefaultNames;

			serializedProperty.longValue = (uint)EditorGUILayout.MaskField(label, (int)serializedProperty.longValue, layerNames);
		}
		GUI.enabled = false;
		EditorGUILayout.LongField("Value", serializedProperty.longValue);
		GUI.enabled = true;
	}
}