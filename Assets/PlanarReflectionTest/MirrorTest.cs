using UnityEngine;

namespace JD.PlanarReflection.Test
{
	public class MirrorTest : MonoBehaviour
	{
		[SerializeField] private Camera cam;
		[SerializeField] private Transform plane;

		private void OnDrawGizmos()
		{
			Vector3 position = cam.transform.position;
			Vector3 right = cam.transform.right;
			Vector3 up = cam.transform.up;
			Vector3 forward = cam.transform.forward;

			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere(position, 0.2f);

			Gizmos.color = Color.red;
			Gizmos.DrawRay(position, right);

			Gizmos.color = Color.green;
			Gizmos.DrawRay(position, up);

			Gizmos.color = Color.blue;
			Gizmos.DrawRay(position, forward);

			position = PlanarReflectionUtils.MirrorPosition(position, plane.position, plane.up);
			up = PlanarReflectionUtils.MirrorDirection(up, plane.up);
			forward = PlanarReflectionUtils.MirrorDirection(forward, plane.up);
			right = Vector3.Cross(forward, up);

			Gizmos.color = Color.magenta;
			Gizmos.DrawWireSphere(position, 0.2f);

			Gizmos.color = Color.red;
			Gizmos.DrawRay(position, right);

			Gizmos.color = Color.green;
			Gizmos.DrawRay(position, up);

			Gizmos.color = Color.blue;
			Gizmos.DrawRay(position, forward);
		}
	}
}