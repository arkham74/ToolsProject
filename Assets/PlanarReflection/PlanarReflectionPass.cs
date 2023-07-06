using System;
using System.Collections.Generic;
using Freya;
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
			CommandBuffer cmd = CommandBufferPool.Get();

			using (new FrameScope(context, cmd, "Planar Reflection"))
			{
				Clear(cmd, ref context);
				Override(cmd, ref context, ref renderingData);
				Draw(ref context, ref renderingData);
				Restore(cmd, ref context, ref renderingData);
			}

			CommandBufferPool.Release(cmd);
		}

		private void Override(CommandBuffer cmd, ref ScriptableRenderContext context, ref RenderingData renderingData)
		{
			Matrix4x4 projectionMatrix = renderingData.cameraData.GetGPUProjectionMatrix();
			Matrix4x4 viewMatrix = renderingData.cameraData.GetViewMatrix();

			Matrix4x4 reflectionMatrix = PlanarReflectionUtils.CalculateReflectionMatrix(settings.planePosition, settings.planeNormal);

			viewMatrix = reflectionMatrix * viewMatrix;

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
			FilteringSettings filteringSettings = FilteringSettings.defaultValue;
			context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings);
			context.DrawSkybox(renderingData.cameraData.camera);
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			cmd.ReleaseTemporaryRT(planarTexId);
		}
	}
}


