using System.Collections.Generic;
using UnityEngine;

namespace JD
{
	public struct DistanceComparer : IComparer<RaycastHit>
	{
		public int Compare(RaycastHit x, RaycastHit y)
		{
			return x.distance.CompareTo(y.distance);
		}
	}
}
