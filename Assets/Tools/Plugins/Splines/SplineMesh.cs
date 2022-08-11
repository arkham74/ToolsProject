using System;
using SebastianLague;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace JD.Splines
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public class SplineMesh : SplineSampler
	{
		[SerializeField][Min(3)] private int resolution = 10;
		[SerializeField][Min(0.01f)] private float thickness = 0.05f;
		[SerializeField] private MeshFilter meshFilter;
		[SerializeField] private MeshRenderer meshRenderer;
		[SerializeField][GradientUsage(true)] private Gradient gradient;
		private Mesh mesh;

		protected override void Reset()
		{
			base.Reset();
			meshFilter = GetComponent<MeshFilter>();
			meshRenderer = GetComponent<MeshRenderer>();
			meshRenderer.sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
		}

		protected override void PositionsAndNormals(Span<Vector3> positions, Span<Vector3> normals)
		{
			if (mesh == null)
			{
				mesh = new Mesh
				{
					name = "Cylinder Mesh"
				};
				meshFilter.sharedMesh = mesh;
			}

			for (int i = 0; i < positions.Length; i++)
			{
				positions[i].y += thickness;
			}

			CylinderGenerator.CreateMesh(ref mesh, positions, gradient, resolution, thickness);
		}
	}
}