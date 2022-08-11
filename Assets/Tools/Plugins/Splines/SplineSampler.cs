using System;
using UnityEngine;

namespace JD.Splines
{
	[ExecuteAlways]
	[RequireComponent(typeof(Spline))]
	public abstract class SplineSampler : MonoBehaviour
	{
		[SerializeField] private bool updateInPlayMode;
		[SerializeField][Min(2)] protected int samples = 30;
		[SerializeField] protected Spline spline;

		protected virtual void Reset()
		{
			spline = GetComponent<Spline>();
		}

		protected abstract void PositionsAndNormals(Span<Vector3> positions, Span<Vector3> normals);

		private void LateUpdate()
		{
			if (Application.isPlaying && !updateInPlayMode) return;

			Span<Vector3> positions = stackalloc Vector3[samples];
			Span<Vector3> normals = stackalloc Vector3[samples];
			for (int i = 0; i < samples; i++)
			{
				float t = (float)i / (samples - 1);
				positions[i] = spline.Evaluate(t);
				normals[i] = spline.EvaluateNormal(t);
			}
			PositionsAndNormals(positions, normals);
		}
	}
}