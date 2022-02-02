#if TOOLS_DOTWEEN
using DG.Tweening;
using UnityEngine;

// ReSharper disable once InconsistentNaming
public class DOTweenLocalMove : MonoBehaviour
{
	public Vector3 startOpen = Vector3.one;
	public Vector3 startClose = Vector3.one;
	public Vector3 end = Vector3.zero;
	public float duration = 1f;
	public Ease easeIn = Ease.Linear;
	public Ease easeOut = Ease.Linear;

	public void Open()
	{
		transform.DOKill(true);
		transform.DOLocalMove(end, duration).From(startOpen).SetEase(easeIn);
	}

	public void Close()
	{
		transform.DOKill(true);
		transform.DOLocalMove(startClose, duration).From(end).SetEase(easeOut);
	}
}
#endif