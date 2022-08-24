using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace JD.Editor
{
	[CustomPropertyDrawer(typeof(RenderingLayerAttribute))]
	public class RenderingLayerPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect rect, SerializedProperty serializedProperty, GUIContent label)
		{
			if (serializedProperty.propertyType == SerializedPropertyType.Integer)
			{
				RenderPipelineAsset pipeline = GraphicsSettings.defaultRenderPipeline;
				if (pipeline && pipeline.renderingLayerMaskNames != null)
				{
					string[] options = pipeline.renderingLayerMaskNames;
					long mask = serializedProperty.longValue;
					uint value = (uint)EditorGUI.MaskField(rect, label, (int)mask, options);
					serializedProperty.longValue = value;
					return;
				}
			}

			EditorGUI.PropertyField(rect, serializedProperty, label);
		}
	}
}
