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
using Random = UnityEngine.Random;
using UnityEngine.Profiling;
using UnityEngine.Splines;
using Unity.Collections;

namespace JD.Splines
{
	[RequireComponent(typeof(LineRenderer))]
	[ExecuteAlways]
	public class SplineLineRenderer : SplineSampler
	{
		[SerializeField] private LineRenderer lineRenderer;

		protected override void Reset()
		{
			base.Reset();
			lineRenderer = GetComponent<LineRenderer>();
			lineRenderer.useWorldSpace = false;
			lineRenderer.startWidth = 0.1f;
			lineRenderer.endWidth = 0.1f;
			lineRenderer.sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Particles/Unlit"));
		}

		protected override void PositionsAndNormals(NativeArray<Vector3> positions, Spline spline)
		{
			int length = positions.Length;
			lineRenderer.loop = spline.Closed;
			lineRenderer.positionCount = length;
			for (int i = 0; i < length; i++)
			{
				lineRenderer.SetPosition(i, positions[i]);
			}
		}
	}
}
#endif