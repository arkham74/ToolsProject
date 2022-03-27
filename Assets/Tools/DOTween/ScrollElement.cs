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
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;

public class ScrollElement : MonoBehaviour, ISelectHandler
{
	public float waitTime = 0.1f;
	public RectTransform element;
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
		if (scrollRect.velocity.magnitude > 0.01f) return;
		StartCoroutine(ScrollToElement());
	}

	private IEnumerator ScrollToElement()
	{
		yield return new WaitForSeconds(waitTime);
		scrollRect.ScrollTo(element);
	}
}
#endif