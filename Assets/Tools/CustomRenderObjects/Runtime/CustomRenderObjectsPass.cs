#if TOOLS_URP
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;

namespace JD.CustomRenderObjects
{
	public class CustomRenderObjectsPass : ScriptableRenderPass
	{
		private static readonly List<ShaderTagId> shaderTagIds = new List<ShaderTagId>()
		{
			new ShaderTagId("SRPDefaultUnlit"),
			new ShaderTagId("UniversalForward"),
			new ShaderTagId("UniversalForwardOnly"),
		};

		private readonly CustomRenderObjectsSettings settings;
		private RenderTargetIdentifier cameraDepth;
		private RenderTargetIdentifier cameraColor;

		public CustomRenderObjectsPass(CustomRenderObjectsSettings settings)
		{
			settings.renderPassEvent = (RenderPassEvent)Mathf.Max((int)settings.renderPassEvent, (int)RenderPassEvent.BeforeRenderingPrePasses);
			this.settings = settings;
			this.renderPassEvent = settings.renderPassEvent;
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			ScriptableRenderer renderer = renderingData.cameraData.renderer;
			cameraDepth = renderer.cameraDepthTarget;
			cameraColor = renderer.cameraColorTarget;
		}

		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			ConfigureInput(ScriptableRenderPassInput.Color | ScriptableRenderPassInput.Depth);

			if (settings.target.Trim().IsNullOrWhiteSpaceOrEmpty())
			{
				ConfigureTarget(cameraColor, cameraDepth);
			}
			else
			{
				ConfigureTarget(settings.target, cameraDepth);
			}
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (settings.sceneView || !renderingData.cameraData.isSceneViewCamera && !renderingData.cameraData.isPreviewCamera)
			{
				CommandBuffer cmd = CommandBufferPool.Get(settings.name);
				OverrideCamera(cmd, context, renderingData);
				DrawRenderers(cmd, context, renderingData);
				RestoreCamera(cmd, context, renderingData);
				CommandBufferPool.Release(cmd);
			}
		}

		private void OverrideCamera(CommandBuffer cmd, ScriptableRenderContext context, RenderingData renderingData)
		{
			if (settings.cameraFieldOfView > 0)
			{
				cmd.Clear();
				CameraData cameraData = renderingData.cameraData;
				Camera camera = cameraData.camera;
				Rect pixelRect = renderingData.cameraData.camera.pixelRect;
				float cameraAspect = pixelRect.width / pixelRect.height;
				Matrix4x4 projectionMatrix = Matrix4x4.Perspective(settings.cameraFieldOfView, cameraAspect, camera.nearClipPlane, camera.farClipPlane);
				projectionMatrix = GL.GetGPUProjectionMatrix(projectionMatrix, cameraData.IsCameraProjectionMatrixFlipped());
				RenderingUtils.SetViewAndProjectionMatrices(cmd, cameraData.GetViewMatrix(), projectionMatrix, false);
				context.ExecuteCommandBuffer(cmd);
			}
		}

		private void RestoreCamera(CommandBuffer cmd, ScriptableRenderContext context, RenderingData renderingData)
		{
			if (settings.cameraFieldOfView > 0)
			{
				cmd.Clear();
				CameraData cameraData = renderingData.cameraData;
				RenderingUtils.SetViewAndProjectionMatrices(cmd, cameraData.GetViewMatrix(), cameraData.GetGPUProjectionMatrix(), false);
				context.ExecuteCommandBuffer(cmd);
			}
		}

		private void DrawRenderers(CommandBuffer cmd, ScriptableRenderContext context, RenderingData renderingData)
		{
			RenderQueueRange renderQueueRange = GetQueueRange(settings.renderQueueType);
			RenderStateBlock renderStateBlock = new RenderStateBlock(RenderStateMask.Depth);
			renderStateBlock.depthState = new DepthState(settings.depthWrite, settings.depthCompareFunction);
			FilteringSettings filteringSettings = new FilteringSettings(renderQueueRange, settings.layerMask, settings.renderLayerMask);
			DrawingSettings drawingSettings = CreateDrawingSettings(shaderTagIds, ref renderingData, settings.sortingCriteria);
			if (settings.overrideMaterial)
			{
				drawingSettings.overrideMaterial = settings.overrideMaterial;
				drawingSettings.overrideMaterialPassIndex = settings.overrideMaterialPassIndex;
			}
			context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings, ref renderStateBlock);
		}

		private RenderQueueRange GetQueueRange(RenderQueueType renderQueueType) => renderQueueType switch
		{
			RenderQueueType.Opaque => RenderQueueRange.opaque,
			RenderQueueType.Transparent => RenderQueueRange.transparent,
			_ => RenderQueueRange.all,
		};
	}
}
#endif