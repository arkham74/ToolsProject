using JD;
using UnityEngine;

public static class PlanarReflectionUtils
{
	public static Matrix4x4 CalculateReflectionMatrix(Vector3 planePosition, Vector3 planeNormal)
	{
		Matrix4x4 reflectionMatrix = default;
		planeNormal.Normalize();

		planePosition = planePosition.SetY(-planePosition.y);

		float v = Vector3.Dot(planePosition, planeNormal);

		float x = planeNormal.x;
		float y = planeNormal.y;
		float z = planeNormal.z;

		reflectionMatrix.m00 = (1f - 2f * x * x);
		reflectionMatrix.m01 = (-2f * x * y);
		reflectionMatrix.m02 = (-2f * x * z);
		reflectionMatrix.m03 = (-2f * v * x);

		reflectionMatrix.m10 = (-2f * y * x);
		reflectionMatrix.m11 = (1f - 2f * y * y);
		reflectionMatrix.m12 = (-2f * y * z);
		reflectionMatrix.m13 = (-2f * v * y);

		reflectionMatrix.m20 = (-2f * z * x);
		reflectionMatrix.m21 = (-2f * z * y);
		reflectionMatrix.m22 = (1f - 2f * z * z);
		reflectionMatrix.m23 = (-2f * v * z);

		reflectionMatrix.m30 = 0f;
		reflectionMatrix.m31 = 0f;
		reflectionMatrix.m32 = 0f;
		reflectionMatrix.m33 = 1f;

		return reflectionMatrix;
	}
}