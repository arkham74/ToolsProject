using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

#if TOOLS_URP
namespace UnityEngine.Rendering.Universal
{
	public partial class DownscaleFeature : ScriptableRendererFeature
	{
		private DownscalePass scriptablePass;
		public int targetRes = 180;
		// public Vector2Int targetRes = new Vector2Int(320, 180);
		// public GraphicsFormat graphicsFormat = GraphicsFormat.R8G8B8A8_SRGB;
		public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingTransparents;

		/// <inheritdoc/>
		public override void Create()
		{
			scriptablePass = new DownscalePass
			{
				profilerTag = name,
				targetRes = targetRes,
				// graphicsFormat = graphicsFormat,
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