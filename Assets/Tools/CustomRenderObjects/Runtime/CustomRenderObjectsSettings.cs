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

		public bool sceneView = true;
		public bool clearDepth;
		public RenderPassEvent passEvent = RenderPassEvent.AfterRenderingOpaques;
		public ScriptableRenderPassInput passInput = ScriptableRenderPassInput.Color | ScriptableRenderPassInput.Depth;
		public CompareFunction depthCompareFunction = CompareFunction.LessEqual;
		public RenderQueueType renderQueueType = RenderQueueType.Opaque;
		public SortingCriteria sortingCriteria = SortingCriteria.CommonOpaque;
		public RenderStateMask renderStateMask = RenderStateMask.Nothing;
		public LayerMask layerMask = -1;
		[RenderingLayer] public uint renderLayerMask = uint.MaxValue;

		public string target = string.Empty;

		public bool depthWrite = true;
		public Material overrideMaterial;
		public int overrideMaterialPassIndex;
		public int cameraFieldOfView = 0;
	}
}
#endif