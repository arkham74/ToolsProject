using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD
{
	public static class CameraExtensions
	{
		private static Plane[] planes = new Plane[6];

		public static bool See(this Camera cam, MeshRenderer renderer)
		{
			GeometryUtility.CalculateFrustumPlanes(cam, planes);
			return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
		}

		public static Vector2 GetMouseScreenPosition()
		{
#if ENABLE_INPUT_SYSTEM
			return Mouse.current.position.ReadValue();
#else
			return Input.mousePosition;
#endif
		}

		public static Vector3 MouseToViewportPoint(this Camera camera)
		{
			return camera.ScreenToViewportPoint(GetMouseScreenPosition());
		}

		public static Ray MouseToRay(this Camera camera)
		{
			return camera.ScreenPointToRay(GetMouseScreenPosition());
		}

		public static Vector3 MouseToWorldPoint(this Camera camera)
		{
			return camera.ScreenToWorldPoint(GetMouseScreenPosition());
		}

		public static bool ScreenToWorldPointOnPlane(this Camera camera, out Vector3 point)
		{
			Vector3 mousePosition = GetMouseScreenPosition();
			Plane plane = new Plane(Vector3.up, Vector3.zero);
			return camera.ScreenToWorldPointOnPlane(mousePosition, plane, out point);
		}

		public static bool ScreenToWorldPointOnPlane(this Camera camera, Vector3 screenPoint, Plane plane, out Vector3 point)
		{
			Ray ray = camera.ScreenPointToRay(screenPoint);
			bool hit = plane.Raycast(ray, out float distance);
			point = ray.GetPoint(distance);
			return hit;
		}
	}
}