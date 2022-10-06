using System;

namespace JD
{
	[Flags]
	public enum HexDirection
	{
		Right = 1,
		DownRight = 2,
		DownLeft = 4,
		Left = 8,
		UpLeft = 16,
		UpRight = 32,
		All = Right | DownRight | DownLeft | Left | UpLeft | UpRight,
	}
}