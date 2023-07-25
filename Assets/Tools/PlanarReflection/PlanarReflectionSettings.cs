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
		[RenderingLayer] public uint renderingLayer;
		public bool disableSSAO;
	}
}
#endif