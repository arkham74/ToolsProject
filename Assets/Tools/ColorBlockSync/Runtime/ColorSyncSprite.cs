using System;
using UnityEngine;

namespace JD
{
	[ExecuteAlways]
	public class ColorSyncSprite : ColorSync<ColorDataSprite>
	{
		[SerializeField] private SpriteRenderer spriteRenderer;

		private void Reset()
		{
			spriteRenderer = GetComponentInChildren<SpriteRenderer>(true);
		}

		public override void Apply()
		{
			if (spriteRenderer)
			{
				spriteRenderer.color = colorData;
				spriteRenderer.material = colorData;
				spriteRenderer.sprite = colorData;
			}
		}
	}
}