#if TOOLS_DOTWEEN
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using TMPro;
using JD;
using Freya;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tools = JD.Tools;
using DG.Tweening;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace SAR
{
	public class FadeInTrigger : BaseTrigger
	{
		[SerializeField] private float duration = 1f;
		[SerializeField] private float delay = 0f;
		[SerializeField] private float start = 0f;
		[SerializeField] private float end = 1f;
		[SerializeField] private CanvasGroup canvasGroup;

		private void Reset()
		{
			canvasGroup = GetComponentInChildren<CanvasGroup>();
		}

		protected override void Trigger()
		{
			canvasGroup.DOFade(end, duration).From(start).SetDelay(delay);
		}
	}
}
#endif