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