using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.PathTrace
{
	public class PathTracePass : ScriptableRenderPass
	{
		public PathTraceSettings settings;

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer cmd = CommandBufferPool.Get("PathTrace");
			RenderTargetIdentifier colorTarget = renderingData.cameraData.renderer.cameraColorTarget;
			cmd.Blit(null, colorTarget, settings.material);
			context.ExecuteCommandBuffer(cmd);
			CommandBufferPool.Release(cmd);
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
		}
	}
}