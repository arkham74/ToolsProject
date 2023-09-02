using UnityEngine;

namespace JD
{
	[ExecuteAlways]
	public class ColorSyncSprite : ColorSync<ColorDataSprite>
	{
		[SerializeField] private SpriteRenderer spriteRenderer;

		public override void Apply()
		{
			spriteRenderer.color = colorData;
			spriteRenderer.material = colorData;
			spriteRenderer.sprite = colorData;
		}
	}
}