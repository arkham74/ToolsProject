using UnityEngine;

namespace JD
{
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

		public static bool Approximately(this Quaternion quatA, Quaternion value, float acceptableRange)
		{
			return 1 - Mathf.Abs(Quaternion.Dot(quatA, value)) < acceptableRange;
		}
	}
}