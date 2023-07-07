using UnityEngine;

public class MirrorTest : MonoBehaviour
{
	[SerializeField] private Camera cam;
	[SerializeField] private Transform plane;

	private void OnDrawGizmos()
	{
		Matrix4x4 viewMatrix = cam.cameraToWorldMatrix;

		Vector3 position = viewMatrix.GetPosition();
		Vector3 forward = -viewMatrix.GetColumn(2);

		Gizmos.DrawWireSphere(position, 0.2f);
		Gizmos.DrawRay(position, forward);

		Matrix4x4 mirrorMatrix = PlanarReflectionUtils.GetMirrorMatrix(plane.position, plane.rotation);
		viewMatrix = mirrorMatrix * viewMatrix;

		position = viewMatrix.GetPosition();
		forward = -viewMatrix.GetColumn(2);

		Gizmos.color = Color.red;

		Gizmos.DrawWireSphere(position, 0.2f);
		Gizmos.DrawRay(position, forward);
	}
}