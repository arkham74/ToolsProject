using System;
using System.ComponentModel;
using Freya;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.Outline
{
	public class CameraPass : ScriptableRenderPass
	{
		private static readonly int maskId = Shader.PropertyToID("_OutlineTargetMask");
		private static readonly int colorId = Shader.PropertyToID("_OutlineColor");
		private static readonly int backId = Shader.PropertyToID("_BackgroundColor");
		private readonly Material material;

		public bool sceneView;
		public Color outlineColor;
		public Color backgroundColor;

		public CameraPass()
		{
			this.material = CoreUtils.CreateEngineMaterial("Hidden/OutlineJFASobel");
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			RenderTextureDescriptor desc = renderingData.cameraData.cameraTargetDescriptor;
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

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CameraData cameraData = renderingData.cameraData;
			if (!cameraData.isPreviewCamera && (sceneView || !cameraData.isSceneViewCamera))
			{
				RenderTargetIdentifier cameraColor = cameraData.renderer.cameraColorTarget;
				CommandBuffer cmd = CommandBufferPool.Get("Outline Pass");
				cmd.Clear();
				cmd.SetGlobalColor(colorId, outlineColor);
				cmd.SetGlobalColor(backId, backgroundColor);
				Blit(cmd, cameraColor, maskId, material, 3);
				Blit(cmd, maskId, cameraColor);
				context.ExecuteCommandBuffer(cmd);
				CommandBufferPool.Release(cmd);
			}
		}
	}
}