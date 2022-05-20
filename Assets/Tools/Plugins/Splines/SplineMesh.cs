using System;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class SplineMesh : SplineRenderer
{
	[SerializeField][Min(3)] private int resolution = 10;
	[SerializeField][Min(0.01f)] private float thickness = 0.05f;
	[SerializeField] private MeshFilter meshFilter;
	[SerializeField] private MeshRenderer meshRenderer;
	private Mesh mesh;

	protected override void Reset()
	{
		base.Reset();
		meshFilter = GetComponent<MeshFilter>();
		meshRenderer = GetComponent<MeshRenderer>();
		meshRenderer.sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
	}

	protected override void Positions(Span<Vector3> positions)
	{
		if (mesh == null)
		{
			mesh = new Mesh();
			mesh.name = "Cylinder Mesh";
			meshFilter.sharedMesh = mesh;
		}
		CylinderGenerator.CreateMesh(ref mesh, positions, resolution, thickness);
	}
}
