using System;

namespace JD
{
	[Flags]
	public enum HexDirection
	{
		Right = 1,
		East = 1,
		DownRight = 2,
		SouthEast = 2,
		DownLeft = 4,
		SouthWest = 4,
		Left = 8,
		West = 8,
		UpLeft = 16,
		NorthWest = 16,
		UpRight = 32,
		NorthEast = 32,
		All = Right | DownRight | DownLeft | Left | UpLeft | UpRight,
	}
}