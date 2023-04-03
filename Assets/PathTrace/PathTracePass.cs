using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.PathTrace
{
	public class PathTracePass : ScriptableRenderPass
	{
		public PathTraceSettings settings;
		ComputeBuffer buffer;

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer cmd = CommandBufferPool.Get("PathTrace");
			RenderTargetIdentifier colorTarget = renderingData.cameraData.renderer.cameraColorTarget;

			var spheres = PathTrace.Spheres.Select(e => e.ToSphere()).ToList();
			int count = spheres.Count;
			int stride = sizeof(float) * 4;

			buffer?.Release();
			buffer = new ComputeBuffer(count, stride, ComputeBufferType.Default);
			buffer.SetData(spheres);

			cmd.SetGlobalBuffer("_Spheres", buffer);
			cmd.SetGlobalInteger("_SphereCount", count);
			cmd.Blit(null, colorTarget, settings.material);

			context.ExecuteCommandBuffer(cmd);
			CommandBufferPool.Release(cmd);
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
		}
	}
}