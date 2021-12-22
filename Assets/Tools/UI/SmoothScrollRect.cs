//#if TOOLS_DOTWEEN

using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityEngine.UI.Extensions
{
	public class SmoothScrollRect : ScrollRect
	{
		public bool smoothScroll;
		public float smoothTime = 0.5f;

		public override void OnScroll(PointerEventData data)
		{
			if (smoothScroll)
			{
				if (!IsActive()) return;
				Vector2 positionBefore = normalizedPosition;
				this.DOKill(true);
				base.OnScroll(data);
				Vector2 positionAfter = normalizedPosition;
				normalizedPosition = positionBefore;
				this.DONormalizedPos(positionAfter, smoothTime);
			}
			else
			{
				base.OnScroll(data);
			}
		}
	}

//#endif
}