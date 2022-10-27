#if DOTWEEN_ENABLED
using DG.Tweening;
using UnityEngine;

namespace JD
{
	public class DOTweenMove : MonoBehaviour
	{
		public Vector3 start = Vector3.zero;
		public Vector3 end = Vector3.up;
		public float duration = 1f;
		public Ease ease = Ease.Linear;
		private Sequence sequence;

		protected void Start()
		{
			transform.DOKill();
			sequence = DOTween.Sequence();
			sequence.Append(transform.DOLocalMove(end, duration).From(start));
			sequence.Append(transform.DOLocalMove(start, duration).From(end));
			sequence.SetEase(ease);
			sequence.SetLoops(-1);
		}

		protected void OnDestroy()
		{
			transform.DOKill();
			sequence.Kill();
		}
	}
}
#endif