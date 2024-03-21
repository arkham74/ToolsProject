using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace JD
{
	public static class ColorExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceRGB(this Color from, Color to) => ColorTools.DistanceRGB(from, to);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceRGBA(this Color from, Color to) => ColorTools.DistanceRGBA(from, to);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceRGB(this Color32 from, Color32 to) => ColorTools.DistanceRGB(from, to);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceRGBA(this Color32 from, Color32 to) => ColorTools.DistanceRGBA(from, to);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ColorHSV ToHSV(this Color32 rgb32)
		{
			Color rgb = rgb32;
			Color.RGBToHSV(rgb, out float h, out float s, out float v);
			return new ColorHSV(h, s, v, rgb.a);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ColorHSV ToHSV(this Color rgb)
		{
			Color.RGBToHSV(rgb, out float h, out float s, out float v);
			return new ColorHSV(h, s, v, rgb.a);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color ToRGB(this ColorHSV hsv)
		{
			Color color = Color.HSVToRGB(hsv.h, hsv.s, hsv.v);
			color.a = hsv.a;
			return color;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color WithRed(this Color color, float r)
		{
			return new Color(r, color.g, color.b, color.a);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color WithGreen(this Color color, float g)
		{
			return new Color(color.r, g, color.b, color.a);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color WithBlue(this Color color, float b)
		{
			return new Color(color.r, color.g, b, color.a);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string ToHexString(this Color32 color)
		{
			return ColorUtility.ToHtmlStringRGB(color);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color FromHexString(this string html)
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color32 ToColor32(this Color color)
		{
			return (Color32)color;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color ToColor(this Color32 color32)
		{
			return (Color)color32;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color32 ToLinear(this Color32 color32)
		{
			return color32.ToColor().linear.ToColor32();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color32 ToGamma(this Color32 color32)
		{
			return color32.ToColor().gamma.ToColor32();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ColorBlock ChangeNormal(this ColorBlock colorBlock, Color normal)
		{
			colorBlock.normalColor = normal;
			return colorBlock;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EqualRGB(this Color32 a, Color32 b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EqualRGBA(this Color32 a, Color32 b)
		{
			return EqualRGB(a, b) && a.a == b.a;
		}
	}
}