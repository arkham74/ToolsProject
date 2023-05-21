using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace JD.Outline
{
	public class OutlineFeature : ScriptableRendererFeature
	{
		[SerializeField][HideInInspector] private Material material;
		[SerializeField] private OutlineSettings[] settings;
		private readonly List<OutlinePass> passList = new List<OutlinePass>();

		public override void Create()
		{
			if (material == null) return;

			passList.Clear();
			foreach (OutlineSettings set in settings)
			{
				if (set && set.width > 0)
				{
					OutlinePass item = new OutlinePass(name, set, material);
					passList.Add(item);
				}
			}
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			foreach (OutlinePass pass in passList)
			{
				pass.renderPassEvent = pass.settings.passEvent;
				renderer.EnqueuePass(pass);
			}
		}
	}
}
