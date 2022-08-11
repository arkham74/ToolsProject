using UnityEngine;
using UnityEngine.UI;

namespace JD
{
	public static class ColorExtensions
	{
		public static Color R(this Color color, float r)
		{
			return new Color(r, color.g, color.b, color.a);
		}

		public static Color G(this Color color, float g)
		{
			return new Color(color.r, g, color.b, color.a);
		}

		public static Color B(this Color color, float b)
		{
			return new Color(color.r, color.g, b, color.a);
		}

		public static Color A(this Color color, float a)
		{
			return new Color(color.r, color.g, color.b, a);
		}

		public static string ToHtml(this Color color)
		{
			return ColorUtility.ToHtmlStringRGB(color);
		}

		public static string ToHtml(this Color32 color)
		{
			return ColorUtility.ToHtmlStringRGB(color);
		}

		public static Color32 ToColor32(this Color color)
		{
			byte r = (byte)(color.r * 255);
			byte g = (byte)(color.g * 255);
			byte b = (byte)(color.b * 255);
			byte a = (byte)(color.a * 255);
			return new Color32(r, g, b, a);
		}

		public static ColorBlock ChangeNormal(this ColorBlock colorBlock, Color normal)
		{
			colorBlock.normalColor = normal;
			return colorBlock;
		}
	}
}