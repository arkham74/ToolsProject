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

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD.Splines
{
	[RequireComponent(typeof(SplineContainer))]
	public abstract class SplineSampler : MonoBehaviour
	{
		[BurstCompile]
		private struct SampleJob : IJobParallelFor
		{
			[ReadOnly] public NativeSpline spline;
			[WriteOnly] public NativeArray<Vector3> positions;

			public SampleJob(NativeSpline spline, NativeArray<Vector3> positions) : this()
			{
				this.spline = spline;
				this.positions = positions;
			}

			public void Execute(int index)
			{
				positions[index] = spline.EvaluatePosition((float)index / ((float)positions.Length - 1f));
			}
		}

		[SerializeField] private SplineContainer splineContainer;
		[SerializeField][Range(1, 100)] protected int samplesPerUnit = 10;
		protected bool dirty;
		private int oldSamples;

		protected abstract void PositionsAndNormals(NativeArray<Vector3> positions, Spline spline);

		protected virtual void Reset()
		{
			splineContainer = GetComponentInChildren<SplineContainer>(true);
		}

		private void OnEnable()
		{
			Spline.Changed += OnChanged;
		}

		private void OnDisable()
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
				NativeArray<Vector3> positions = new NativeArray<Vector3>(samples, Allocator.Persistent);
				NativeSpline nativeSpline = new NativeSpline(spline, Allocator.Persistent);
				SampleJob job = new SampleJob(nativeSpline, positions);
				JobHandle handle = job.Schedule(samples, 1);
				handle.Complete();
				PositionsAndNormals(positions, spline);
				positions.Dispose();
				nativeSpline.Dispose();
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