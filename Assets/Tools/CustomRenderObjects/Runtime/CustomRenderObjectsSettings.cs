#if TOOLS_URP
using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

namespace JD.CustomRenderObjects
{
	[Serializable]
	public class CustomRenderObjectsSettings
	{
		[NonSerialized] public string name;

		public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
		public CompareFunction depthCompareFunction = CompareFunction.LessEqual;
		public RenderQueueType renderQueueType = RenderQueueType.Opaque;
		public SortingCriteria sortingCriteria = SortingCriteria.CommonOpaque;
		public LayerMask layerMask = -1;
		[RenderingLayer] public uint renderLayerMask = uint.MaxValue;

		public string target = "_CameraColorAttachmentA";

		public bool depthWrite = true;
		public Material overrideMaterial;
		public int overrideMaterialPassIndex;
		public int cameraFieldOfView = 0;
	}
}
#endif