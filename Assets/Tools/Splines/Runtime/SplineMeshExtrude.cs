#if TOOLS_SPLINES
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using TMPro;
using JD;
using Freya;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tools = JD.Tools;
using Unity.Collections;
using UnityEngine.Splines;
using Steamworks;
using Unity.Mathematics;
using UnityEngine.Pool;

namespace JD.Splines
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	[ExecuteAlways]
	public class SplineMeshExtrude : SplineSampler
	{
		[SerializeField] private bool simplify = false;
		[SerializeField][Range(0.001f, 0.01f)] private float tolerance = 0.01f;
		[SerializeField][Range(0.01f, 0.1f)] private float thickness = 0.1f;
		[SerializeField] private MeshTopology topology = MeshTopology.Triangles;
		[SerializeField] private MeshFilter meshFilter;
		[SerializeField] private MeshRenderer meshRenderer;

		private Mesh mesh;

		private void OnValidate()
		{
			dirty = true;
		}

		protected override void Reset()
		{
			base.Reset();
			meshFilter = GetComponentInChildren<MeshFilter>(true);
			meshRenderer = GetComponentInChildren<MeshRenderer>(true);
		}

		protected override void EvaluatePositionTangentNormal(NativeArray<float3> positions, NativeArray<float3> tangents, NativeArray<float3> normals)
		{
			CreateMesh();
			mesh.Clear();

			List<Vector3> verts = new List<Vector3>();
			Vector3 offset = Vector3.up * thickness;

			List<float3> list = ListPool<float3>.Get();
			list.AddRange(positions);

			if (simplify)
			{
				SplineUtility.ReducePoints(list, tolerance);
			}

			foreach (Vector3 item in list)
			{
				verts.Add(item);
				verts.Add(item + offset);
			}
			mesh.SetVertices(verts);

			List<int> indices = new List<int>();

			for (int i = 0; i < (list.Count - 1) * 2; i++)
			{
				if (i % 2 == 0)
				{
					indices.Add(i + 2);
					indices.Add(i + 1);
					indices.Add(i + 0);
				}
				else
				{
					indices.Add(i + 0);
					indices.Add(i + 1);
					indices.Add(i + 2);
				}
			}

			mesh.SetIndices(indices, topology, 0);
			mesh.Optimize();
			mesh.OptimizeIndexBuffers();
			mesh.OptimizeReorderVertexBuffer();
			mesh.RecalculateNormals();
			ListPool<float3>.Release(list);
		}

		private void CreateMesh()
		{
			if (mesh == null)
			{
				mesh = new Mesh();
				mesh.name = "SplineExtrudeMesh";
				meshFilter.sharedMesh = mesh;
			}
		}
	}
}
#endif