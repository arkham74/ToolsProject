using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace JD.PathTrace
{
	public class PathTraceFeature : ScriptableRendererFeature
	{
		[SerializeField][HideInInspector] private Shader shader;
		private PathTracePass pass;

		public override void Create()
		{
			pass = new PathTracePass(shader);
			pass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			renderer.EnqueuePass(pass);
		}

		protected override void Dispose(bool disposing)
		{
			pass.Dispose(disposing);
			base.Dispose(disposing);
		}
	}
}