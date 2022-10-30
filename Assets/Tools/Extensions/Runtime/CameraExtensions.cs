using UnityEngine;

public static class CameraExtensions
{
	private static Plane[] planes = new Plane[6];

	public static bool See(this Camera cam, MeshRenderer renderer)
	{
		GeometryUtility.CalculateFrustumPlanes(cam, planes);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}
}
