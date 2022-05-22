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

namespace CordBot
{
	public class SplineDebuger : MonoBehaviour
	{
		[SerializeField] private Spline spline;
		[SerializeField] private float t = 0.5f;

		private void OnDrawGizmosSelected()
		{
			Gizmos.matrix = transform.localToWorldMatrix;
			Vector3 pos = spline.Evaluate(t);
			Gizmos.DrawWireSphere(pos, 0.125f);
		}
	}
}