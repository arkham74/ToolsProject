using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using TMPro;
using Freya;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using System.Runtime.CompilerServices;

namespace JD
{
	[Serializable]
	public struct Hex : IEquatable<Hex>, IFormattable
	{
		[SerializeField] private Vector2 qr;

		public float Q => qr.x;
		public float R => qr.y;
		public float S => -Q - R;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Hex(Vector2 qr)
		{
			this.qr = qr;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Hex(float q, float r) : this(new Vector2(q, r))
		{

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
		public override int GetHashCode() => Q.GetHashCode() ^ (R.GetHashCode() << 2) ^ (S.GetHashCode() >> 2);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString() => ToString(null, null);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider) => $"({Q}, {R}, {S})";

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Hex other) => Q == other.Q && R == other.R && S == other.S;

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