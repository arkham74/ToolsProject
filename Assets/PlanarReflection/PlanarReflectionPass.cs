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
		private static readonly List<ShaderTagId> universalForwardTag = new List<ShaderTagId>
		{
			new ShaderTagId("SRPDefaultUnlit"),
			new ShaderTagId("UniversalForward"),
			new ShaderTagId("UniversalForwardOnly"),
		};

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

			using (new FrameScope(cmd, "Planar Reflection"))
			{
				cmd.ClearRenderTarget(true, true, Color.clear);

				Vector4 normal = Vector3.up;
				Vector4 pos = Vector4.zero;

				CameraData cameraData = renderingData.cameraData;

				Matrix4x4 gpuProjectionMatrix = cameraData.GetGPUProjectionMatrix();
				Matrix4x4 viewMatrix = cameraData.GetViewMatrix();
				Matrix4x4 projectionMatrix = cameraData.GetProjectionMatrix();

				float d = -Vector3.Dot(normal, pos);
				Vector4 reflectionPlane = new Vector4(normal.x, normal.y, normal.z, d);
				Matrix4x4 reflection = Matrix4x4.zero;
				CalculateReflectionMatrix(ref reflection, reflectionPlane);

				Matrix4x4 invertedViewMatrix = viewMatrix * reflection;

				Vector4 clipPlane = CameraSpacePlane(reflection, pos, normal, 1.0f);
				Matrix4x4 projection = cameraData.camera.CalculateObliqueMatrix(clipPlane);
				Matrix4x4 invertedProjectionMatrix = projection;

				RenderingUtils.SetViewAndProjectionMatrices(cmd, invertedViewMatrix, projectionMatrix, false);
				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();

				DrawingSettings drawingSettings = CreateDrawingSettings(universalForwardTag, ref renderingData, SortingCriteria.CommonOpaque);
				FilteringSettings filteringSettings = new FilteringSettings(RenderQueueRange.opaque, -1, uint.MaxValue);
				RenderStateBlock renderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
				context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings, ref renderStateBlock);
				context.DrawSkybox(cameraData.camera);

				RenderingUtils.SetViewAndProjectionMatrices(cmd, viewMatrix, gpuProjectionMatrix, false);

			}

			context.ExecuteCommandBuffer(cmd);
			CommandBufferPool.Release(cmd);
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			cmd.ReleaseTemporaryRT(planarTexId);
		}

		// Given position/normal of the plane, calculates plane in camera space.
		private Vector4 CameraSpacePlane(Matrix4x4 m, Vector3 pos, Vector3 normal, float sideSign)
		{
			Vector3 offsetPos = pos + normal;
			Vector3 cpos = m.MultiplyPoint(offsetPos);
			Vector3 cnormal = m.MultiplyVector(normal).normalized * sideSign;
			return new Vector4(cnormal.x, cnormal.y, cnormal.z, -Vector3.Dot(cpos, cnormal));
		}

		// Calculates reflection matrix around the given plane
		private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
		{
			reflectionMat.m00 = (1F - 2F * plane[0] * plane[0]);
			reflectionMat.m01 = (-2F * plane[0] * plane[1]);
			reflectionMat.m02 = (-2F * plane[0] * plane[2]);
			reflectionMat.m03 = (-2F * plane[3] * plane[0]);

			reflectionMat.m10 = (-2F * plane[1] * plane[0]);
			reflectionMat.m11 = (1F - 2F * plane[1] * plane[1]);
			reflectionMat.m12 = (-2F * plane[1] * plane[2]);
			reflectionMat.m13 = (-2F * plane[3] * plane[1]);

			reflectionMat.m20 = (-2F * plane[2] * plane[0]);
			reflectionMat.m21 = (-2F * plane[2] * plane[1]);
			reflectionMat.m22 = (1F - 2F * plane[2] * plane[2]);
			reflectionMat.m23 = (-2F * plane[3] * plane[2]);

			reflectionMat.m30 = 0F;
			reflectionMat.m31 = 0F;
			reflectionMat.m32 = 0F;
			reflectionMat.m33 = 1F;
		}
	}
}


