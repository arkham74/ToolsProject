#if OUTLINE_URP
using System;
using UnityEngine;

namespace JD.PlanarReflection
{
	[Serializable]
	public class PlanarReflectionSettings
	{
		public LayerMask layer = -1;
		[RenderingLayer] public uint renderingLayer = uint.MaxValue;
		public Vector3 position = Vector3.zero;
		public Quaternion rotation = Quaternion.identity;
		public bool disableSSAO = true;
		[HideInInspector] public Material screenPassMaterial;

		public Vector3 normal => rotation * Vector3.up;
	}
}
#endif