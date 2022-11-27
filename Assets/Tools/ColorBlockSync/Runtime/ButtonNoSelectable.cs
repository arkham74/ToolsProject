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
using Random = UnityEngine.Random;
using UnityEngine.EventSystems;
using System.Text;

namespace JD
{
	public class ButtonNoSelectable : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField] private Graphic target;
		[SerializeField] private ColorBlockData colors;
		[SerializeField] private UnityEvent onClick;
		[SerializeField] private bool _interactable = true;

		private bool hover;

		public UnityEvent OnClick => onClick;

		public bool interactable
		{
			get => _interactable;
			set => SetInteractable(value);
		}

#if UNITY_EDITOR
		private void Reset()
		{
			target = GetComponentInChildren<Graphic>();
			colors = AssetTools.FindAssetByType<ColorBlockData>();
		}

		private void OnValidate()
		{
			if (interactable)
			{
				if (hover)
				{
					Hover();
				}
				else
				{
					Normal();
				}
			}
			else
			{
				Disable();
			}
		}
#endif

		private void SetInteractable(bool value)
		{
			_interactable = value;

			if (interactable)
			{
				if (hover)
				{
					Hover();
				}
				else
				{
					Normal();
				}
			}
			else
			{
				Disable();
			}
		}

		public void ReplaceListener(UnityAction action)
		{
			onClick.RemoveAllListeners();
			onClick.AddListener(action);
		}

		public void AddListener(UnityAction action)
		{
			onClick.AddListener(action);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (interactable)
			{
				onClick.Invoke();
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (interactable)
			{
				hover = true;
				Hover();
			}
			else
			{
				Disable();
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (interactable)
			{
				hover = false;
				Normal();
			}
			else
			{
				Disable();
			}
		}

		private void Hover()
		{
			target.color = colors.colorBlock.highlightedColor;
		}

		private void Normal()
		{
			target.color = colors.colorBlock.normalColor;
		}

		private void Disable()
		{
			target.color = colors.colorBlock.disabledColor;
		}
	}
}
