using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.Outline
{
	public class OutlineFeature : ScriptableRendererFeature
	{
		[SerializeField] private bool sceneView;
		[SerializeField] private RenderPassEvent outlineEvent = RenderPassEvent.BeforeRenderingPostProcessing;
		[SerializeField] private Color outlineColor = Color.white;
		[SerializeField] private Color backgroundColor = Color.black;

		private CameraPass cameraPass;
		// private SurfacePass surfacePass;
		// private OutlinePass outlinePass;
		// private ColorDepthNormalPass colorDepthNormalPass;

		public override void Create()
		{
			// surfacePass = new SurfacePass();
			// outlinePass = new OutlinePass();
			// colorDepthNormalPass = new ColorDepthNormalPass();
			cameraPass = new CameraPass();
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			// outlinePass.material = material;
			// outlinePass.renderPassEvent = outlineEvent;
			// outlinePass.width = width;
			// outlinePass.outlineColor = outlineColor;
			// outlinePass.backgroundColor = backgroundColor;

			// surfacePass.material = material;
			// surfacePass.renderPassEvent = surfaceEvent;
			// surfacePass.layerMask = layerMask;
			// surfacePass.renderingLayerMask = renderingLayerMask;

			// colorDepthNormalPass.material = material;
			// colorDepthNormalPass.renderPassEvent = outlineEvent;

			cameraPass.renderPassEvent = outlineEvent;
			cameraPass.outlineColor = outlineColor;
			cameraPass.backgroundColor = backgroundColor;
			cameraPass.sceneView = sceneView;

			// renderer.EnqueuePass(colorDepthNormalPass);
			// renderer.EnqueuePass(surfacePass);
			// renderer.EnqueuePass(outlinePass);
			renderer.EnqueuePass(cameraPass);
		}
	}
}
