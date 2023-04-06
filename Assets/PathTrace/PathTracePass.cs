using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.PathTrace
{
	public class PathTracePass : ScriptableRenderPass
	{
		public PathTraceSettings settings;
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
			CommandBuffer cmd = CommandBufferPool.Get("PathTrace");
			RenderTargetIdentifier colorTarget = renderingData.cameraData.renderer.cameraColorTarget;

			var spheres = PathTrace.Spheres.Select(e => e.ToSphere()).ToList();
			int count = spheres.Count;
			int stride = Marshal.SizeOf<Sphere>();

			buffer?.Release();
			buffer = new ComputeBuffer(count, stride, ComputeBufferType.Default);
			buffer.SetData(spheres);

			cmd.SetGlobalInteger("_Bounces", settings.bounces);
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