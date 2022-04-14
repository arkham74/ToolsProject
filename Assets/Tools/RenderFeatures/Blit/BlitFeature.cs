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
	/*
	CreateAssetMenu here allows creating the ScriptableObject without being attached to a Renderer Asset
	Can then Enqueue the pass manually via https://gist.github.com/Cyanilux/8fb3353529887e4184159841b8cad208
	as a workaround for 2D Renderer not supporting features (prior to 2021.2). Uncomment if needed.
	*/
	//	[CreateAssetMenu(menuName = "Cyan/Blit")] 
	public partial class BlitFeature : ScriptableRendererFeature
	{
		public enum Target
		{
			CameraColor,
			TextureID,
			RenderTextureObject
		}

		public BlitSettings settings = new BlitSettings();
		public BlitPass blitPass;

		public override void Create()
		{
			var passIndex = settings.blitMaterial != null ? settings.blitMaterial.passCount - 1 : 1;
			settings.blitMaterialPassIndex = Mathf.Clamp(settings.blitMaterialPassIndex, -1, passIndex);
			blitPass = new BlitPass(settings.Event, settings, name);

#if !UNITY_2021_2_OR_NEWER
			if (settings.Event == RenderPassEvent.AfterRenderingPostProcessing)
			{
				Debug.LogWarning("Note that the \"After Rendering Post Processing\"'s Color target doesn't seem to work? (or might work, but doesn't contain the post processing) :( -- Use \"After Rendering\" instead!");
			}
#endif

			if (settings.graphicsFormat == UnityEngine.Experimental.Rendering.GraphicsFormat.None)
			{
				settings.graphicsFormat = SystemInfo.GetGraphicsFormat(UnityEngine.Experimental.Rendering.DefaultFormat.LDR);
			}
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{

			if (settings.blitMaterial == null)
			{
				Debug.LogWarningFormat("Missing Blit Material. {0} blit pass will not execute. Check for missing reference in the assigned renderer.", GetType().Name);
				return;
			}

#if !UNITY_2021_2_OR_NEWER
			// AfterRenderingPostProcessing event is fixed in 2021.2+ so this workaround is no longer required

			if (settings.Event == RenderPassEvent.AfterRenderingPostProcessing)
			{
			}
			else if (settings.Event == RenderPassEvent.AfterRendering && renderingData.postProcessingEnabled)
			{
				// If event is AfterRendering, and src/dst is using CameraColor, switch to _AfterPostProcessTexture instead.
				if (settings.srcType == Target.CameraColor)
				{
					settings.srcType = Target.TextureID;
					settings.srcTextureId = "_AfterPostProcessTexture";
				}
				if (settings.dstType == Target.CameraColor)
				{
					settings.dstType = Target.TextureID;
					settings.dstTextureId = "_AfterPostProcessTexture";
				}
			}
			else
			{
				// If src/dst is using _AfterPostProcessTexture, switch back to CameraColor
				if (settings.srcType == Target.TextureID && settings.srcTextureId == "_AfterPostProcessTexture")
				{
					settings.srcType = Target.CameraColor;
					settings.srcTextureId = "";
				}
				if (settings.dstType == Target.TextureID && settings.dstTextureId == "_AfterPostProcessTexture")
				{
					settings.dstType = Target.CameraColor;
					settings.dstTextureId = "";
				}
			}
#endif

			blitPass.Setup(renderer);
			renderer.EnqueuePass(blitPass);
		}
	}
}