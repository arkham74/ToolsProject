using System;
using System.Globalization;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD
{
	[Serializable]
	public struct ColorHSV : IEquatable<ColorHSV>, IFormattable
	{
		public float h;
		public float s;
		public float v;
		public float a;

		public ColorHSV(float h, float s, float v)
		{
			this.h = h;
			this.s = s;
			this.v = v;
			this.a = 1;
		}

		public ColorHSV(float h, float s, float v, float a)
		{
			this.h = h;
			this.s = s;
			this.v = v;
			this.a = a;
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (string.IsNullOrEmpty(format))
			{
				format = "F3";
			}
			if (formatProvider == null)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return string.Format("HSVA({0}, {1}, {2}, {3})", h.ToString(format, formatProvider), s.ToString(format, formatProvider), v.ToString(format, formatProvider), a.ToString(format, formatProvider));
		}

		public string ToString(string format)
		{
			return ToString(format, null);
		}

		public override string ToString()
		{
			return ToString(null, null);
		}

		public override int GetHashCode()
		{
			return h.GetHashCode() ^ (s.GetHashCode() << 2) ^ (v.GetHashCode() >> 2) ^ (a.GetHashCode() >> 1);
		}

		public bool Equals(ColorHSV other)
		{
			return h.Equals(other.h) && s.Equals(other.s) && v.Equals(other.v) && a.Equals(other.a);
		}

		public override bool Equals(object other)
		{
			if (other is ColorHSV hsv)
			{
				return Equals(hsv);
			}
			return false;
		}

		public static ColorHSV operator +(ColorHSV a, ColorHSV b) => new ColorHSV(a.h + b.h, a.s + b.s, a.v + b.v, a.a + b.a);
		public static ColorHSV operator *(ColorHSV a, ColorHSV b) => new ColorHSV(a.h * b.h, a.s * b.s, a.v * b.v, a.a * b.a);
		public static ColorHSV operator -(ColorHSV a, ColorHSV b) => new ColorHSV(a.h - b.h, a.s - b.s, a.v - b.v, a.a - b.a);

		public static ColorHSV operator *(ColorHSV a, float b) => new ColorHSV(a.h * b, a.s * b, a.v * b, a.a * b);
		public static ColorHSV operator *(float b, ColorHSV a) => new ColorHSV(a.h * b, a.s * b, a.v * b, a.a * b);

		public static ColorHSV operator /(ColorHSV a, float b) => new ColorHSV(a.h / b, a.s / b, a.v / b, a.a / b);

		public static bool operator ==(ColorHSV lhs, ColorHSV rhs) => (Vector4)lhs == (Vector4)rhs;
		public static bool operator !=(ColorHSV lhs, ColorHSV rhs) => !(lhs == rhs);

		public static implicit operator ColorHSV(Color rgb) => rgb.ToHSV();
		public static explicit operator Color(ColorHSV hsv) => hsv.ToRGB();

		public static implicit operator Vector3(ColorHSV hsv) => new Vector3(hsv.h, hsv.s, hsv.v);
		public static implicit operator Vector4(ColorHSV hsv) => new Vector4(hsv.h, hsv.s, hsv.v, hsv.a);

		public static implicit operator ColorHSV(Vector3 vec) => new ColorHSV(vec.x, vec.y, vec.z);
		public static implicit operator ColorHSV(Vector4 vec) => new ColorHSV(vec.x, vec.y, vec.z, vec.w);
	}
}
