using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using TMPro;
using NaughtyAttributes;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tag = NaughtyAttributes.TagAttribute;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;

#endif

[ExecuteAlways]
public class SelectableExtend : MonoBehaviour, IPointerEnterHandler, ISubmitHandler, IPointerClickHandler,
	ISelectHandler, IDeselectHandler, IPointerExitHandler
{
	public Selectable selectable;
	public SelectableData data;
	private Image image;
	private bool skipSound;

#if UNITY_EDITOR
	private void Reset()
	{
		selectable = GetComponent<Selectable>();
		data = AssetTools.FindAssetByType<SelectableData>();
	}
#endif

	private void Awake()
	{
		if (Application.isPlaying)
		{
			image = new GameObject().AddComponent<Image>();
			image.transform.SetParent(transform);
			image.sprite = data.normal;
			image.type = Image.Type.Sliced;
			RectTransform rectTransform = image.rectTransform;
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
			rectTransform.offsetMax = Vector2.zero;
			rectTransform.offsetMin = Vector2.zero;
			rectTransform.localScale = Vector3.one;
		}
	}

	private void Update()
	{
		selectable.colors = data.colorBlock;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		HighLight();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (EventSystem.current.currentSelectedGameObject != selectable.gameObject)
		{
			LowLight();
		}
	}

	public void OnSelect(BaseEventData eventData)
	{
		HighLight();
	}

	public void OnDeselect(BaseEventData eventData)
	{
		LowLight();
	}

	private void LowLight()
	{
		image.sprite = data.normal;
	}

	private void HighLight()
	{
		if (!skipSound)
		{
			AudioManager.Instance.PlayHover();
		}
		image.sprite = data.hover;
		skipSound = false;
	}

	public void OnSubmit(BaseEventData eventData)
	{
		PlayClickOrDisable();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		PlayClickOrDisable();
	}

	private void PlayClickOrDisable()
	{
		if (selectable.interactable)
			AudioManager.Instance.PlayClick();
		else
			AudioManager.Instance.PlayDisabled();
	}

	public void Select(bool nosound = false)
	{
		skipSound = nosound;
		selectable.SelectButton();
	}
}