#if TOOLS_DOTWEEN
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static partial class DOTweenExtensions
{
	public static TweenerCore<Vector3, Vector3, VectorOptions> AnimateEnable(this Transform transform,
		float duration = 0.25f, float delay = 0, Ease ease = Ease.OutBack, bool timeScaleIndependent = false,
		float end = 1f)
	{
		transform.gameObject.SetActive(true);
		return AnimateShow(transform, duration, delay, ease, timeScaleIndependent, end);
	}

	public static TweenerCore<Vector3, Vector3, VectorOptions> AnimateDisable(this Transform transform,
		float duration = 0.25f, float delay = 0, Ease ease = Ease.InBack, bool timeScaleIndependent = false)
	{
		return AnimateHide(transform, duration, delay, ease, timeScaleIndependent).OnComplete(() => transform.gameObject.SetActive(false));
	}

	public static TweenerCore<Vector3, Vector3, VectorOptions> AnimateShow(this Transform transform,
		float duration = 0.25f, float delay = 0, Ease ease = Ease.OutBack, bool timeScaleIndependent = false,
		float end = 1f)
	{
		transform.DOKill(true);
		return transform.DOScale(end, duration).SetEase(ease).SetUpdate(timeScaleIndependent).From(0).SetDelay(delay);
	}

	public static TweenerCore<Vector3, Vector3, VectorOptions> AnimateHide(this Transform transform,
		float duration = 0.25f, float delay = 0, Ease ease = Ease.InBack, bool timeScaleIndependent = false)
	{
		transform.DOKill(true);
		return transform.DOScale(0, duration).SetEase(ease).SetUpdate(timeScaleIndependent).SetDelay(delay);
	}

	public static void AnimateDestroy(this Transform transform)
	{
		transform.AnimateHide().OnComplete(() => GameObject.Destroy(transform.gameObject));
	}

	public static TweenerCore<string, string, StringOptions> DOText(this TextMeshProUGUI target, string endValue, float duration, bool richTextEnabled = true, ScrambleMode scrambleMode = ScrambleMode.None, string scrambleChars = null)
	{
		if (endValue == null)
		{
			if (Debugger.logPriority > 0)
			{
				Debugger.LogWarning(
					"You can't pass a NULL string to DOText: an empty string will be used instead to avoid errors");
			}

			endValue = "";
		}

		TweenerCore<string, string, StringOptions> t = DOTween.To(() => target.text, x => target.text = x, endValue, duration);
		t.SetOptions(richTextEnabled, scrambleMode, scrambleChars).SetTarget(target);
		return t;
	}

	public static TweenerCore<int, int, NoOptions> DORevealText(this TextMeshProUGUI target, string endValue, float duration)
	{
		if (endValue == null) endValue = "";
		target.text = endValue;
		TweenerCore<int, int, NoOptions> t = DOTween.To(() => target.maxVisibleCharacters, x => target.maxVisibleCharacters = x, endValue.Length, duration);
		t.SetTarget(target).From(0);
		return t;
	}

	public static TweenerCore<int, int, NoOptions> DONumbers(this TextMeshProUGUI target, int endValue, float duration, string @string = "{0}")
	{
		target.text = "0";
		TweenerCore<int, int, NoOptions> t = DOTween.To(() => int.Parse(target.text), x => target.text = string.Format(@string, x), endValue, duration);
		t.SetTarget(target);
		return t;
	}

	public static void ScrollTo(this ScrollRect scroller, RectTransform target, float duration = 0.5f)
	{
		Canvas.ForceUpdateCanvases();

		Vector2 contentPos = scroller.transform.InverseTransformPoint(scroller.content.position);
		Vector2 childPos = scroller.transform.InverseTransformPoint(target.position);
		Vector2 endPos = contentPos - childPos;

		if (!scroller.horizontal)
		{
			endPos.x = contentPos.x;
		}

		if (!scroller.vertical)
		{
			endPos.y = contentPos.y;
		}

		DOTween.To(() => scroller.content.anchoredPosition, x => scroller.content.anchoredPosition = x, endPos, duration);
	}
}
#endif