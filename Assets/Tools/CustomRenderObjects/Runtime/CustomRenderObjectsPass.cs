#if TOOLS_URP
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;

namespace JD.CustomRenderObjects
{
	public class CustomRenderObjectsPass : ScriptableRenderPass
	{
		private readonly RenderQueueType renderQueueType;
		private FilteringSettings mFilteringSettings;
		private readonly CustomRenderObjects.CustomCameraSettings mCameraSettings;
		private readonly ProfilingSampler mProfilingSampler;

		public Material OverrideMaterial { get; set; }
		public int OverrideMaterialPassIndex { get; set; }

		private readonly List<ShaderTagId> mShaderTagIdList = new List<ShaderTagId>();

		public void SetDetphState(bool writeEnabled, CompareFunction function = CompareFunction.Less)
		{
			mRenderStateBlock.mask |= RenderStateMask.Depth;
			mRenderStateBlock.depthState = new DepthState(writeEnabled, function);
		}

		public void SetStencilState(int reference, CompareFunction compareFunction, StencilOp passOp, StencilOp failOp, StencilOp zFailOp)
		{
			StencilState stencilState = StencilState.defaultValue;
			stencilState.enabled = true;
			stencilState.SetCompareFunction(compareFunction);
			stencilState.SetPassOperation(passOp);
			stencilState.SetFailOperation(failOp);
			stencilState.SetZFailOperation(zFailOp);

			mRenderStateBlock.mask |= RenderStateMask.Stencil;
			mRenderStateBlock.stencilReference = reference;
			mRenderStateBlock.stencilState = stencilState;
		}

		private RenderStateBlock mRenderStateBlock;

		public CustomRenderObjectsPass(string profilerTag, RenderPassEvent renderPassEvent, string[] shaderTags, RenderQueueType renderQueueType, int layerMask, uint renderLayerMask, CustomRenderObjects.CustomCameraSettings cameraSettings)
		{
			profilingSampler = new ProfilingSampler(nameof(CustomRenderObjectsPass));

			mProfilingSampler = new ProfilingSampler(profilerTag);
			this.renderPassEvent = renderPassEvent;
			this.renderQueueType = renderQueueType;
			OverrideMaterial = null;
			OverrideMaterialPassIndex = 0;
			RenderQueueRange renderQueueRange = (renderQueueType == RenderQueueType.Transparent)
				? RenderQueueRange.transparent
				: RenderQueueRange.opaque;
			mFilteringSettings = new FilteringSettings(renderQueueRange, layerMask, renderLayerMask);

			if (shaderTags is { Length: > 0 })
			{
				foreach (string passName in shaderTags)
					mShaderTagIdList.Add(new ShaderTagId(passName));
			}
			else
			{
				mShaderTagIdList.Add(new ShaderTagId("SRPDefaultUnlit"));
				mShaderTagIdList.Add(new ShaderTagId("UniversalForward"));
				mShaderTagIdList.Add(new ShaderTagId("UniversalForwardOnly"));
			}

			mRenderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
			mCameraSettings = cameraSettings;
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			SortingCriteria sortingCriteria = (renderQueueType == RenderQueueType.Transparent) ? SortingCriteria.CommonTransparent : renderingData.cameraData.defaultOpaqueSortFlags;

			DrawingSettings drawingSettings = CreateDrawingSettings(mShaderTagIdList, ref renderingData, sortingCriteria);
			drawingSettings.overrideMaterial = OverrideMaterial;
			drawingSettings.overrideMaterialPassIndex = OverrideMaterialPassIndex;

			ref CameraData cameraData = ref renderingData.cameraData;
			Camera camera = cameraData.camera;

			// In case of camera stacking we need to take the viewport rect from base camera
			Rect pixelRect = renderingData.cameraData.camera.pixelRect;
			float cameraAspect = pixelRect.width / pixelRect.height;

			// NOTE: Do NOT mix ProfilingScope with named CommandBuffers i.e. CommandBufferPool.Get("name").
			// Currently there's an issue which results in mismatched markers.
			CommandBuffer cmd = CommandBufferPool.Get();
			using (new ProfilingScope(cmd, mProfilingSampler))
			{
				if (mCameraSettings.overrideCamera)
				{
					Matrix4x4 projectionMatrix = Matrix4x4.Perspective(mCameraSettings.cameraFieldOfView, cameraAspect,
						camera.nearClipPlane, camera.farClipPlane);
					projectionMatrix = GL.GetGPUProjectionMatrix(projectionMatrix, cameraData.IsCameraProjectionMatrixFlipped());

					Matrix4x4 viewMatrix = cameraData.GetViewMatrix();
					Vector4 cameraTranslation = viewMatrix.GetColumn(3);
					viewMatrix.SetColumn(3, cameraTranslation + mCameraSettings.offset);

					RenderingUtils.SetViewAndProjectionMatrices(cmd, viewMatrix, projectionMatrix, false);
				}

				// Ensure we flush our command-buffer before we render...
				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();

				// Render the objects...
				context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref mFilteringSettings, ref mRenderStateBlock);

				if (mCameraSettings.overrideCamera && mCameraSettings.restoreCamera)
				{
					RenderingUtils.SetViewAndProjectionMatrices(cmd, cameraData.GetViewMatrix(), cameraData.GetGPUProjectionMatrix(), false);
				}
			}

			context.ExecuteCommandBuffer(cmd);
			CommandBufferPool.Release(cmd);
		}
	}
}
#endif