using System;
using JD;
using JD.Splines;
using UnityEngine;

namespace JD.Splines
{
	public class SplineTravel : MonoBehaviour
	{
		[SerializeField] private Spline spline;
		[SerializeField] private Transform target;
		[SerializeField] private float speed = 2f;
		private float time;

		private void Reset()
		{
			spline = GetComponentInParent<Spline>();
			target = GetComponent<Transform>();
		}

		private void Update()
		{
			if (spline)
			{
				time += Time.deltaTime * speed;
				Vector3 pos = spline.EvaluateByDistance(time);
				pos = spline.transform.LocalToWorld(pos);
				target.position = pos;
			}
		}
	}
}