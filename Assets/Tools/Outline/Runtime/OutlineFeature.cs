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
			if (settings == null) return;
			if (material == null) return;

			passList.Clear();
			foreach (OutlineSettings set in settings)
			{
				if (set)
				{
					OutlinePass outlinePass = new OutlinePass(name, set, material);
					passList.Add(outlinePass);
				}
			}
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			foreach (OutlinePass outlinePass in passList)
			{
				if (outlinePass.settings.width > 0)
				{
					RenderPassEvent outlinePassEvent = outlinePass.settings.passEvent;
					outlinePass.renderPassEvent = outlinePassEvent;
					renderer.EnqueuePass(outlinePass);
				}
			}
		}
	}
}
