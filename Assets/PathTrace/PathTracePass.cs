using System;
using System.Collections.Generic;
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
		private readonly List<Sphere> spheres = new List<Sphere>();

		private ComputeBuffer buffer;
		private Material material;

		public PathTracePass(Shader shader)
		{
			material = CoreUtils.CreateEngineMaterial(shader);
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			if (buffer == null)
			{
				buffer = new ComputeBuffer(1000, stride, ComputeBufferType.Default);
			}
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CameraData cameraData = renderingData.cameraData;
			if (cameraData.isPreviewCamera) return;

			int bounces = 2;
			int samples = 10;
			bool scene = true;
			PathTraceVolumeComponent comp = VolumeManager.instance.stack.GetComponent<PathTraceVolumeComponent>();
			if (comp)
			{
				samples = comp.samples.value;
				bounces = comp.bounces.value;
				scene = comp.scene.value;
			}

			if (cameraData.isSceneViewCamera && !scene) return;

			CommandBuffer cmd = CommandBufferPool.Get("PathTrace");
			RenderTargetIdentifier colorTarget = cameraData.renderer.cameraColorTarget;

			for (int i = 0; i < PathTrace.Spheres.Count; i++)
			{
				spheres.Add(PathTrace.Spheres[i].ToSphere());
			}

			int count = spheres.Count;
			buffer.SetData(spheres);

			cmd.SetGlobalInteger("_Samples", samples);
			cmd.SetGlobalInteger("_Bounces", bounces);
			cmd.SetGlobalBuffer("_Spheres", buffer);
			cmd.SetGlobalInteger("_SphereCount", count);
			cmd.Blit(null, colorTarget, material);

			context.ExecuteCommandBuffer(cmd);
			CommandBufferPool.Release(cmd);
			spheres.Clear();
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
		}

		internal void Dispose(bool disposing)
		{
			buffer?.Release();
		}
	}
}