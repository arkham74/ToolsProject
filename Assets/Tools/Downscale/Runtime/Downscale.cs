#if TOOLS_URP
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD
{
	[DisallowMultipleRendererFeature]
	public class Downscale : ScriptableRendererFeature
	{
		[SerializeField][Min(1)] private int targetRes = 45;
		[SerializeField] private bool bilinear = false;
		[SerializeField] private RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;

		private DownscalePass scriptablePass;

		public override void Create()
		{
			scriptablePass = new DownscalePass(name, targetRes, bilinear);
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			scriptablePass.renderPassEvent = renderPassEvent;
			renderer.EnqueuePass(scriptablePass);
		}
	}
}
#endif