using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.Draw
{
	internal class DrawPass : ScriptableRenderPass
	{
		private const int MAX_LEN = 1023;
		private static readonly int targetID = Shader.PropertyToID("_DrawTarget");

		private static readonly int lineCountId = Shader.PropertyToID("_LineCount");
		private static readonly int lineStartId = Shader.PropertyToID("_LineStart");
		private static readonly int lineEndId = Shader.PropertyToID("_LineEnd");
		private static readonly int lineColorId = Shader.PropertyToID("_LineColor");

		private static Vector4[] lineStart = new Vector4[MAX_LEN];
		private static Vector4[] lineEnd = new Vector4[MAX_LEN];
		private static Vector4[] lineColor = new Vector4[MAX_LEN];
		private static int lineCount;

		internal Material material;

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			RenderTextureDescriptor desc = renderingData.cameraData.cameraTargetDescriptor;
			desc.msaaSamples = 1;
			desc.bindMS = false;
			desc.width = desc.width.Max(1);
			desc.height = desc.height.Max(1);
			cmd.GetTemporaryRT(targetID, desc);
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (!renderingData.cameraData.isPreviewCamera && material)
			{
				CommandBuffer cmd = CommandBufferPool.Get("Draw Pass");

				ScriptableRenderer renderer = renderingData.cameraData.renderer;
				RenderTargetIdentifier cameraColorTarget = renderer.cameraColorTarget;
				RenderTargetIdentifier cameraDepthTarget = renderer.cameraDepthTarget;

				lineCount = 0;
				Draw.OnUpdate.Invoke();

				cmd.SetGlobalInteger(lineCountId, lineCount);
				cmd.SetGlobalVectorArray(lineStartId, lineStart);
				cmd.SetGlobalVectorArray(lineEndId, lineEnd);
				cmd.SetGlobalVectorArray(lineColorId, lineColor);

				Blit(cmd, cameraColorTarget, targetID, material);
				Blit(cmd, targetID, cameraColorTarget);

				context.ExecuteCommandBuffer(cmd);
				CommandBufferPool.Release(cmd);
			}
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			cmd.ReleaseTemporaryRT(targetID);
		}

		internal static void AddLine(Line line)
		{
			if (lineCount >= MAX_LEN) return;
			Vector3 start = line.start;
			Vector3 end = line.end;
			Vector4 packStart = new Vector4(start.x, start.y, start.z, line.width);
			Vector4 packEnd = new Vector4(end.x, end.y, end.z, line.width);
			lineStart[lineCount] = packStart;
			lineEnd[lineCount] = packEnd;
			lineColor[lineCount] = line.color;
			lineCount++;
		}
	}
}