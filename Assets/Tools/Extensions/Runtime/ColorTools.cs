using UnityEngine;
using System.Runtime.CompilerServices;

#if ENABLE_INPUT_SYSTEM
#endif

namespace SAR
{
	public static class ColorTools
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceRGB(Color from, Color to) => Vector3.Distance((Vector4)from, (Vector4)to);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceRGBA(Color from, Color to) => Vector4.Distance((Vector4)from, (Vector4)to);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceRGB(Color32 from, Color32 to) => Vector3.Distance((Vector4)(Color)from, (Vector4)(Color)to);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceRGBA(Color32 from, Color32 to) => Vector4.Distance((Color)from, (Color)to);
	}
}