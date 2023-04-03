using System;
using UnityEngine;

namespace JD.PathTrace
{
	[Serializable]
	public struct PathTraceSettings
	{
		public Material material;
	}

	[Serializable]
	public struct PathTraceMaterial
	{
		public Color color;
	};

	public struct Sphere
	{
		public Vector3 center;
		public float radius;
		public PathTraceMaterial material;
	};
}