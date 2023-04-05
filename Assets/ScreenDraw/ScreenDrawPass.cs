using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.ScreenDraw
{
	public class ScreenDrawPass : ScriptableRenderPass
	{
		public static readonly int lineCountId = Shader.PropertyToID("_LineCount");
		public static readonly int linesId = Shader.PropertyToID("_Lines");

		public Material material;

		public static readonly List<ScreenDrawLine> lines = new List<ScreenDrawLine>();
		public ComputeBuffer buffer;

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (!renderingData.cameraData.isPreviewCamera && material)
			{
				ScreenDraw.OnUpdate.Invoke();
				int count = lines.Count;
				if (count > 0)
				{
					CommandBuffer cmd = CommandBufferPool.Get("Screen Draw Pass");
					ScriptableRenderer renderer = renderingData.cameraData.renderer;
					RenderTargetIdentifier cameraColorTarget = renderer.cameraColorTarget;
					RenderTargetIdentifier cameraDepthTarget = renderer.cameraDepthTarget;
					int stride = Marshal.SizeOf<ScreenDrawLine>();
					buffer?.Release();
					buffer = new ComputeBuffer(count, stride, ComputeBufferType.Default);
					buffer.SetData(lines);

					cmd.SetGlobalBuffer(linesId, buffer);
					cmd.SetGlobalInteger(lineCountId, count);

					cmd.Blit(null, cameraColorTarget, material);

					context.ExecuteCommandBuffer(cmd);
					CommandBufferPool.Release(cmd);
					lines.Clear();
				}
			}
		}

		public static void AddLine(Vector3 start, Vector3 end, Color color, float width)
		{
			lines.Add(new ScreenDrawLine(start, end, color, width));
		}
	}
}