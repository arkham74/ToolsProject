using System;
using UnityEngine;

namespace JD
{
	public class ColorDataColor : ColorData
	{
		[SerializeField] private Color color = Color.white;
		public static implicit operator Color(ColorDataColor data) => data.color;
	}
}