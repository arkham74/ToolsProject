using System;
using UnityEngine;
using UnityEngine.UI;

namespace JD
{
	public class ColorDataBlock : ColorData
	{
		[SerializeField] private ColorBlock colorBlock = ColorBlock.defaultColorBlock;
		public static implicit operator ColorBlock(ColorDataBlock data) => data.colorBlock;
	}
}