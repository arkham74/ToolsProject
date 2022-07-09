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
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using UnityEngine.EventSystems;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace CustomTools
{
	public class ButtonNoSelectable : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField] private Graphic target;
		[SerializeField] private ColorBlockData colors;
		[SerializeField] private UnityEvent onClick;

		public UnityEvent OnClick => onClick;

#if UNITY_EDITOR
		private void Reset()
		{
			target = GetComponentInChildren<Graphic>();
			colors = AssetTools.FindAssetByType<ColorBlockData>();
		}

		private void OnValidate()
		{
			if (target && colors)
				target.color = colors.colorBlock.normalColor;
		}
#endif

		public void Register(UnityAction action)
		{
			onClick.RemoveAllListeners();
			onClick.AddListener(action);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			// target.color = colors.colorBlock.pressedColor;
			onClick.Invoke();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			target.color = colors.colorBlock.highlightedColor;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			target.color = colors.colorBlock.normalColor;
		}
	}
}