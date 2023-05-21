using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.Outline
{
	public class OutlinePass : ScriptableRenderPass
	{
		private const int PASS_JFAFILL = 0;
		private const int PASS_JFAINIT = 1;
		private const int PASS_JFAVORONOI = 2;
		private const int PASS_JFAOUTLINE = 3;
		private const int PASS_SOBELOUTLINE = 4;

		private static readonly int maskId = Shader.PropertyToID("_OutlineTargetMask");
		private static readonly int pingId = Shader.PropertyToID("_OutlineTargetPing");
		private static readonly int pongId = Shader.PropertyToID("_OutlineTargetPong");
		private static readonly int stepId = Shader.PropertyToID("_StepWidth");

		private static readonly ShaderTagId uniShaderTag = new ShaderTagId("UniversalForward");

		public readonly OutlineSettings settings;

		private RenderTargetIdentifier cameraDepth;
		private RenderTargetIdentifier cameraColor;
		private Material outlineMaterial;
		private readonly ProfilingSampler m_ProfilingSampler;

		public OutlinePass(string passName, OutlineSettings settings, Material material)
		{
			m_ProfilingSampler = new ProfilingSampler(passName);
			base.profilingSampler = m_ProfilingSampler;
			this.outlineMaterial = material;
			this.settings = settings;
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			CameraData cameraData = renderingData.cameraData;
			cameraColor = cameraData.renderer.cameraColorTarget;
			cameraDepth = cameraData.renderer.cameraDepthTarget;

			RenderTextureDescriptor desc = cameraData.cameraTargetDescriptor;
			desc.depthBufferBits = 0;

			desc.graphicsFormat = GraphicsFormat.R8_UNorm;
			cmd.GetTemporaryRT(maskId, desc);

			desc.graphicsFormat = GraphicsFormat.R16G16_SNorm;
			cmd.GetTemporaryRT(pingId, desc);
			cmd.GetTemporaryRT(pongId, desc);
		}

		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			ConfigureInput(ScriptableRenderPassInput.Color | ScriptableRenderPassInput.Depth);
			// ConfigureClear(ClearFlag.ColorStencil, Color.clear);
			ConfigureTarget(maskId, cameraDepth);
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			cmd.ReleaseTemporaryRT(maskId);
			cmd.ReleaseTemporaryRT(pingId);
			cmd.ReleaseTemporaryRT(pongId);
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer cmd = CommandBufferPool.Get();
			ClearMask(cmd, context);
			DrawMask(cmd, context, renderingData);
			if (settings.width > 1)
			{
				JumpFloodPass(cmd, context, renderingData);
			}
			else
			{
				SobelPass(cmd, context, renderingData);
			}
			CommandBufferPool.Release(cmd);
		}

		private void ClearMask(CommandBuffer cmd, ScriptableRenderContext context)
		{
			cmd.Clear();
			using (new ProfilingScope(cmd, m_ProfilingSampler))
			{
				cmd.ClearRenderTarget(RTClearFlags.ColorStencil, Color.clear, 1, 0);
			}
			context.ExecuteCommandBuffer(cmd);
		}

		private void DrawMask(CommandBuffer cmd, ScriptableRenderContext context, RenderingData renderingData)
		{
			cmd.Clear();
			using (new ProfilingScope(cmd, m_ProfilingSampler))
			{
				FilteringSettings filteringSettings = new FilteringSettings(RenderQueueRange.all, -1, settings.layer);
				RenderStateBlock renderStateBlock = new RenderStateBlock(RenderStateMask.Depth | RenderStateMask.Raster);
				DepthState depthState = new DepthState(false, (CompareFunction)settings.compare);
				DrawingSettings drawingSettings = CreateDrawingSettings(uniShaderTag, ref renderingData, SortingCriteria.CommonOpaque);
				CullingResults cullResults = renderingData.cullResults;

				renderStateBlock.depthState = depthState;
				drawingSettings.overrideMaterial = outlineMaterial;
				drawingSettings.overrideMaterialPassIndex = PASS_JFAFILL;
				drawingSettings.enableDynamicBatching = true;

				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();
				context.DrawRenderers(cullResults, ref drawingSettings, ref filteringSettings, ref renderStateBlock);
			}
			context.ExecuteCommandBuffer(cmd);
		}

		private void JumpFloodPass(CommandBuffer cmd, ScriptableRenderContext context, RenderingData renderingData)
		{
			cmd.Clear();
			using (new ProfilingScope(cmd, m_ProfilingSampler))
			{
				Blit(cmd, maskId, pongId, outlineMaterial, PASS_JFAINIT);

				int numMips = Mathf.CeilToInt(Mathf.Log(settings.width + 1, 2));
				int jfaIter = numMips - 1;

				//JFA PASS
				for (int i = 0; i <= jfaIter; i++)
				{
					// calculate appropriate jump width for each iteration
					cmd.SetGlobalInteger(stepId, (int)Mathf.Pow(2, jfaIter - i));

					// ping pong between buffers
					if (i % 2 == 0)
					{
						Blit(cmd, pongId, pingId, outlineMaterial, PASS_JFAVORONOI);
					}
					else
					{
						Blit(cmd, pingId, pongId, outlineMaterial, PASS_JFAVORONOI);
					}
				}

				cmd.SetGlobalFloat("_OutlineWidth", settings.width);
				cmd.SetGlobalColor("_OutlineColor", settings.color);
				Blit(cmd, jfaIter % 2 == 0 ? pingId : pongId, cameraColor, outlineMaterial, PASS_JFAOUTLINE);
			}
			context.ExecuteCommandBuffer(cmd);
		}

		private void SobelPass(CommandBuffer cmd, ScriptableRenderContext context, RenderingData renderingData)
		{
			cmd.Clear();
			using (new ProfilingScope(cmd, m_ProfilingSampler))
			{
				cmd.SetGlobalColor("_OutlineColor", settings.color);
				Blit(cmd, maskId, cameraColor, outlineMaterial, PASS_SOBELOUTLINE);
			}
			context.ExecuteCommandBuffer(cmd);
		}
	}
}
