using DG.Tweening;
using NaughtyAttributes;
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

	[Button]
	public void Open()
	{
		transform.DOKill(true);
		transform.DOLocalMove(end, duration).From(startOpen).SetEase(easeIn);
	}

	[Button]
	public void Close()
	{
		transform.DOKill(true);
		transform.DOLocalMove(startClose, duration).From(end).SetEase(easeOut);
	}
}