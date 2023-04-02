using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JD.PathTrace
{
	public class PathTraceSphere : MonoBehaviour
	{
		private void OnDrawGizmos()
		{
			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.DrawWireSphere(Vector3.zero, 1);
		}
	}
}

