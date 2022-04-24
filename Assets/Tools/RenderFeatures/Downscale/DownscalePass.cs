#if TOOLS_URP
namespace UnityEngine.Rendering.Universal
{
	public partial class DownscaleFeature
	{
		private class DownscalePass : ScriptableRenderPass
		{
			public string profilerTag;
			public int targetRes = 180;
			public RenderTargetIdentifier source;
			private RenderTargetIdentifier downTarget;
			private RenderTargetIdentifier upTarget;
			private int downTargetId;
			private int upTargetId;

			// This method is called before executing the render pass.
			// It can be used to configure render targets and their clear state. Also to create temporary render target textures.
			// When empty this render pass will render to the active camera render target.
			// You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
			// The render pipeline will ensure target setup and clearing happens in a performant manner.
			public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
			{
				RenderTextureDescriptor upDescriptor = renderingData.cameraData.cameraTargetDescriptor;
				RenderTextureDescriptor downDescriptor = renderingData.cameraData.cameraTargetDescriptor;
				float aspect = (float)downDescriptor.width / downDescriptor.height;
				downDescriptor.width = Mathf.FloorToInt(aspect * targetRes);
				downDescriptor.height = targetRes;

				downTargetId = Shader.PropertyToID("downScaleTarget");
				upTargetId = Shader.PropertyToID("upScaleTarget");

				cmd.GetTemporaryRT(upTargetId, upDescriptor, FilterMode.Point);
				cmd.GetTemporaryRT(downTargetId, downDescriptor, FilterMode.Point);

				downTarget = new RenderTargetIdentifier(downTargetId);
				upTarget = new RenderTargetIdentifier(upTargetId);

				ConfigureTarget(downTarget);
				ConfigureTarget(upTarget);
			}

			// Here you can implement the rendering logic.
			// Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
			// https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
			// You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
			public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
			{
				CommandBuffer cmd = CommandBufferPool.Get(profilerTag);

				cmd.Blit(source, upTarget);
				cmd.Blit(upTarget, downTarget);
				cmd.Blit(downTarget, source);

				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();
				CommandBufferPool.Release(cmd);
			}

			// Cleanup any allocated resources that were created during the execution of this render pass.
			public override void OnCameraCleanup(CommandBuffer cmd)
			{
				cmd.ReleaseTemporaryRT(downTargetId);
				cmd.ReleaseTemporaryRT(upTargetId);
			}
		}
	}
}
#endif