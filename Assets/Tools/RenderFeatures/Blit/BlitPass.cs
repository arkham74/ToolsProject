using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/*
 * Blit Renderer Feature                                                https://github.com/Cyanilux/URP_BlitRenderFeature
 * ------------------------------------------------------------------------------------------------------------------------
 * Based on the Blit from the UniversalRenderingExamples
 * https://github.com/Unity-Technologies/UniversalRenderingExamples/tree/master/Assets/Scripts/Runtime/RenderPasses
 * 
 * Extended to allow for :
 * - Specific access to selecting a source and destination (via current camera's color / texture id / render texture object
 * - (Pre-2021.2/v12) Automatic switching to using _AfterPostProcessTexture for After Rendering event, in order to correctly handle the blit after post processing is applied
 * - Setting a _InverseView matrix (cameraToWorldMatrix), for shaders that might need it to handle calculations from screen space to world.
 * 		e.g. Reconstruct world pos from depth : https://www.cyanilux.com/tutorials/depth/#blit-perspective 
 * - (2020.2/v10 +) Enabling generation of DepthNormals (_CameraNormalsTexture)
 * 		This will only include shaders who have a DepthNormals pass (mostly Lit Shaders / Graphs)
 		(workaround for Unlit Shaders / Graphs: https://gist.github.com/Cyanilux/be5a796cf6ddb20f20a586b94be93f2b)
 * ------------------------------------------------------------------------------------------------------------------------
 * @Cyanilux
*/

namespace Cyan
{
	public partial class BlitFeature
	{
		public class BlitPass : ScriptableRenderPass
		{
			public Material blitMaterial;
			public FilterMode filterMode;

			private BlitSettings settings;

			private RenderTargetIdentifier source;
			private RenderTargetIdentifier destination;

			RenderTargetHandle m_TemporaryColorTexture;
			RenderTargetHandle m_DestinationTexture;
			string m_ProfilerTag;

#if !UNITY_2020_2_OR_NEWER // v8
			private ScriptableRenderer renderer;
#endif

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

			public void Setup(ScriptableRenderer renderer)
			{
#if UNITY_2020_2_OR_NEWER // v10+
				if (settings.requireDepthNormals)
					ConfigureInput(ScriptableRenderPassInput.Normal);
#else // v8
				this.renderer = renderer;
#endif
			}

			public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
			{
				CommandBuffer cmd = CommandBufferPool.Get(m_ProfilerTag);
				RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
				opaqueDesc.depthBufferBits = 0;

				// Set Source / Destination
#if UNITY_2020_2_OR_NEWER // v10+
				var renderer = renderingData.cameraData.renderer;
#else // v8
				// For older versions, cameraData.renderer is internal so can't be accessed. Will pass it through from AddRenderPasses instead
				var renderer = this.renderer;
#endif

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

			public override void FrameCleanup(CommandBuffer cmd)
			{
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
}