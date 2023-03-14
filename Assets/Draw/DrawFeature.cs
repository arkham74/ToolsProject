using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace JD.Draw
{
	internal class DrawFeature : ScriptableRendererFeature
	{
		[SerializeField] private RenderPassEvent passEvent = RenderPassEvent.BeforeRenderingPostProcessing;
		[SerializeField] private Material material;

		private DrawPass pass;

		public override void Create()
		{
			pass = new DrawPass();
			pass.renderPassEvent = passEvent;
			pass.material = material;
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			renderer.EnqueuePass(pass);
		}
	}
}