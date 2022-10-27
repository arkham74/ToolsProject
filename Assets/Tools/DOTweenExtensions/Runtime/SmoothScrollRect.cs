using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JD
{
	public class SmoothScrollRect : ScrollRect
	{
		[SerializeField] private Ease smoothEase = Ease.Linear;
		[SerializeField] private bool smoothScroll = true;
		[SerializeField] private float smoothTime = 0.5f;

		public override void OnScroll(PointerEventData data)
		{
			if (smoothScroll)
			{
				if (!IsActive())
				{
					return;
				}

				Vector2 positionBefore = normalizedPosition;
				this.DOKill(true);
				base.OnScroll(data);
				Vector2 positionAfter = normalizedPosition;
				normalizedPosition = positionBefore;
				this.DONormalizedPos(positionAfter, smoothTime).SetEase(smoothEase);
			}
			else
			{
				base.OnScroll(data);
			}
		}
	}
}
