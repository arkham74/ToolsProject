using System.Runtime.CompilerServices;
using UnityEngine;

namespace JD
{
	public static class VectorMath
	{
		const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

		[MethodImpl(INLINE)]
		public static float InverseLerp(Vector2 a, Vector2 b, Vector2 v)
		{
			return Vector2.Dot(v - a, b - a) / Vector2.Dot(b - a, b - a);
		}

		[MethodImpl(INLINE)]
		public static float InverseLerp(Vector3 a, Vector3 b, Vector3 v)
		{
			return Vector3.Dot(v - a, b - a) / Vector3.Dot(b - a, b - a);
		}

		[MethodImpl(INLINE)]
		public static float InverseLerp(Vector4 a, Vector4 b, Vector4 v)
		{
			return Vector4.Dot(v - a, b - a) / Vector4.Dot(b - a, b - a);
		}
	}
}