#if TOOLS_URP
using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

namespace JD.CustomRenderObjects
{
	[Serializable]
	public class CustomRenderObjectsSettings
	{
		[NonSerialized] public string name;

		public bool sceneView = true;
		public RTClearFlags clearFlags = RTClearFlags.None;
		public LayerMask layerMask = -1;
		[RenderingLayer] public uint renderLayerMask = uint.MaxValue;

		public RenderPassEvent passEvent = RenderPassEvent.AfterRenderingOpaques;
		public ScriptableRenderPassInput passInput = ScriptableRenderPassInput.Color | ScriptableRenderPassInput.Depth;
		public RenderQueueType renderQueueType = RenderQueueType.Opaque;
		public RenderStateMask renderStateMask = RenderStateMask.Nothing;
		public CompareFunction depthCompareFunction = CompareFunction.LessEqual;
		public bool depthWrite = true;

		public string target = string.Empty;
		public GraphicsFormat graphicsFormat = GraphicsFormat.B10G11R11_UFloatPack32;

		public Material overrideMaterial;
		public int overrideMaterialPassIndex;
		public int cameraFieldOfView = 0;

		public bool overrideDepth => renderStateMask.HasFlag(RenderStateMask.Depth);
	}
}
#endif