using JD;
using UnityEngine;

public static class PlanarReflectionUtils
{
	private readonly static Vector3 normalDirection = Vector3.up;

	public static Vector4 GetMirrorPlane(Vector3 position, Quaternion rotation, Matrix4x4 viewMatrix, float offset = 0.001f)
	{
		Vector3 normal = rotation * normalDirection;
		var pos = position - normalDirection * 0.1f;
		var offsetPos = pos + normal * offset;
		var cpos = viewMatrix.MultiplyPoint(offsetPos);
		var cnormal = viewMatrix.MultiplyVector(normal).normalized;
		return new Vector4(cnormal.x, cnormal.y, cnormal.z, -Vector3.Dot(cpos, cnormal));
	}

	public static Matrix4x4 GetMirrorMatrix(Vector3 position, Quaternion rotation, float offset = 0.001f)
	{
		Vector3 normal = rotation * normalDirection;

		// Setup
		var depth = -Vector3.Dot(normal, position) - offset;

		// Create matrix
		var mirrorMatrix = new Matrix4x4()
		{
			m00 = (1f - 2f * normal.x * normal.x),
			m01 = (-2f * normal.x * normal.y),
			m02 = (-2f * normal.x * normal.z),
			m03 = (-2f * depth * normal.x),
			m10 = (-2f * normal.y * normal.x),
			m11 = (1f - 2f * normal.y * normal.y),
			m12 = (-2f * normal.y * normal.z),
			m13 = (-2f * depth * normal.y),
			m20 = (-2f * normal.z * normal.x),
			m21 = (-2f * normal.z * normal.y),
			m22 = (1f - 2f * normal.z * normal.z),
			m23 = (-2f * depth * normal.z),
			m30 = 0f,
			m31 = 0f,
			m32 = 0f,
			m33 = 1f,
		};
		return mirrorMatrix;
	}
}