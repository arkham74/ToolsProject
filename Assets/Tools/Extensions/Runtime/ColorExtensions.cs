using UnityEngine;
using UnityEngine.UI;

namespace JD
{
	public static class ColorExtensions
	{
		public static Color WithRed(this Color color, float r)
		{
			return new Color(r, color.g, color.b, color.a);
		}

		public static Color WithGreen(this Color color, float g)
		{
			return new Color(color.r, g, color.b, color.a);
		}

		public static Color WithBlue(this Color color, float b)
		{
			return new Color(color.r, color.g, b, color.a);
		}

		public static string ToHtml(this Color color)
		{
			return ColorUtility.ToHtmlStringRGB(color);
		}

		public static string ToHtml(this Color32 color)
		{
			return ColorUtility.ToHtmlStringRGB(color);
		}

		public static Color FromHtml(this string html)
		{
			if (ColorUtility.TryParseHtmlString(html, out Color color))
			{
				return color;
			}

			if (ColorUtility.TryParseHtmlString("#" + html, out color))
			{
				return color;
			}

			return Color.black;
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