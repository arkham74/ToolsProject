using System;
using UnityEngine;
using UnityEngine.UI;

namespace JD.DataSync
{
	public class ColorBlockData : SyncData
	{
		[SerializeField] private ColorBlock colorBlock = ColorBlock.defaultColorBlock;
		[SerializeField] private bool ignoreTimeScale = true;
		[SerializeField] private bool useRGB = true;
		[SerializeField] private bool useAlpha = true;

		public ColorBlock ColorBlock => colorBlock;

		public override void Normal(Graphic target)
		{
			float fadeDuration = colorBlock.fadeDuration;
			target.CrossFadeColor(colorBlock.normalColor, fadeDuration, ignoreTimeScale, useAlpha, useRGB);
		}

		public override void Highlight(Graphic target)
		{
			float fadeDuration = colorBlock.fadeDuration;
			target.CrossFadeColor(colorBlock.highlightedColor, fadeDuration, ignoreTimeScale, useAlpha, useRGB);
		}

		public override void Disabled(Graphic target)
		{
			float fadeDuration = colorBlock.fadeDuration;
			target.CrossFadeColor(colorBlock.disabledColor, fadeDuration, ignoreTimeScale, useAlpha, useRGB);
		}
	}
}