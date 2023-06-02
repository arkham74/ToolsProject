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
using UnityEngine.Splines;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;
using Unity.Mathematics;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD.Splines
{
	[RequireComponent(typeof(SplineContainer))]
	public abstract class SplineSampler : MonoBehaviour
	{
		[SerializeField] private SplineContainer splineContainer;
		[SerializeField][Range(1, 100)] protected int samplesPerUnit = 10;
		protected bool dirty;
		private int oldSamples;

		protected abstract void EvaluatePositionTangentNormal(NativeArray<float3> positions, NativeArray<float3> tangents, NativeArray<float3> normals);

		protected virtual void Reset()
		{
			splineContainer = GetComponentInChildren<SplineContainer>(true);
		}

		protected virtual void OnEnable()
		{
			Spline.Changed += OnChanged;
		}

		protected virtual void OnDisable()
		{
			Spline.Changed -= OnChanged;
		}

		private void OnChanged(Spline spline, int index, SplineModification mod)
		{
			if (splineContainer.Spline == spline)
			{
				dirty = true;
			}
		}

		private void Update()
		{
			CheckSamplesChange();
			SampleCurve();
		}

		private void SampleCurve()
		{
			if (dirty)
			{
				Spline spline = splineContainer.Spline;
				int samples = Mathf.CeilToInt(samplesPerUnit * spline.GetLength());
				NativeArray<float3> positions = new NativeArray<float3>(samples, Allocator.TempJob);
				NativeArray<float3> tangents = new NativeArray<float3>(samples, Allocator.TempJob);
				NativeArray<float3> normals = new NativeArray<float3>(samples, Allocator.TempJob);
				SplineJobs.EvaluatePositionTangentNormal(spline, positions, tangents, normals);
				EvaluatePositionTangentNormal(positions, tangents, normals);
				positions.Dispose();
				tangents.Dispose();
				normals.Dispose();
				dirty = false;
			}
		}

		private void CheckSamplesChange()
		{
			if (oldSamples != samplesPerUnit)
			{
				oldSamples = samplesPerUnit;
				dirty = true;
			}
		}
	}
}
#endif