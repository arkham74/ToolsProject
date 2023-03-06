using System;
using System.ComponentModel;
using Freya;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.Outline
{
	public class OutlinePass : ScriptableRenderPass
	{
		private static readonly int maskId = Shader.PropertyToID("_OutlineTargetMask");
		private static readonly int pingId = Shader.PropertyToID("_Ping");
		private static readonly int pongId = Shader.PropertyToID("_Pong");
		private static readonly int stepId = Shader.PropertyToID("_StepWidth");
		private static readonly int widthId = Shader.PropertyToID("_OutlineWidth");
		private static readonly int colorId = Shader.PropertyToID("_OutlineColor");
		private static readonly int backId = Shader.PropertyToID("_BackgroundColor");

		public Material material;
		public int width;
		public Color outlineColor;
		public Color backgroundColor;
		private bool debug;

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			RenderTextureDescriptor desc = renderingData.cameraData.cameraTargetDescriptor;
			desc.msaaSamples = 1;
			desc.bindMS = false;
			desc.width = desc.width.Max(1);
			desc.height = desc.height.Max(1);
			desc.graphicsFormat = GraphicsFormat.R16G16_SNorm;
			cmd.GetTemporaryRT(pingId, desc);
			cmd.GetTemporaryRT(pongId, desc);
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			cmd.ReleaseTemporaryRT(pingId);
			cmd.ReleaseTemporaryRT(pongId);
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CameraData cameraData = renderingData.cameraData;
			if (!cameraData.isPreviewCamera && !cameraData.isSceneViewCamera)
			{
				CommandBuffer cmd = CommandBufferPool.Get("Outline Pass");
				if (Keyboard.current != null && Keyboard.current.tKey.wasReleasedThisFrame)
				{
					debug = !debug;
				}
				if (debug)
				{
					DebugPass(cmd, context, renderingData);
				}
				else
				{
					SobelPass(cmd, context, renderingData);
				}
				CommandBufferPool.Release(cmd);
			}
		}

		private void DebugPass(CommandBuffer cmd, ScriptableRenderContext context, RenderingData renderingData)
		{
			RenderTargetIdentifier cameraColor = renderingData.cameraData.renderer.cameraColorTarget;
			cmd.Clear();
			Blit(cmd, maskId, cameraColor);
			context.ExecuteCommandBuffer(cmd);
		}

		private void SobelPass(CommandBuffer cmd, ScriptableRenderContext context, RenderingData renderingData)
		{
			RenderTargetIdentifier cameraColor = renderingData.cameraData.renderer.cameraColorTarget;
			cmd.Clear();
			cmd.SetGlobalColor(colorId, outlineColor);
			cmd.SetGlobalColor(backId, backgroundColor);
			Blit(cmd, maskId, cameraColor, material, 3);
			context.ExecuteCommandBuffer(cmd);
		}

		private void JumpFloodPass(CommandBuffer cmd, ScriptableRenderContext context, RenderingData renderingData)
		{
			RenderTargetIdentifier cameraColor = renderingData.cameraData.renderer.cameraColorTarget;
			cmd.Clear();
			Blit(cmd, maskId, pongId, material, 0);
			int jfaIter = Mathf.CeilToInt(Mathfs.Log(width, 2)) - 1;

			for (int i = 0; i <= jfaIter; i++)
			{
				cmd.SetGlobalInteger(stepId, (int)Mathf.Pow(2, jfaIter - i));

				if (i % 2 == 0)
				{
					Blit(cmd, pongId, pingId, material, 1);
				}
				else
				{
					Blit(cmd, pingId, pongId, material, 1);
				}
			}

			cmd.SetGlobalFloat(widthId, width - 1);
			cmd.SetGlobalColor(colorId, outlineColor);
			cmd.SetGlobalColor(backId, backgroundColor);
			Blit(cmd, jfaIter % 2 == 0 ? pingId : pongId, cameraColor, material, 2);
			context.ExecuteCommandBuffer(cmd);
		}
	}
}
