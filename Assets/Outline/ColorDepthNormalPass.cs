using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.Outline
{
	class ColorDepthNormalPass : ScriptableRenderPass
	{
		private static readonly int maskId = Shader.PropertyToID("_OutlineTargetMask");

		public Material material;

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			RenderTextureDescriptor desc = renderingData.cameraData.cameraTargetDescriptor;
			desc.msaaSamples = 1;
			desc.bindMS = false;
			desc.width = desc.width.Max(1);
			desc.height = desc.height.Max(1);
			cmd.GetTemporaryRT(maskId, desc);
		}

		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			ConfigureInput(ScriptableRenderPassInput.Color | ScriptableRenderPassInput.Depth | ScriptableRenderPassInput.Normal);
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (!renderingData.cameraData.isPreviewCamera && !renderingData.cameraData.isSceneViewCamera)
			{
				CommandBuffer cmd = CommandBufferPool.Get("Outline Pass");
				RenderTargetIdentifier cameraColor = renderingData.cameraData.renderer.cameraColorTarget;
				Blit(cmd, cameraColor, maskId, material, 6);
				Blit(cmd, maskId, cameraColor);
				context.ExecuteCommandBuffer(cmd);
				CommandBufferPool.Release(cmd);
			}
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			cmd.ReleaseTemporaryRT(maskId);
		}
	}
}