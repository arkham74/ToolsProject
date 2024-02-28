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
using JD.DataSync;

namespace JD
{
	public class ButtonNoSelectable : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField] private bool _interactable = true;
		[SerializeField] private Graphic target;
		[SerializeField] private SyncData colors;
		[SerializeField] private UnityEvent onClick;

		private bool hover;

		public UnityEvent OnClick => onClick;

		public bool interactable
		{
			get => _interactable;
			set => SetInteractable(value);
		}

		private void OnDisable()
		{
			hover = false;
		}

		private void OnEnable()
		{
			Refresh();
		}

#if UNITY_EDITOR
		private void Reset()
		{
			target = GetComponentInChildren<Graphic>();
			colors = AssetTools.FindAssetByType<ColorBlockData>();
		}

		private void OnValidate()
		{
			Refresh();
		}
#endif

		private void SetInteractable(bool value)
		{
			_interactable = value;
			Refresh();
		}

		private void Refresh()
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

		public void ReplaceListener(UnityAction action)
		{
			if (onClick != null)
			{
				onClick.RemoveAllListeners();
				onClick.AddListener(action);
			}
		}

		public void AddListener(UnityAction action)
		{
			if (onClick != null)
			{
				onClick.AddListener(action);
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (interactable)
			{
				if (onClick != null)
				{
					onClick.Invoke();
				}
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
			if (target)
			{
				colors.Highlight(target);
			}
		}

		private void Normal()
		{
			if (target)
			{
				colors.Normal(target);
			}
		}

		private void Disable()
		{
			if (target)
			{
				colors.Disabled(target);
			}
		}
	}
}
