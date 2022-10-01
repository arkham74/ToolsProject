using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using TMPro;
using Freya;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tools = JD.Tools;
using System.Runtime.CompilerServices;

namespace JD
{
	public readonly partial struct Hex : IEquatable<Hex>, IFormattable
	{
		public readonly float q;
		public readonly float r;
		public readonly float s;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Hex(float q, float r, float s)
		{
			this.q = q;
			this.r = r;
			this.s = s;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Hex(float q, float r)
		{
			this.q = q;
			this.r = r;
			this.s = -q - r;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex operator -(Hex hex) => hex.Negate();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex operator +(Hex a, Hex b) => a.Add(b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex operator -(Hex a, Hex b) => a.Sub(b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex operator *(Hex a, float b) => a.Mul(b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Hex operator /(Hex a, float b) => a.Div(b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Hex a, Hex b) => a.Equals(b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Hex a, Hex b) => !a.Equals(b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode() => q.GetHashCode() ^ (r.GetHashCode() << 2) ^ (s.GetHashCode() >> 2);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString() => ToString(null, null);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider) => $"({q}, {r}, {s})";

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Hex other) => q == other.q && r == other.r && s == other.s;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			if (obj is Hex hex)
			{
				return Equals(hex);
			}

			return false;
		}
	}
}