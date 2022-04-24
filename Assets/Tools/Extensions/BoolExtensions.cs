using System;
using System.Collections.Generic;
using System.Linq;

public static class BoolExtensions
{
	public static int ToInt(this bool value)
	{
		return Convert.ToInt32(value);
	}

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
