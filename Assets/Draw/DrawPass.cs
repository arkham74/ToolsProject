using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.Draw
{
	internal class DrawPass : ScriptableRenderPass
	{
		private static readonly int targetID = Shader.PropertyToID("_DrawTarget");
		private static readonly int lineCountId = Shader.PropertyToID("_LineCount");
		private static readonly int linesId = Shader.PropertyToID("_Lines");

		internal Material material;

		private static readonly List<Line> lines = new List<Line>();
		private ComputeBuffer buffer;

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

				Draw.OnUpdate.Invoke();
				int count = lines.Count;
				int stride = Marshal.SizeOf<Line>();
				buffer?.Release();
				buffer = new ComputeBuffer(count, stride, ComputeBufferType.Default);
				buffer.SetData(lines);

				cmd.SetGlobalBuffer(linesId, buffer);
				cmd.SetGlobalInteger(lineCountId, count);

				Blit(cmd, cameraColorTarget, targetID, material);
				Blit(cmd, targetID, cameraColorTarget);

				context.ExecuteCommandBuffer(cmd);
				CommandBufferPool.Release(cmd);
				lines.Clear();
			}
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			cmd.ReleaseTemporaryRT(targetID);
		}

		internal static void AddLine(Line line)
		{
			lines.Add(line);
		}
	}
}