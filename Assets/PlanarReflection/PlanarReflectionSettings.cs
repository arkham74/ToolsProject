using System;
using UnityEngine;

namespace JD.PlanarReflection
{
	[Serializable]
	public class PlanarReflectionSettings
	{
		[RenderingLayer] public uint renderingLayerMask = 1;
		public float offset;
		public Vector3 position = Vector3.zero;
		public Quaternion rotation = Quaternion.identity;

		public Vector3 normal => rotation * Vector3.up;
	}
}