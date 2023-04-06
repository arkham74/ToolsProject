using System;
using UnityEngine;

namespace JD.PathTrace
{
	public struct Sphere
	{
		public Vector3 center;
		public float radius;
		public PathTraceMaterial material;
	};

	[Serializable]
	public struct PathTraceMaterial
	{
		[ColorUsage(false, true)] public Color color;
		[ColorUsage(false, true)] public Color emission;
	};
}