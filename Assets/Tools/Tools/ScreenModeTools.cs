using UnityEngine;

namespace JD
{
	public static class ScreenModeTools
	{
		public enum CustomScreenMode
		{
			ExclusiveFullScreen,
			FullScreenWindow,
			Windowed
		}

		public static FullScreenMode ConvertScreenMode(CustomScreenMode mode) => mode switch
		{
			CustomScreenMode.ExclusiveFullScreen => FullScreenMode.ExclusiveFullScreen,
			CustomScreenMode.Windowed => FullScreenMode.Windowed,
			_ => FullScreenMode.FullScreenWindow,
		};

		public static CustomScreenMode ConvertScreenMode(FullScreenMode mode) => mode switch
		{
			FullScreenMode.ExclusiveFullScreen => CustomScreenMode.ExclusiveFullScreen,
			FullScreenMode.Windowed => CustomScreenMode.Windowed,
			_ => CustomScreenMode.FullScreenWindow,
		};
	}
}
