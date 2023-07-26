//based on https://github.com/Kink3d/kMirrors/blob/master/Runtime/Mirror.cs

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

		public void SetupPass(PlanarReflectionSettings settings)
		{
			this.settings = settings;
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			RenderTextureDescriptor desc = renderingData.cameraData.cameraTargetDescriptor;
			if (settings.useMips)
			{
				desc.autoGenerateMips = true;
				desc.useMipMap = true;
			}
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
				if (cameraType == CameraType.SceneView && !settings.sceneView) return;

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

		private void Override(CommandBuffer cmd, ref ScriptableRenderContext context, ref RenderingData renderingData)
		{
			Vector3 planeNormal = Vector3.up;
			Vector3 planePosition = Vector3.zero;

			Matrix4x4 viewMatrix = GetViewMatrix(ref renderingData, planeNormal, planePosition);
			Matrix4x4 projectionMatrix = GetProjectionMatrix(ref renderingData, viewMatrix, planeNormal, planePosition);

			cmd.Clear();
			RenderingUtils.SetViewAndProjectionMatrices(cmd, viewMatrix, projectionMatrix, false);
			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();
		}

		private static Matrix4x4 GetProjectionMatrix(ref RenderingData renderingData, Matrix4x4 viewMatrix, Vector3 planeNormal, Vector3 planePosition)
		{
			Vector4 mirrorPlane = GetMirrorPlane(viewMatrix, planeNormal, planeNormal);
			Matrix4x4 projMat = renderingData.cameraData.camera.CalculateObliqueMatrix(mirrorPlane);
			return GL.GetGPUProjectionMatrix(projMat, true);
		}

		private static Vector4 GetMirrorPlane(Matrix4x4 viewMatrix, Vector3 planeNormal, Vector3 planePosition)
		{
			var offsetPos = planePosition - planeNormal * 0.99f;
			var cpos = viewMatrix.MultiplyPoint(offsetPos);
			var cnormal = viewMatrix.MultiplyVector(planeNormal).normalized;
			return new Vector4(cnormal.x, cnormal.y, cnormal.z, -Vector3.Dot(cpos, cnormal));
		}

		private static Matrix4x4 GetViewMatrix(ref RenderingData renderingData, Vector3 planeNormal, Vector3 planePosition)
		{
			Matrix4x4 viewMatrix = renderingData.cameraData.GetViewMatrix();
			Vector4 right = viewMatrix.GetColumn(0);
			Vector4 up = viewMatrix.GetColumn(1);
			Vector4 forward = viewMatrix.GetColumn(2);
			Vector4 position = viewMatrix.GetColumn(3);

			right = PlanarReflectionUtils.MirrorDirection(right, planeNormal);
			up = -PlanarReflectionUtils.MirrorDirection(up, planeNormal);
			forward = PlanarReflectionUtils.MirrorDirection(forward, planeNormal);
			position = PlanarReflectionUtils.MirrorPosition(position, planePosition, planeNormal);

			viewMatrix.SetColumn(0, right);
			viewMatrix.SetColumn(1, up);
			viewMatrix.SetColumn(2, forward);
			viewMatrix.SetColumn(3, position);
			return viewMatrix;
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
			if (settings.renderSkybox)
			{
				context.DrawSkybox(renderingData.cameraData.camera);
			}
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			cmd.ReleaseTemporaryRT(planarTexId);
		}
	}
}
#endif