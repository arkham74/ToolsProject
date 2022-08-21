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

namespace JD.Splines
{
	public class SplineDebuger : MonoBehaviour
	{
		[SerializeField] private Spline spline;
		[SerializeField][Range(0f, 1f)] private float t = 0.5f;

		private void Reset()
		{
			spline = GetComponent<Spline>();
		}

		private void OnDrawGizmosSelected()
		{
			if (spline)
			{
				Gizmos.matrix = transform.localToWorldMatrix;
				Vector3 pos = spline.Evaluate(t);
				Gizmos.DrawWireSphere(pos, 0.125f);
			}
		}
	}
}