#if OUTLINE_URP
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.PlanarReflection
{
	[DisallowMultipleRendererFeature]
	public class PlanarReflection : ScriptableRendererFeature
	{
		[HideInInspector][SerializeField] private Material material;

		[SerializeField]
		private PlanarReflectionSettings settings = new PlanarReflectionSettings()
		{
			layer = -1,
			renderingLayer = uint.MaxValue,
			disableSSAO = true,
		};

		private PlanarReflectionPass pass;

		public override void Create()
		{
			pass = new PlanarReflectionPass(settings);
			pass.renderPassEvent = RenderPassEvent.BeforeRendering;
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			renderer.EnqueuePass(pass);
		}
	}
}
#endif