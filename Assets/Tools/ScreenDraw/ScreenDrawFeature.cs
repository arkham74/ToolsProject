using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.ScreenDraw
{
	public class ScreenDrawFeature : ScriptableRendererFeature
	{
		[SerializeField] private RenderPassEvent passEvent = RenderPassEvent.BeforeRenderingPostProcessing;
		[HideInInspector][SerializeField] private Shader shader;

		public ScreenDrawPass pass;

		public override void Create()
		{
			pass = new ScreenDrawPass();
			pass.renderPassEvent = passEvent;
			pass.material = CoreUtils.CreateEngineMaterial(shader);
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			renderer.EnqueuePass(pass);
		}

		protected override void Dispose(bool disposing)
		{
			pass?.Dispose(disposing);
			base.Dispose(disposing);
		}
	}
}