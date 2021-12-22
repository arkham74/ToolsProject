#if TOOLS_URP
namespace UnityEngine.Rendering.Universal
{
	/// <summary>
	/// Draws full screen mesh using given material and pass and reading from source target.
	/// </summary>
	internal class DrawFullscreenPass : ScriptableRenderPass
	{
		public DrawFullscreenFeature.Settings settings;

		private RenderTargetIdentifier source;
		private RenderTargetIdentifier destination;
		private readonly int temporaryRtId = Shader.PropertyToID("_TempRT");

		private int sourceId;
		private int destinationId;
		private bool isSourceAndDestinationSameTarget;

		private readonly string mProfilerTag;

		public DrawFullscreenPass(string tag)
		{
			mProfilerTag = tag;
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			RenderTextureDescriptor blitTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
			blitTargetDescriptor.depthBufferBits = 0;

			isSourceAndDestinationSameTarget = settings.sourceType == settings.destinationType &&
			                                   (settings.sourceType == BufferType.CAMERA_COLOR ||
			                                    settings.sourceTextureId == settings.destinationTextureId);

			ScriptableRenderer renderer = renderingData.cameraData.renderer;

			if (settings.sourceType == BufferType.CAMERA_COLOR)
			{
				sourceId = -1;
				source = renderer.cameraColorTarget;
			}
			else
			{
				sourceId = Shader.PropertyToID(settings.sourceTextureId);
				cmd.GetTemporaryRT(sourceId, blitTargetDescriptor, FilterMode.Point);
				source = new RenderTargetIdentifier(sourceId);
			}

			if (isSourceAndDestinationSameTarget)
			{
				destinationId = temporaryRtId;
				cmd.GetTemporaryRT(destinationId, blitTargetDescriptor, FilterMode.Point);
				destination = new RenderTargetIdentifier(destinationId);
			}
			else if (settings.destinationType == BufferType.CAMERA_COLOR)
			{
				destinationId = -1;
				destination = renderer.cameraColorTarget;
			}
			else
			{
				destinationId = Shader.PropertyToID(settings.destinationTextureId);
				cmd.GetTemporaryRT(destinationId, blitTargetDescriptor, FilterMode.Point);
				destination = new RenderTargetIdentifier(destinationId);
			}
		}

		/// <inheritdoc/>
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer cmd = CommandBufferPool.Get(mProfilerTag);

			// Can't read and write to same color target, create a temp render target to blit.
			if (isSourceAndDestinationSameTarget)
			{
				Blit(cmd, source, destination, settings.blitMaterial, settings.blitMaterialPassIndex);
				Blit(cmd, destination, source);
			}
			else
			{
				Blit(cmd, source, destination, settings.blitMaterial, settings.blitMaterialPassIndex);
			}

			context.ExecuteCommandBuffer(cmd);
			CommandBufferPool.Release(cmd);
		}

		/// <inheritdoc/>
		public override void FrameCleanup(CommandBuffer cmd)
		{
			if (destinationId != -1) cmd.ReleaseTemporaryRT(destinationId);

			if (source == destination && sourceId != -1) cmd.ReleaseTemporaryRT(sourceId);
		}
	}
}
#endif