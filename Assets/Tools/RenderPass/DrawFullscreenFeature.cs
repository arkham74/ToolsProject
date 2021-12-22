using System;

#if TOOLS_URP
namespace UnityEngine.Rendering.Universal
{
	public enum BufferType
	{
		CAMERA_COLOR,
		CUSTOM
	}

	public class DrawFullscreenFeature : ScriptableRendererFeature
	{
		[Serializable]
		public class Settings
		{
			public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingOpaques;

			public Material blitMaterial;
			public int blitMaterialPassIndex = -1;
			public BufferType sourceType = BufferType.CAMERA_COLOR;
			public BufferType destinationType = BufferType.CAMERA_COLOR;
			public string sourceTextureId = "_SourceTexture";
			public string destinationTextureId = "_DestinationTexture";
		}

		public Settings settings = new Settings();
		private DrawFullscreenPass blitPass;

		public override void Create()
		{
			blitPass = new DrawFullscreenPass(name);
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			if (settings.blitMaterial == null)
			{
				Debug.LogWarningFormat(
					"Missing Blit Material. {0} blit pass will not execute. Check for missing reference in the assigned renderer.",
					GetType().Name);
				return;
			}

			blitPass.renderPassEvent = settings.renderPassEvent;
			blitPass.settings = settings;
			renderer.EnqueuePass(blitPass);
		}
	}
}
#endif