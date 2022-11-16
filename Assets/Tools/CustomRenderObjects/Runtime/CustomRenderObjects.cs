#if TOOLS_URP
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace JD.CustomRenderObjects
{
	public class CustomRenderObjects : ScriptableRendererFeature
	{
		[SerializeField] private CustomRenderObjectsSettings settings = new CustomRenderObjectsSettings();
		private CustomRenderObjectsPass renderObjectsPass;

		public override void Create()
		{
			renderObjectsPass = new CustomRenderObjectsPass(settings);
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			renderer.EnqueuePass(renderObjectsPass);
		}
	}
}
#endif