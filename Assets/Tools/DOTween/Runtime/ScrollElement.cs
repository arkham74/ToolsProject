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
using Text = TMPro.TextMeshProUGUI;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;


namespace JD
{
	public class ScrollElement : MonoBehaviour, ISelectHandler
	{
		[SerializeField] private float duration = 0.5f;
		[SerializeField] private float waitTime = 0.1f;
		[SerializeField] private RectTransform element;
		private ScrollRect scrollRect;

		private void Reset()
		{
			element = GetComponent<RectTransform>();
		}

		private void Awake()
		{
			scrollRect = GetComponentInParent<ScrollRect>();
		}

		public void OnSelect(BaseEventData eventData)
		{
			if (scrollRect.velocity.magnitude > 0.01f)
			{
				return;
			}

			StartCoroutine(ScrollToElement());
		}

		private IEnumerator ScrollToElement()
		{
			yield return new WaitForSeconds(waitTime);
			scrollRect.ScrollTo(element, duration);
		}
	}
}
#endif
