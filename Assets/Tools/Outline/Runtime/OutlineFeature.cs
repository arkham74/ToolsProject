using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace JD.Outline
{
	public class OutlineFeature : ScriptableRendererFeature
	{
		[SerializeField] private OutlineSettings settings;
		[SerializeField][HideInInspector] private Material material;
		private OutlinePass pass;

		public override void Create()
		{
			if (settings && settings.width > 0 && material)
				pass = new OutlinePass(name, settings, material);
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			if (settings && settings.width > 0 && material)
			{
				pass.renderPassEvent = settings.passEvent;
				renderer.EnqueuePass(pass);
			}
		}
	}
}
