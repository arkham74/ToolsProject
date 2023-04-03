using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace JD.PathTrace
{
	public class PathTraceFeature : ScriptableRendererFeature
	{
		[SerializeField] private PathTraceSettings settings;
		private PathTracePass pass;

		public override void Create()
		{
			pass = new PathTracePass();
			pass.renderPassEvent = RenderPassEvent.AfterRendering;
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			pass.settings = settings;
			renderer.EnqueuePass(pass);
		}
	}
}