using System.Runtime.CompilerServices;
using UnityEngine;

namespace JD
{
	public static class QuaternionExtensions
	{
		// [MethodImpl(MethodImplOptions.AggressiveInlining)]
		// public static Quaternion Inverse(this Quaternion q)
		// {
		// 	return Quaternion.Inverse(q);
		// }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion RotateToDirection(this Quaternion quaternion, Vector3 direction, float time)
		{
			return Quaternion.Lerp(quaternion, Quaternion.LookRotation(direction), time);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Approximately(this Quaternion quatA, Quaternion value, float acceptableRange)
		{
			return 1 - Mathf.Abs(Quaternion.Dot(quatA, value)) < acceptableRange;
		}
	}
}