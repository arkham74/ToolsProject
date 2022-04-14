using UnityEngine;
using UnityEngine.Rendering.Universal;

/*
 * Blit Renderer Feature                                                https://github.com/Cyanilux/URP_BlitRenderFeature
 * ------------------------------------------------------------------------------------------------------------------------
 * Based on the Blit from the UniversalRenderingExamples
 * https://github.com/Unity-Technologies/UniversalRenderingExamples/tree/master/Assets/Scripts/Runtime/RenderPasses
 * 
 * Extended to allow for :
 * - Specific access to selecting a source and destination (via current camera's color / texture id / render texture object
 * - (Pre-2021.2/v12) Automatic switching to using _AfterPostProcessTexture for After Rendering event, in order to correctly handle the blit after post processing is applied
 * - Setting a _InverseView matrix (cameraToWorldMatrix), for shaders that might need it to handle calculations from screen space to world.
 * 		e.g. Reconstruct world pos from depth : https://www.cyanilux.com/tutorials/depth/#blit-perspective 
 * - (2020.2/v10 +) Enabling generation of DepthNormals (_CameraNormalsTexture)
 * 		This will only include shaders who have a DepthNormals pass (mostly Lit Shaders / Graphs)
 		(workaround for Unlit Shaders / Graphs: https://gist.github.com/Cyanilux/be5a796cf6ddb20f20a586b94be93f2b)
 * ------------------------------------------------------------------------------------------------------------------------
 * @Cyanilux
*/

namespace Cyan
{
	public partial class BlitFeature
	{
		[System.Serializable]
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
}