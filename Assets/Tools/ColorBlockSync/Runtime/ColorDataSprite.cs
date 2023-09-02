using UnityEngine;

namespace JD
{
	public class ColorDataSprite : ColorDataColor
	{
		[SerializeField] private Material material;
		[SerializeField] private Sprite sprite;

		public static implicit operator Material(ColorDataSprite data) => data.material;
		public static implicit operator Sprite(ColorDataSprite data) => data.sprite;
	}
}