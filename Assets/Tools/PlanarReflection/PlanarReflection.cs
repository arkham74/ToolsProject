#if OUTLINE_URP
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.PlanarReflection
{
	[DisallowMultipleRendererFeature]
	public class PlanarReflection : ScriptableRendererFeature
	{
		[SerializeField] private PlanarReflectionSettings settings;
		private PlanarReflectionPass pass;

		public override void Create()
		{
			pass = new PlanarReflectionPass(settings);
			pass.renderPassEvent = RenderPassEvent.BeforeRenderingOpaques;
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			renderer.EnqueuePass(pass);
		}
	}
}
#endif