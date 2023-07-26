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
			renderPassEvent = RenderPassEvent.BeforeRenderingGbuffer,
			disableSSAO = true,
			useMips = false,
			sceneView = true,
		};

		private PlanarReflectionPass pass;

		public override void Create()
		{
			pass = new PlanarReflectionPass();
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			pass.SetupPass(settings);
			pass.renderPassEvent = settings.renderPassEvent;
			renderer.EnqueuePass(pass);
		}
	}
}
#endif