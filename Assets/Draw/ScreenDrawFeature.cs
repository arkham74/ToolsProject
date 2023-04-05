using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace JD.ScreenDraw
{
	public class ScreenDrawFeature : ScriptableRendererFeature
	{
		[SerializeField] private RenderPassEvent passEvent = RenderPassEvent.BeforeRenderingPostProcessing;
		[SerializeField] private Material material;

		public ScreenDrawPass pass;

		public override void Create()
		{
			pass = new ScreenDrawPass();
			pass.renderPassEvent = passEvent;
			pass.material = material;
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			renderer.EnqueuePass(pass);
		}
	}
}