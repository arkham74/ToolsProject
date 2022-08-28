#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine.Rendering;

namespace JD.CustomRenderObjects.Editor
{
	[CustomPropertyDrawer(typeof(CustomRenderObjects.Settings), true)]
	internal class CustomRenderObjectsPassFeatureEditor : PropertyDrawer
	{
		private static class Styles
		{
			public static readonly float DefaultLineSpace = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
			public static readonly GUIContent Callback = new GUIContent("Event", "Choose at which point this render pass is executed in the frame.");

			//Headers
			public static readonly GUIContent FiltersHeader = new GUIContent("Filters", "Settings that control which objects should be rendered.");
			public static readonly GUIContent RenderHeader = new GUIContent("Overrides", "Different parts of the rendering that you can choose to override.");

			//Filters
			public static readonly GUIContent RenderQueueFilter = new GUIContent("Queue", "Only render objects in the selected render queue range.");
			public static readonly GUIContent LayerMask = new GUIContent("Layer Mask", "Only render objects in a layer that match the given layer mask.");
			public static readonly GUIContent RenderMask = new GUIContent("Render Layer Mask", "Only render objects in a render layer that match the given render layer mask.");
			public static readonly GUIContent ShaderPassFilter = new GUIContent("LightMode Tags", "Controls which shader passes to render by filtering by LightMode tag.");

			//Render Options
			public static readonly GUIContent OverrideMaterial = new GUIContent("Material", "Choose an override material, every renderer will be rendered with this material.");
			public static readonly GUIContent OverrideMaterialPass = new GUIContent("Pass Index", "The pass index for the override material to use.");

			//Depth Settings
			public static readonly GUIContent OverrideDepth = new GUIContent("Depth", "Select this option to specify how this Renderer Feature affects or uses the values in the Depth buffer.");
			public static readonly GUIContent WriteDepth = new GUIContent("Write Depth", "Choose to write depth to the screen.");
			public static readonly GUIContent DepthState = new GUIContent("Depth Test", "Choose a new depth test function.");

			//Camera Settings
			public static readonly GUIContent OverrideCamera = new GUIContent("Camera", "Override camera matrices. Toggling this setting will make camera use perspective projection.");
			public static readonly GUIContent CameraFOV = new GUIContent("Field Of View", "The camera's view angle measured in degrees along vertical axis.");
			public static readonly GUIContent PositionOffset = new GUIContent("Position Offset", "Position offset to apply to the original camera's position.");
			public static readonly GUIContent RestoreCamera = new GUIContent("Restore", "Restore to the original camera matrices after the execution of the render passes added by this feature.");
		}

		//Headers and layout
		private HeaderBool mFiltersFoldout;
		private readonly int mFilterLines = 3;
		private HeaderBool mRenderFoldout;
		private readonly int mMaterialLines = 2;
		private readonly int mDepthLines = 3;
		private readonly int mCameraLines = 4;

		// Serialized Properties
		private SerializedProperty mCallback;

		private SerializedProperty mPassTag;

		//Filter props
		private SerializedProperty mFilterSettings;
		private SerializedProperty mRenderQueue;
		private SerializedProperty mLayerMask;
		private SerializedProperty mRenderLayerMask;

		private SerializedProperty mShaderPasses;

		//Render props
		private SerializedProperty mOverrideMaterial;

		private SerializedProperty mOverrideMaterialPass;

		//Depth props
		private SerializedProperty mOverrideDepth;
		private SerializedProperty mWriteDepth;

		private SerializedProperty mDepthState;

		//Stencil props
		private SerializedProperty mStencilSettings;

		//Caemra props
		private SerializedProperty mCameraSettings;
		private SerializedProperty mOverrideCamera;
		private SerializedProperty mFOV;
		private SerializedProperty mCameraOffset;
		private SerializedProperty mRestoreCamera;

		private readonly List<SerializedObject> mProperties = new List<SerializedObject>();

		// Return all events higher or equal than before rendering prepasses
		// filter obsolete events
		private static bool FilterRenderPassEvent(int evt) => evt >= (int)RenderPassEvent.BeforeRenderingPrePasses && typeof(RenderPassEvent).GetField(Enum.GetName(typeof(RenderPassEvent), evt))?.GetCustomAttribute(typeof(ObsoleteAttribute)) == null;

		// Return all render pass event names that match filterRenderPassEvent
		private readonly GUIContent[] mEventOptionNames = Enum.GetValues(typeof(RenderPassEvent)).Cast<int>().Where(FilterRenderPassEvent).Select(x => new GUIContent(Enum.GetName(typeof(RenderPassEvent), x))).ToArray();

		// Return all render pass event options that match filterRenderPassEvent
		private readonly int[] mEventOptionValues = Enum.GetValues(typeof(RenderPassEvent)).Cast<int>().Where(FilterRenderPassEvent).ToArray();

		private void Init(SerializedProperty property)
		{
			//Header bools
			string key = $"{ToString().Split('.').Last()}.{property.serializedObject.targetObject.name}";
			mFiltersFoldout = new HeaderBool($"{key}.FiltersFoldout", true);
			mRenderFoldout = new HeaderBool($"{key}.RenderFoldout");


			mCallback = property.FindPropertyRelative("event");
			mPassTag = property.FindPropertyRelative("passTag");

			//Filter props
			mFilterSettings = property.FindPropertyRelative("filterSettings");
			mRenderQueue = mFilterSettings.FindPropertyRelative("renderQueueType");
			mLayerMask = mFilterSettings.FindPropertyRelative("layerMask");
			mRenderLayerMask = mFilterSettings.FindPropertyRelative("renderLayerMask");
			mShaderPasses = mFilterSettings.FindPropertyRelative("passNames");

			//Render options
			mOverrideMaterial = property.FindPropertyRelative("overrideMaterial");
			mOverrideMaterialPass = property.FindPropertyRelative("overrideMaterialPassIndex");

			//Depth props
			mOverrideDepth = property.FindPropertyRelative("overrideDepthState");
			mWriteDepth = property.FindPropertyRelative("enableWrite");
			mDepthState = property.FindPropertyRelative("depthCompareFunction");

			//Stencil
			mStencilSettings = property.FindPropertyRelative("stencilSettings");

			//Camera
			mCameraSettings = property.FindPropertyRelative("cameraSettings");
			mOverrideCamera = mCameraSettings.FindPropertyRelative("overrideCamera");
			mFOV = mCameraSettings.FindPropertyRelative("cameraFieldOfView");
			mCameraOffset = mCameraSettings.FindPropertyRelative("offset");
			mRestoreCamera = mCameraSettings.FindPropertyRelative("restoreCamera");

			mProperties.Add(property.serializedObject);
		}

		public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
		{
			rect.height = EditorGUIUtility.singleLineHeight;
			EditorGUI.BeginChangeCheck();
			EditorGUI.BeginProperty(rect, label, property);

			if (!mProperties.Contains(property.serializedObject))
			{
				Init(property);
			}

			string passName = property.serializedObject.FindProperty("m_Name").stringValue;
			if (passName != mPassTag.stringValue)
			{
				mPassTag.stringValue = passName;
				property.serializedObject.ApplyModifiedProperties();
			}

			//Forward Callbacks
			EditorGUI.BeginChangeCheck();
			int selectedValue = EditorGUI.IntPopup(rect, Styles.Callback, mCallback.intValue, mEventOptionNames, mEventOptionValues);
			if (EditorGUI.EndChangeCheck())
				mCallback.intValue = selectedValue;
			rect.y += Styles.DefaultLineSpace;

			DoFilters(ref rect);

			mRenderFoldout.Value = EditorGUI.Foldout(rect, mRenderFoldout.Value, Styles.RenderHeader, true);
			SaveHeaderBool(mRenderFoldout);
			rect.y += Styles.DefaultLineSpace;
			if (mRenderFoldout.Value)
			{
				EditorGUI.indentLevel++;
				//Override material
				DoMaterialOverride(ref rect);
				rect.y += Styles.DefaultLineSpace;
				//Override depth
				DoDepthOverride(ref rect);
				rect.y += Styles.DefaultLineSpace;
				//Override stencil
				EditorGUI.PropertyField(rect, mStencilSettings);
				rect.y += EditorGUI.GetPropertyHeight(mStencilSettings);
				//Override camera
				DoCameraOverride(ref rect);
				rect.y += Styles.DefaultLineSpace;

				EditorGUI.indentLevel--;
			}

			EditorGUI.EndProperty();
			if (EditorGUI.EndChangeCheck())
				property.serializedObject.ApplyModifiedProperties();
		}

		private void DoFilters(ref Rect rect)
		{
			mFiltersFoldout.Value = EditorGUI.Foldout(rect, mFiltersFoldout.Value, Styles.FiltersHeader, true);
			SaveHeaderBool(mFiltersFoldout);
			rect.y += Styles.DefaultLineSpace;
			if (mFiltersFoldout.Value)
			{
				EditorGUI.indentLevel++;
				//Render queue filter
				EditorGUI.PropertyField(rect, mRenderQueue, Styles.RenderQueueFilter);
				rect.y += Styles.DefaultLineSpace;
				//Layer mask
				EditorGUI.PropertyField(rect, mLayerMask, Styles.LayerMask);
				rect.y += Styles.DefaultLineSpace;
				//Render layer mask
				mRenderLayerMask.longValue = (uint)EditorGUI.MaskField(rect, Styles.RenderMask, (int)mRenderLayerMask.longValue, GraphicsSettings.defaultRenderPipeline.renderingLayerMaskNames);
				rect.y += Styles.DefaultLineSpace;
				//Shader pass list
				EditorGUI.PropertyField(rect, mShaderPasses, Styles.ShaderPassFilter, true);
				rect.y += EditorGUI.GetPropertyHeight(mShaderPasses);
				EditorGUI.indentLevel--;
			}
		}

		private void DoMaterialOverride(ref Rect rect)
		{
			//Override material
			EditorGUI.PropertyField(rect, mOverrideMaterial, Styles.OverrideMaterial);
			if (mOverrideMaterial.objectReferenceValue)
			{
				rect.y += Styles.DefaultLineSpace;
				EditorGUI.indentLevel++;
				EditorGUI.BeginChangeCheck();
				EditorGUI.PropertyField(rect, mOverrideMaterialPass, Styles.OverrideMaterialPass);
				if (EditorGUI.EndChangeCheck())
					mOverrideMaterialPass.intValue = Mathf.Max(0, mOverrideMaterialPass.intValue);
				EditorGUI.indentLevel--;
			}
		}

		private void DoDepthOverride(ref Rect rect)
		{
			EditorGUI.PropertyField(rect, mOverrideDepth, Styles.OverrideDepth);
			if (mOverrideDepth.boolValue)
			{
				rect.y += Styles.DefaultLineSpace;
				EditorGUI.indentLevel++;
				//Write depth
				EditorGUI.PropertyField(rect, mWriteDepth, Styles.WriteDepth);
				rect.y += Styles.DefaultLineSpace;
				//Depth testing options
				EditorGUI.PropertyField(rect, mDepthState, Styles.DepthState);
				EditorGUI.indentLevel--;
			}
		}

		private void DoCameraOverride(ref Rect rect)
		{
			EditorGUI.PropertyField(rect, mOverrideCamera, Styles.OverrideCamera);
			if (mOverrideCamera.boolValue)
			{
				rect.y += Styles.DefaultLineSpace;
				EditorGUI.indentLevel++;
				//FOV
				EditorGUI.Slider(rect, mFOV, 4f, 179f, Styles.CameraFOV);
				rect.y += Styles.DefaultLineSpace;
				//Offset vector
				Vector4 offset = mCameraOffset.vector4Value;
				EditorGUI.BeginChangeCheck();
				Vector3 newOffset = EditorGUI.Vector3Field(rect, Styles.PositionOffset, new Vector3(offset.x, offset.y, offset.z));
				if (EditorGUI.EndChangeCheck())
					mCameraOffset.vector4Value = new Vector4(newOffset.x, newOffset.y, newOffset.z, 1f);
				rect.y += Styles.DefaultLineSpace;
				//Restore prev camera projections
				EditorGUI.PropertyField(rect, mRestoreCamera, Styles.RestoreCamera);
				rect.y += Styles.DefaultLineSpace;

				EditorGUI.indentLevel--;
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			float height = Styles.DefaultLineSpace;

			Init(property);
			height += Styles.DefaultLineSpace * (mFiltersFoldout.Value ? mFilterLines : 1);
			height += mFiltersFoldout.Value ? EditorGUI.GetPropertyHeight(mShaderPasses) : 0;

			height += Styles.DefaultLineSpace; // add line for overrides dropdown
			if (mRenderFoldout.Value)
			{
				height += Styles.DefaultLineSpace * (mOverrideMaterial.objectReferenceValue != null ? mMaterialLines : 1);
				height += Styles.DefaultLineSpace * (mOverrideDepth.boolValue ? mDepthLines : 1);
				height += EditorGUI.GetPropertyHeight(mStencilSettings);
				height += Styles.DefaultLineSpace * (mOverrideCamera.boolValue ? mCameraLines : 1);
			}

			return height;
		}

		private void SaveHeaderBool(HeaderBool boolObj)
		{
			EditorPrefs.SetBool(boolObj.Key, boolObj.Value);
		}

		private class HeaderBool
		{
			public readonly string Key;
			public bool Value;

			public HeaderBool(string key, bool @default = false)
			{
				Key = key;
				Value = EditorPrefs.HasKey(Key) ? EditorPrefs.GetBool(Key) : @default;
				EditorPrefs.SetBool(Key, Value);
			}
		}
	}
}
#endif
