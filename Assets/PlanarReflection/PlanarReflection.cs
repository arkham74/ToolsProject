using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.PlanarReflection
{
	public class PlanarReflection : ScriptableRendererFeature
	{
		[SerializeField] private RenderPassEvent renderEvent = RenderPassEvent.BeforeRenderingOpaques;
		private PlanarReflectionPass pass;

		public override void Create()
		{
			pass = new PlanarReflectionPass();
			pass.renderPassEvent = renderEvent;
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			renderer.EnqueuePass(pass);
		}
	}
}


