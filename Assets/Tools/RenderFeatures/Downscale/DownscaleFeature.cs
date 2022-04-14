using UnityEngine;
using UnityEngine.Rendering;

#if TOOLS_URP
namespace UnityEngine.Rendering.Universal
{
	public partial class DownscaleFeature : ScriptableRendererFeature
	{
		private DownscalePass scriptablePass;
		[Min(1)] public int targetRes = 180;
		public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingTransparents;

		/// <inheritdoc/>
		public override void Create()
		{
			scriptablePass = new DownscalePass
			{
				profilerTag = name,
				targetRes = targetRes,
				renderPassEvent = renderPassEvent
			};
		}

		// Here you can inject one or multiple render passes in the renderer.
		// This method is called when setting up the renderer once per-camera.
		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			scriptablePass.source = renderer.cameraColorTarget;
			renderer.EnqueuePass(scriptablePass);
		}
	}
}
#endif