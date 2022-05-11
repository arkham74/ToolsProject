#if TOOLS_URP
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Cyan
{
	public enum Target
	{
		CameraColor,
		TextureID,
		RenderTextureObject
	}

	[Serializable]
	public class BlitSettings
	{
		public RenderPassEvent Event = RenderPassEvent.AfterRenderingOpaques;

		public Material blitMaterial = null;
		public int blitMaterialPassIndex = 0;
		public bool setInverseViewMatrix = false;
		public bool requireDepthNormals = false;

		public Target srcType = Target.CameraColor;
		public string srcTextureId = "_CameraColorTexture";
		public RenderTexture srcTextureObject;

		public Target dstType = Target.CameraColor;
		public string dstTextureId = "_BlitPassTexture";
		public RenderTexture dstTextureObject;

		public bool overrideGraphicsFormat = false;
		public UnityEngine.Experimental.Rendering.GraphicsFormat graphicsFormat;

	}
}
#endif