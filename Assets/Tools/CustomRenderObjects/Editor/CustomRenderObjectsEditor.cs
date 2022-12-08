#if TOOLS_URP
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace JD.CustomRenderObjects.Editor
{
	[CustomEditor(typeof(CustomRenderObjects))]
	public class CustomRenderObjectsEditor : UnityEditor.Editor
	{
		SerializedProperty sceneViewProp;
		SerializedProperty clearDepthProp;
		SerializedProperty layerMaskProp;
		SerializedProperty renderLayerMaskProp;
		SerializedProperty passEventProp;
		SerializedProperty passInputProp;
		SerializedProperty renderQueueTypeProp;
		SerializedProperty sortingCriteriaProp;
		SerializedProperty renderStateMaskProp;
		SerializedProperty depthCompareFunctionProp;
		SerializedProperty depthWriteProp;
		SerializedProperty targetProp;
		SerializedProperty overrideMaterialProp;
		SerializedProperty overrideMaterialPassIndexProp;
		SerializedProperty cameraFieldOfViewProp;

		private void OnEnable()
		{
			SerializedProperty settingsProp = serializedObject.FindProperty("settings");
			sceneViewProp = settingsProp.FindPropertyRelative("sceneView");
			clearDepthProp = settingsProp.FindPropertyRelative("clearDepth");
			layerMaskProp = settingsProp.FindPropertyRelative("layerMask");
			renderLayerMaskProp = settingsProp.FindPropertyRelative("renderLayerMask");
			passEventProp = settingsProp.FindPropertyRelative("passEvent");
			passInputProp = settingsProp.FindPropertyRelative("passInput");
			renderQueueTypeProp = settingsProp.FindPropertyRelative("renderQueueType");
			sortingCriteriaProp = settingsProp.FindPropertyRelative("sortingCriteria");
			renderStateMaskProp = settingsProp.FindPropertyRelative("renderStateMask");
			depthCompareFunctionProp = settingsProp.FindPropertyRelative("depthCompareFunction");
			depthWriteProp = settingsProp.FindPropertyRelative("depthWrite");
			targetProp = settingsProp.FindPropertyRelative("target");
			overrideMaterialProp = settingsProp.FindPropertyRelative("overrideMaterial");
			overrideMaterialPassIndexProp = settingsProp.FindPropertyRelative("overrideMaterialPassIndex");
			cameraFieldOfViewProp = settingsProp.FindPropertyRelative("cameraFieldOfView");
		}
		public override void OnInspectorGUI()
		{
			EditorGUILayout.PropertyField(sceneViewProp);
			EditorGUILayout.PropertyField(clearDepthProp);
			EditorGUILayout.PropertyField(layerMaskProp);
			EditorGUILayout.PropertyField(renderLayerMaskProp);
			EditorGUILayout.PropertyField(passEventProp);
			EditorGUILayout.PropertyField(passInputProp);
			EditorGUILayout.PropertyField(renderQueueTypeProp);
			EditorGUILayout.PropertyField(sortingCriteriaProp);
			EditorGUILayout.PropertyField(renderStateMaskProp);

			if (((RenderStateMask)renderStateMaskProp.enumValueFlag).HasFlag(RenderStateMask.Depth))
			{
				EditorGUILayout.PropertyField(depthCompareFunctionProp);
				EditorGUILayout.PropertyField(depthWriteProp);
			}

			EditorGUILayout.PropertyField(targetProp);
			EditorGUILayout.PropertyField(overrideMaterialProp);
			EditorGUILayout.PropertyField(overrideMaterialPassIndexProp);
			EditorGUILayout.PropertyField(cameraFieldOfViewProp);
		}
	}
}
#endif