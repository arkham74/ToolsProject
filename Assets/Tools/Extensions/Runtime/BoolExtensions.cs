using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JD
{
	public static class BoolExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ToInt(this bool value)
		{
			return Convert.ToInt32(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static byte ToByte(this bool[] source)
		{
			byte result = 0;
			int index = 8 - source.Length;
			foreach (bool b in source)
			{
				if (b)
				{
					result |= (byte)(1 << (7 - index));
				}

				index++;
			}

			return result;
		}
	}
}