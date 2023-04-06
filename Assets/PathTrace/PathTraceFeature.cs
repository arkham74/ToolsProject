using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace JD.PathTrace
{
	public class PathTraceFeature : ScriptableRendererFeature
	{
		[SerializeField] private PathTraceSettings settings;
		[SerializeField][HideInInspector] private Shader shader;
		private PathTracePass pass;

		public override void Create()
		{
			pass = new PathTracePass(shader);
			pass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			pass.settings = settings;
			renderer.EnqueuePass(pass);
		}
	}
}