#if OUTLINE_URP
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace JD.PlanarReflection
{
	[Serializable]
	public struct PlanarReflectionSettings
	{
		public LayerMask layer;
		public RenderPassEvent renderPassEvent;
		[RenderingLayer] public uint renderingLayer;
		public bool disableSSAO;
		public bool renderSkybox;
		public bool useMips;
		public bool sceneView;
	}
}
#endif