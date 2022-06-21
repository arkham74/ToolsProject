#if TOOLS_URP
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Cyan
{
	public class BlitPass : ScriptableRenderPass
	{
		public Material blitMaterial = null;
		public FilterMode filterMode;
		private RenderTargetIdentifier source;
		private RenderTargetIdentifier destination;
		private RenderTargetHandle m_TemporaryColorTexture;
		private RenderTargetHandle m_DestinationTexture;
		private readonly BlitSettings settings;
		private readonly string m_ProfilerTag;

		public BlitPass(RenderPassEvent renderPassEvent, BlitSettings settings, string tag)
		{
			this.renderPassEvent = renderPassEvent;
			this.settings = settings;
			blitMaterial = settings.blitMaterial;
			m_ProfilerTag = tag;
			m_TemporaryColorTexture.Init("_TemporaryColorTexture");
			if (settings.dstType == Target.TextureID)
			{
				m_DestinationTexture.Init(settings.dstTextureId);
			}
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			if (settings.requireDepthNormals)
				ConfigureInput(ScriptableRenderPassInput.Normal);
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer cmd = CommandBufferPool.Get(m_ProfilerTag);
			RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
			opaqueDesc.depthBufferBits = 0;

			// Set Source / Destination
			var renderer = renderingData.cameraData.renderer;

			// note : Seems this has to be done in here rather than in AddRenderPasses to work correctly in 2021.2+
			if (settings.srcType == Target.CameraColor)
			{
				source = renderer.cameraColorTarget;
			}
			else if (settings.srcType == Target.TextureID)
			{
				source = new RenderTargetIdentifier(settings.srcTextureId);
			}
			else if (settings.srcType == Target.RenderTextureObject)
			{
				source = new RenderTargetIdentifier(settings.srcTextureObject);
			}

			if (settings.dstType == Target.CameraColor)
			{
				destination = renderer.cameraColorTarget;
			}
			else if (settings.dstType == Target.TextureID)
			{
				destination = new RenderTargetIdentifier(settings.dstTextureId);
			}
			else if (settings.dstType == Target.RenderTextureObject)
			{
				destination = new RenderTargetIdentifier(settings.dstTextureObject);
			}

			if (settings.setInverseViewMatrix)
			{
				Shader.SetGlobalMatrix("_InverseView", renderingData.cameraData.camera.cameraToWorldMatrix);
			}

			if (settings.dstType == Target.TextureID)
			{
				if (settings.overrideGraphicsFormat)
				{
					opaqueDesc.graphicsFormat = settings.graphicsFormat;
				}
				cmd.GetTemporaryRT(m_DestinationTexture.id, opaqueDesc, filterMode);
			}

			Shader.SetGlobalFloat("_UnscaledTime", Time.unscaledTime);

			//Debug.Log($"src = {source},     dst = {destination} ");
			// Can't read and write to same color target, use a TemporaryRT
			if (source == destination || (settings.srcType == settings.dstType && settings.srcType == Target.CameraColor))
			{
				cmd.GetTemporaryRT(m_TemporaryColorTexture.id, opaqueDesc, filterMode);
				Blit(cmd, source, m_TemporaryColorTexture.Identifier(), blitMaterial, settings.blitMaterialPassIndex);
				Blit(cmd, m_TemporaryColorTexture.Identifier(), destination);
			}
			else
			{
				Blit(cmd, source, destination, blitMaterial, settings.blitMaterialPassIndex);
			}

			context.ExecuteCommandBuffer(cmd);
			CommandBufferPool.Release(cmd);
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			base.OnCameraCleanup(cmd);

			if (settings.dstType == Target.TextureID)
			{
				cmd.ReleaseTemporaryRT(m_DestinationTexture.id);
			}
			if (source == destination || (settings.srcType == settings.dstType && settings.srcType == Target.CameraColor))
			{
				cmd.ReleaseTemporaryRT(m_TemporaryColorTexture.id);
			}
		}
	}
}
#endif
