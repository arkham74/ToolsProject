using JD;
using UnityEngine;

namespace JD.PlanarReflection
{
	public static class PlanarReflectionUtils
	{
		public static Vector4 MirrorPosition(Vector4 position, Vector4 planePosition, Vector4 planeNormal)
		{
			float distance = Vector4.Dot(planePosition - position, planeNormal);
			Vector4 mirroredPosition = position + 2f * distance * planeNormal;
			mirroredPosition.w = 1;
			return mirroredPosition;
		}

		public static Vector4 MirrorDirection(Vector4 direction, Vector4 planeNormal)
		{
			Vector4 mirroredDirection = direction - 2f * Vector4.Dot(direction, planeNormal) * planeNormal;
			mirroredDirection.w = 0;
			return mirroredDirection;
		}
	}
}