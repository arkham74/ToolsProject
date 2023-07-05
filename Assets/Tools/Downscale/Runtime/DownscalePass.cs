#if TOOLS_URP
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD
{
	public class DownscalePass : ScriptableRenderPass
	{
		private static readonly int bigBufferId = Shader.PropertyToID("_BigBuffer");
		private static readonly int smallBufferId = Shader.PropertyToID("_SmallBuffer");

		private string profilerTag;
		private int targetRes;
		private bool bilinear;

		public DownscalePass(string profilerTag, int targetRes)
		{
			this.profilerTag = profilerTag;
			this.targetRes = targetRes;
		}

		public DownscalePass(string profilerTag, int targetRes, bool bilinear) : this(profilerTag, targetRes)
		{
			this.bilinear = bilinear;
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			CameraData cameraData = renderingData.cameraData;

			RenderTextureDescriptor bigDescriptor = cameraData.cameraTargetDescriptor;
			bigDescriptor.autoGenerateMips = true;
			bigDescriptor.useMipMap = true;

			cmd.GetTemporaryRT(bigBufferId, bigDescriptor, FilterMode.Trilinear);

			RenderTextureDescriptor smallDescriptor = cameraData.cameraTargetDescriptor;
			smallDescriptor.width = (int)(targetRes * cameraData.camera.aspect);
			smallDescriptor.height = targetRes;

			cmd.GetTemporaryRT(smallBufferId, smallDescriptor, FilterMode.Point);
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			RenderTargetIdentifier colorTarget = renderingData.cameraData.renderer.cameraColorTarget;
			CommandBuffer cmd = CommandBufferPool.Get(profilerTag);

			if (bilinear)
			{
				Blit(cmd, colorTarget, bigBufferId);
				Blit(cmd, bigBufferId, smallBufferId);
				Blit(cmd, smallBufferId, colorTarget);
			}
			else
			{
				Blit(cmd, colorTarget, smallBufferId);
				Blit(cmd, smallBufferId, colorTarget);
			}

			context.ExecuteCommandBuffer(cmd);
			CommandBufferPool.Release(cmd);
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			cmd.ReleaseTemporaryRT(bigBufferId);
			cmd.ReleaseTemporaryRT(smallBufferId);
		}
	}
}
#endif