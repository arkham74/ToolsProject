using JD;
using UnityEngine;

namespace JD
{
	public static class SteamworksExtensions
	{
		public static Color32 ToColor32(this Steamworks.Data.Color image)
		{
			return new Color32(image.r, image.g, image.b, image.a);
		}

		public static UnityEngine.Color ToColor(this Steamworks.Data.Color image)
		{
			return image.ToColor32();
		}

		public static Texture2D ToTexture2D(this Steamworks.Data.Image image)
		{
			int width = (int)image.Width;
			int height = (int)image.Height;

			// Create a new Texture2D
			Texture2D avatar = new Texture2D(width, height, TextureFormat.ARGB32, false);

			// Set filter type, or else its really blury
			avatar.filterMode = FilterMode.Trilinear;

			// // Flip image
			for (int x = 0; x < image.Width; x++)
			{
				for (int y = 0; y < image.Height; y++)
				{
					Color32 color = image.GetPixel(x, y).ToColor32();
					avatar.SetPixel(x, height - y, color);
				}
			}

			// avatar.LoadImage(image.Data);
			// avatar.LoadRawTextureData<Color32>(image.Data);

			avatar.Apply();
			return avatar;
		}
	}
}