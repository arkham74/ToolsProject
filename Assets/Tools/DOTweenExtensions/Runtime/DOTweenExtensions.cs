using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using Text = TMPro.TextMeshProUGUI;
using UnityEngine;
using UnityEngine.UI;

namespace JD
{
	public static partial class DOTweenExtensions
	{
		public static Tweener DOBlendablePunchLocalRotation(this Transform target, Vector3 punch, float duration, int vibrato = 10, float elasticity = 1f)
		{
			if (duration <= 0f)
			{
				if (Debugger.logPriority > 0)
				{
					Debug.LogWarning("DOBlendablePunchRotation: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			Vector3 to = Vector3.zero;
			return DOTween.Punch(() => to, delegate (Vector3 v)
			{
				Quaternion rotation = Quaternion.Euler(to.x, to.y, to.z);
				Quaternion quaternion = Quaternion.Euler(v.x, v.y, v.z) * Quaternion.Inverse(rotation);
				to = v;
				Quaternion rotation2 = target.localRotation;
				target.localRotation = rotation2 * Quaternion.Inverse(rotation2) * quaternion * rotation2;
			}, punch, duration, vibrato, elasticity).Blendable().SetTarget(target);
		}

		public static TweenerCore<Vector3, Vector3, VectorOptions> DOTransformPosition(this Transform source, Transform target, Vector3 offset, float duration)
		{
			Vector3 endValue() => target.position + offset;
			TweenerCore<Vector3, Vector3, VectorOptions> t = DOTween.To(() => source.position, e => source.position = endValue(), endValue(), duration);
			t.SetTarget(source);
			return t;
		}

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
					Debugger.LogWarning("You can't pass a NULL string to DOText: an empty string will be used instead to avoid errors");
				}

				endValue = "";
			}

			TweenerCore<string, string, StringOptions> t = DOTween.To(() => target.text, x => target.text = x, endValue, duration);
			t.SetOptions(richTextEnabled, scrambleMode, scrambleChars).SetTarget(target);
			return t;
		}

		public static TweenerCore<int, int, NoOptions> DORevealText(this TextMeshProUGUI target, string endValue, float duration)
		{
			if (endValue == null)
			{
				endValue = "";
			}

			target.text = endValue;
			TweenerCore<int, int, NoOptions> t = DOTween.To(() => target.maxVisibleCharacters, x => target.maxVisibleCharacters = x, endValue.Length, duration);
			t.SetTarget(target).From(0);
			return t;
		}

		public static TweenerCore<int, int, NoOptions> DONumbers(this TextMeshProUGUI target, int endValue, float duration, string @string = "{0}", int startValue = 0)
		{
			target.text = startValue.ToString();
			TweenerCore<int, int, NoOptions> t = DOTween.To(() => int.Parse(target.text), x => target.text = string.Format(@string, x), endValue, duration);
			t.SetTarget(target);
			return t;
		}

		public static void ScrollTo(this ScrollRect scroller, RectTransform target, float duration = 0.5f)
		{
			Canvas.ForceUpdateCanvases();

			Vector2 contentPos = scroller.viewport.InverseTransformPoint(scroller.content.position);
			Vector2 childPos = scroller.viewport.InverseTransformPoint(target.position);
			Vector2 endPos = contentPos - childPos;

			if (!scroller.horizontal)
			{
				endPos.x = contentPos.x;
			}

			if (!scroller.vertical)
			{
				endPos.y = contentPos.y;
			}

			if (duration > 0)
			{
				scroller.content.DOKill(true);
				scroller.content.DOLocalMove(endPos, duration).SetUpdate(true);
			}
			else
			{
				scroller.content.localPosition = endPos;
			}
		}
	}
}