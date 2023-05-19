using System;
using System.Collections;
using DG.Tweening;
using JD;
using UnityEngine;

namespace RTS
{
	public class CameraManager : Singleton<CameraManager>
	{
		[SerializeField] private Transform cameraTarget;

		public IEnumerator MoveToWait(Vector3 position)
		{
			yield return cameraTarget.DOMove(position, 10).SetEase(Ease.InOutQuad).SetSpeedBased(true).WaitForCompletion();
		}

		public IEnumerator MoveToWait(Vector3[] positions)
		{
			yield return cameraTarget.DOPath(positions, 10).SetEase(Ease.InOutQuad).SetSpeedBased(true).WaitForCompletion();
		}
	}
}