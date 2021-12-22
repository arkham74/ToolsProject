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
	public class NonSelectable : MonoBehaviour, ISelectHandler
	{
		private Selectable selectable;

		private void Start()
		{
			selectable = GetComponent<Selectable>();
		}

		public void OnSelect(BaseEventData eventData)
		{
			Debug.LogWarning("OnSelect");
			selectable.OnDeselect(eventData);
		}
	}
}