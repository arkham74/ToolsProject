using System;
using UnityEngine;

namespace JD.PlanarReflection
{
	[Serializable]
	public class PlanarReflectionSettings
	{
		public Vector3 planePosition = Vector3.zero;
		public Quaternion planeRotation = Quaternion.identity;

		public Vector3 planeNormal => planeRotation * Vector3.up;
	}
}