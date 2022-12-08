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
		SerializedProperty clearFlagsProp;
		SerializedProperty layerMaskProp;
		SerializedProperty renderLayerMaskProp;
		SerializedProperty passEventProp;
		SerializedProperty passInputProp;
		SerializedProperty renderQueueTypeProp;
		SerializedProperty renderStateMaskProp;
		SerializedProperty depthCompareFunctionProp;
		SerializedProperty depthWriteProp;
		SerializedProperty targetProp;
		SerializedProperty overrideMaterialProp;
		SerializedProperty overrideMaterialPassIndexProp;
		SerializedProperty cameraFieldOfViewProp;
		SerializedProperty graphicsFormatProp;

		private void OnEnable()
		{
			SerializedProperty settingsProp = serializedObject.FindProperty("settings");
			sceneViewProp = settingsProp.FindPropertyRelative("sceneView");
			clearFlagsProp = settingsProp.FindPropertyRelative("clearFlags");
			layerMaskProp = settingsProp.FindPropertyRelative("layerMask");
			renderLayerMaskProp = settingsProp.FindPropertyRelative("renderLayerMask");
			passEventProp = settingsProp.FindPropertyRelative("passEvent");
			passInputProp = settingsProp.FindPropertyRelative("passInput");
			renderQueueTypeProp = settingsProp.FindPropertyRelative("renderQueueType");
			renderStateMaskProp = settingsProp.FindPropertyRelative("renderStateMask");
			depthCompareFunctionProp = settingsProp.FindPropertyRelative("depthCompareFunction");
			depthWriteProp = settingsProp.FindPropertyRelative("depthWrite");
			targetProp = settingsProp.FindPropertyRelative("target");
			overrideMaterialProp = settingsProp.FindPropertyRelative("overrideMaterial");
			overrideMaterialPassIndexProp = settingsProp.FindPropertyRelative("overrideMaterialPassIndex");
			cameraFieldOfViewProp = settingsProp.FindPropertyRelative("cameraFieldOfView");
			graphicsFormatProp = settingsProp.FindPropertyRelative("graphicsFormat");
		}
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.PropertyField(sceneViewProp);
			EditorGUILayout.PropertyField(clearFlagsProp);
			EditorGUILayout.PropertyField(layerMaskProp);
			EditorGUILayout.PropertyField(renderLayerMaskProp);
			EditorGUILayout.PropertyField(passEventProp);
			EditorGUILayout.PropertyField(passInputProp);
			EditorGUILayout.PropertyField(renderQueueTypeProp);
			EditorGUILayout.PropertyField(renderStateMaskProp);

			if (((RenderStateMask)renderStateMaskProp.enumValueFlag).HasFlag(RenderStateMask.Depth))
			{
				EditorGUILayout.PropertyField(depthCompareFunctionProp);
				EditorGUILayout.PropertyField(depthWriteProp);
			}

			EditorGUILayout.PropertyField(targetProp);
			if (!targetProp.stringValue.IsNullOrWhiteSpaceOrEmpty())
				EditorGUILayout.PropertyField(graphicsFormatProp);

			EditorGUILayout.PropertyField(overrideMaterialProp);

			Material mat = (Material)overrideMaterialProp.objectReferenceValue;
			if (mat)
			{
				string[] passes = new string[mat.passCount];
				for (int i = 0; i < passes.Length; i++) passes[i] = mat.GetPassName(i);
				int newID = overrideMaterialPassIndexProp.intValue;
				newID = EditorGUILayout.Popup(overrideMaterialPassIndexProp.displayName, newID, passes);
				overrideMaterialPassIndexProp.intValue = newID;
			}

			EditorGUILayout.PropertyField(cameraFieldOfViewProp);
			serializedObject.ApplyModifiedProperties();
		}
	}
}
#endif