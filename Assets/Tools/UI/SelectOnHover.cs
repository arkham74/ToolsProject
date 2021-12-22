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
using NaughtyAttributes;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tag = NaughtyAttributes.TagAttribute;

namespace StuntMasters
{
	public class SelectOnHover : MonoBehaviour, IPointerEnterHandler
	{
		public Selectable selectable;

		private void Reset()
		{
			selectable = GetComponentInChildren<Selectable>();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			selectable.Select();
		}
	}
}