using System;
using System.Collections;
using System.Collections.Generic;
using Freya;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.Rendering;

namespace JD.PathTrace
{
	[ExecuteAlways]
	public class PathTraceSphere : MonoBehaviour
	{
		[SerializeField] private PathTraceMaterial material;

		private void OnDrawGizmos()
		{
			if (VolumeManager.instance.stack.GetComponent<PathTraceVolumeComponent>().scene.value) return;
			Gizmos.color = Color.red;
			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.DrawWireSphere(Vector3.zero, 1);
		}

		private void OnEnable()
		{
			PathTrace.Spheres.Add(this);
		}

		private void OnDisable()
		{
			PathTrace.Spheres.Remove(this);
		}

		public Sphere ToSphere()
		{
			Sphere sphere = new Sphere()
			{
				center = transform.position,
				radius = transform.lossyScale.Max(),
				material = material,
			};
			return sphere;
		}
	}
}

