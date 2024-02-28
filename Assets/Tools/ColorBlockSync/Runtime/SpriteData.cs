using System;
using UnityEngine;
using UnityEngine.UI;

namespace JD.DataSync
{
	public class SpriteData : SyncData
	{
		[SerializeField] private Sprite normalSprite;
		[SerializeField] private Sprite highlightedSprite;
		[SerializeField] private Sprite disabledSprite;

		public override void Normal(Graphic target)
		{
			Override(target, normalSprite);
		}

		public override void Highlight(Graphic target)
		{
			Override(target, highlightedSprite);
		}

		public override void Disabled(Graphic target)
		{
			Override(target, disabledSprite);
		}

		private void Override(Graphic target, Sprite sprite)
		{
			if (sprite && target is Image image)
			{
				image.overrideSprite = sprite;
			}
		}
	}
}