using UnityEngine;

public static class QuaternionExtensions
{
	public static Quaternion Inverse(this Quaternion q)
	{
		return Quaternion.Inverse(q);
	}

	public static Quaternion RotateToDirection(this Quaternion quaternion, Vector3 direction, float time)
	{
		return Quaternion.Lerp(quaternion, Quaternion.LookRotation(direction), time);
	}
}
