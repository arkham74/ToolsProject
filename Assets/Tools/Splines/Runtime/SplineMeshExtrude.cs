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
		[SerializeField] private MeshTopology topology = MeshTopology.LineStrip;
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

		protected override void PositionsAndNormals(NativeArray<Vector3> native, Spline spline)
		{
			Debug.Log("PositionsAndNormals");
			CreateMesh();
			mesh.Clear();

			List<Vector3> verts = new List<Vector3>();
			Vector3 offset = Vector3.up * thickness;

			List<Vector3> asd = new List<Vector3>(native);
			List<Vector3> positions = new List<Vector3>();

			if (simplify)
			{
				LineUtility.Simplify(asd, tolerance, positions);
			}
			else
			{
				positions.AddRange(asd);
			}

			foreach (Vector3 item in positions)
			{
				verts.Add(item);
				verts.Add(item + offset);
			}
			mesh.SetVertices(verts);

			List<int> indices = new List<int>();

			for (int i = 0; i < (positions.Count - 1) * 2; i++)
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