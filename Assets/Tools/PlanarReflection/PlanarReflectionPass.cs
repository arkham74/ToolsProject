#if OUTLINE_URP
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;
using UnityEngine.Rendering.Universal;

namespace JD.PlanarReflection
{
	public class PlanarReflectionPass : ScriptableRenderPass
	{
		private static readonly int planarTexId = Shader.PropertyToID("_PlanarTex");
		private PlanarReflectionSettings settings;

		public PlanarReflectionPass(PlanarReflectionSettings settings)
		{
			this.settings = settings;
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			RenderTextureDescriptor desc = renderingData.cameraData.cameraTargetDescriptor;
			cmd.GetTemporaryRT(planarTexId, desc);
		}

		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			ConfigureInput(ScriptableRenderPassInput.Color | ScriptableRenderPassInput.Depth);
			ConfigureTarget(planarTexId);
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CameraType cameraType = renderingData.cameraData.cameraType;
			if (cameraType != CameraType.Preview && cameraType != CameraType.VR && cameraType != CameraType.Reflection)
			{
				CommandBuffer cmd = CommandBufferPool.Get();

				using (new FrameScope(context, cmd, "Planar Reflection"))
				{
					if (settings.disableSSAO)
					{
						cmd.DisableShaderKeyword(ShaderKeywordStrings.ScreenSpaceOcclusion);
						context.ExecuteCommandBuffer(cmd);
						cmd.Clear();
					}

					Clear(cmd, ref context);
					Override(cmd, ref context, ref renderingData);
					Draw(ref context, ref renderingData);
					Restore(cmd, ref context, ref renderingData);
					ScreenPass(cmd, ref context, ref renderingData);

					if (settings.disableSSAO)
					{
						cmd.EnableShaderKeyword(ShaderKeywordStrings.ScreenSpaceOcclusion);
						context.ExecuteCommandBuffer(cmd);
						cmd.Clear();
					}
					else
					{
						cmd.DisableShaderKeyword(ShaderKeywordStrings.ScreenSpaceOcclusion);
						context.ExecuteCommandBuffer(cmd);
						cmd.Clear();
					}
				}

				CommandBufferPool.Release(cmd);
			}
		}

		private void ScreenPass(CommandBuffer cmd, ref ScriptableRenderContext context, ref RenderingData renderingData)
		{
			cmd.Clear();
			RenderTargetIdentifier cameraColorTarget = renderingData.cameraData.renderer.cameraColorTarget;
			Blit(cmd, planarTexId, cameraColorTarget, settings.screenPassMaterial);
			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();
		}

		private void Override(CommandBuffer cmd, ref ScriptableRenderContext context, ref RenderingData renderingData)
		{
			Matrix4x4 projectionMatrix = renderingData.cameraData.GetGPUProjectionMatrix();
			Matrix4x4 viewMatrix = renderingData.cameraData.GetViewMatrix();

			Vector4 right = viewMatrix.GetColumn(0);
			Vector4 up = viewMatrix.GetColumn(1);
			Vector4 forward = viewMatrix.GetColumn(2);
			Vector4 position = viewMatrix.GetColumn(3);

			up = -PlanarReflectionUtils.MirrorDirection(up, settings.normal);
			forward = PlanarReflectionUtils.MirrorDirection(forward, settings.normal);
			right = Vector3.Cross(forward, up);

			position = PlanarReflectionUtils.MirrorPosition(position, settings.position, settings.normal);

			viewMatrix.SetColumn(0, right);
			viewMatrix.SetColumn(1, up);
			viewMatrix.SetColumn(2, forward);
			viewMatrix.SetColumn(3, position);

			cmd.Clear();
			RenderingUtils.SetViewAndProjectionMatrices(cmd, viewMatrix, projectionMatrix, false);
			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();
		}


		private void Restore(CommandBuffer cmd, ref ScriptableRenderContext context, ref RenderingData renderingData)
		{
			cmd.Clear();
			RenderingUtils.SetViewAndProjectionMatrices(cmd, renderingData.cameraData.GetViewMatrix(), renderingData.cameraData.GetGPUProjectionMatrix(), false);
			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();
		}

		private void Clear(CommandBuffer cmd, ref ScriptableRenderContext context)
		{
			cmd.Clear();
			cmd.ClearRenderTarget(true, true, Color.clear);
			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();
		}

		private void Draw(ref ScriptableRenderContext context, ref RenderingData renderingData)
		{
			ShaderTagId shaderTagId = new ShaderTagId("UniversalForward");
			DrawingSettings drawingSettings = CreateDrawingSettings(shaderTagId, ref renderingData, SortingCriteria.CommonOpaque);
			FilteringSettings filteringSettings = new FilteringSettings(RenderQueueRange.opaque, settings.layer, settings.renderingLayer);
			context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings);
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			cmd.ReleaseTemporaryRT(planarTexId);
		}
	}
}
#endif