using System;
using System.Collections.Generic;
using System.ComponentModel;
using Freya;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.Outline
{
	public class SurfacePass : ScriptableRenderPass
	{
		private static readonly int maskId = Shader.PropertyToID("_OutlineTargetMask");
		private static readonly int decalId = Shader.PropertyToID("_DBufferTexture0");

		private static readonly List<ShaderTagId> uniShaderTag = new List<ShaderTagId>()
		{
			new ShaderTagId("SRPDefaultUnlit"),
			new ShaderTagId("UniversalForward"),
			new ShaderTagId("UniversalForwardOnly"),
		};

		private RenderTargetIdentifier cameraDepth;
		private RenderTargetIdentifier cameraColor;

		public Material material;
		public LayerMask layerMask;
		public uint renderingLayerMask;

		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			// ConfigureInput(ScriptableRenderPassInput.Color | ScriptableRenderPassInput.Depth);
			// ConfigureTarget(maskId, cameraDepth);
			ConfigureInput(ScriptableRenderPassInput.Color);
			ConfigureTarget(maskId);
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			CameraData cameraData = renderingData.cameraData;
			cameraColor = cameraData.renderer.cameraColorTarget;
			cameraDepth = cameraData.renderer.cameraDepthTarget;

			RenderTextureDescriptor desc = cameraData.cameraTargetDescriptor;
			desc.msaaSamples = 1;
			desc.bindMS = false;
			desc.width = desc.width.Max(1);
			desc.height = desc.height.Max(1);
			cmd.GetTemporaryRT(maskId, desc);
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			cmd.ReleaseTemporaryRT(maskId);
		}

		private void DecalPass(CommandBuffer cmd, ScriptableRenderContext context, RenderingData renderingData)
		{
			cmd.Clear();
			Blit(cmd, decalId, maskId, material, 5);
			context.ExecuteCommandBuffer(cmd);
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (!renderingData.cameraData.isPreviewCamera && !renderingData.cameraData.isSceneViewCamera)
			{
				CommandBuffer cmd = CommandBufferPool.Get("Surface Pass");

				cmd.Clear();
				cmd.ClearRenderTarget(true, true, Color.clear);
				context.ExecuteCommandBuffer(cmd);

				FilteringSettings filteringSettings = new FilteringSettings(RenderQueueRange.opaque, layerMask, renderingLayerMask);
				DrawingSettings drawingSettings = CreateDrawingSettings(uniShaderTag, ref renderingData, renderingData.cameraData.defaultOpaqueSortFlags);
				drawingSettings.overrideMaterial = material;
				drawingSettings.overrideMaterialPassIndex = 4;
				drawingSettings.enableDynamicBatching = true;
				context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings);

				filteringSettings = new FilteringSettings(RenderQueueRange.transparent, layerMask, renderingLayerMask);
				drawingSettings = CreateDrawingSettings(uniShaderTag, ref renderingData, SortingCriteria.CommonTransparent);
				context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings);

				DecalPass(cmd, context, renderingData);

				CommandBufferPool.Release(cmd);
			}
		}
	}
}
