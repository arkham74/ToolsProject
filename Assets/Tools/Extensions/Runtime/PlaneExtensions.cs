using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD
{
	public static class PlaneExtensions
	{
		public static bool Raycast(this Plane plane, Ray ray, out RaycastHit hit)
		{
			hit = default;

			if (plane.Raycast(ray, out float distance))
			{
				hit.distance = distance;
				hit.point = ray.GetPoint(distance);
				hit.normal = plane.normal;
				return true;
			}

			return false;
		}
	}
}