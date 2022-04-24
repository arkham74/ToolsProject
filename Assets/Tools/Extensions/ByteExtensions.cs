using System;

public static class ByteExtensions
{
	public static bool[] ToBoolArray(this byte b)
	{
		bool[] result = new bool[8];
		for (int i = 0; i < 8; i++)
		{
			result[i] = (b & (1 << i)) != 0;
		}

		Array.Reverse(result);
		return result;
	}
}
