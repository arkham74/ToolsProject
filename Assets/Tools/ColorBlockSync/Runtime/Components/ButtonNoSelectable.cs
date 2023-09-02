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
		[FormerlySerializedAs("colors")][SerializeField] private ColorDataBlock colorsData;
		[SerializeField] private UnityEvent onClick;
		[FormerlySerializedAs("_interactable")][SerializeField] private bool interactable = true;

		private bool hover;

		public UnityEvent OnClick => onClick;

		public bool Interactable
		{
			get => interactable;
			set => SetInteractable(value);
		}

		#if UNITY_EDITOR
		private void Reset()
		{
			target = GetComponentInChildren<Graphic>();
			colorsData = AssetTools.FindAssetByType<ColorDataBlock>();
		}

		private void OnValidate()
		{
			if (Interactable)
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
			interactable = value;

			if (Interactable)
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
			if (Interactable)
			{
				onClick.Invoke();
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (Interactable)
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
			if (Interactable)
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
			ColorBlock block = colorsData;
			target.color = block.highlightedColor;
		}

		private void Normal()
		{
			ColorBlock block = colorsData;
			target.color = block.normalColor;
		}

		private void Disable()
		{
			ColorBlock block = colorsData;
			target.color = block.disabledColor;
		}
	}
}