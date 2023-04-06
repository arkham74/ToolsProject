using System;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.PathTrace
{
	public class PathTracePass : ScriptableRenderPass
	{
		private readonly int stride = Marshal.SizeOf<Sphere>();

		private ComputeBuffer buffer;
		private Material material;

		public PathTracePass(Shader shader)
		{
			material = CoreUtils.CreateEngineMaterial(shader);
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (renderingData.cameraData.isPreviewCamera) return;

			int bounces = 2;
			bool scene = true;
			PathTraceVolumeComponent comp = VolumeManager.instance.stack.GetComponent<PathTraceVolumeComponent>();
			if (comp)
			{
				bounces = comp.bounces.value;
				scene = comp.scene.value;
			}

			if (renderingData.cameraData.isSceneViewCamera && !scene) return;

			CommandBuffer cmd = CommandBufferPool.Get("PathTrace");
			RenderTargetIdentifier colorTarget = renderingData.cameraData.renderer.cameraColorTarget;

			var spheres = PathTrace.Spheres.Select(e => e.ToSphere()).ToList();
			int count = spheres.Count;

			buffer?.Release();
			buffer = new ComputeBuffer(count, stride, ComputeBufferType.Default);
			buffer.SetData(spheres);

			cmd.SetGlobalInteger("_Bounces", bounces);
			cmd.SetGlobalBuffer("_Spheres", buffer);
			cmd.SetGlobalInteger("_SphereCount", count);
			cmd.Blit(null, colorTarget, material);

			context.ExecuteCommandBuffer(cmd);
			CommandBufferPool.Release(cmd);
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
		}
	}
}