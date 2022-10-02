#if TOOLS_URP
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

namespace JD.CustomRenderObjects
{
	[ExcludeFromPreset]
	[Tooltip("Render Objects simplifies the injection of additional render passes by exposing a selection of commonly used settings.")]
	public class CustomRenderObjects : ScriptableRendererFeature
	{
		[Serializable]
		public class Settings
		{
			public string passTag = "CustomRenderObjectsFeature";
			public RenderPassEvent @event = RenderPassEvent.AfterRenderingOpaques;
			public FilterSettings filterSettings = new FilterSettings();
			public Material overrideMaterial;
			public int overrideMaterialPassIndex;
			public bool overrideDepthState;
			public CompareFunction depthCompareFunction = CompareFunction.LessEqual;
			public bool enableWrite = true;
			public StencilStateData stencilSettings = new StencilStateData();
			public CustomCameraSettings cameraSettings = new CustomCameraSettings();
		}

		[Serializable]
		public class FilterSettings
		{
			public RenderQueueType renderQueueType;
			public LayerMask layerMask;
			public uint renderLayerMask;
			public string[] passNames;

			public FilterSettings()
			{
				renderQueueType = RenderQueueType.Opaque;
				layerMask = -1;
				renderLayerMask = 1;
			}
		}

		[Serializable]
		public class CustomCameraSettings
		{
			public bool overrideCamera;
			public bool restoreCamera = true;
			public Vector4 offset;
			public float cameraFieldOfView = 60.0f;
		}

		public Settings settings = new Settings();

		private CustomRenderObjectsPass renderObjectsPass;

		public override void Create()
		{
			FilterSettings filter = settings.filterSettings;

			// Render Objects pass doesn't support events before rendering pre-passes.
			// The camera is not setup before this point and all rendering is monoscopeic.
			// Events before BeforeRenderingPre-passes should be used for input texture passes (shadow map, LUT, etc) that doesn't depend on the camera.
			// These events are filtering in the UI, but we still should prevent users from changing it from code or
			// by changing the serialized data.
			if (settings.@event < RenderPassEvent.BeforeRenderingPrePasses)
			{
				settings.@event = RenderPassEvent.BeforeRenderingPrePasses;
			}

			renderObjectsPass = new CustomRenderObjectsPass(settings.passTag, settings.@event, filter.passNames,
				filter.renderQueueType, filter.layerMask, filter.renderLayerMask, settings.cameraSettings);

			renderObjectsPass.OverrideMaterial = settings.overrideMaterial;
			renderObjectsPass.OverrideMaterialPassIndex = settings.overrideMaterialPassIndex;

			if (settings.overrideDepthState)
			{
				renderObjectsPass.SetDetphState(settings.enableWrite, settings.depthCompareFunction);
			}

			if (settings.stencilSettings.overrideStencilState)
			{
				renderObjectsPass.SetStencilState(settings.stencilSettings.stencilReference,
					settings.stencilSettings.stencilCompareFunction, settings.stencilSettings.passOperation,
					settings.stencilSettings.failOperation, settings.stencilSettings.zFailOperation);
			}
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			renderer.EnqueuePass(renderObjectsPass);
		}
	}
}
#endif