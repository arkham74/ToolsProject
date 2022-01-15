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
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tag = NaughtyAttributes.TagAttribute;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;

#endif

namespace CordBot
{
	[ExecuteAlways]
	public class SetImageColor : MonoBehaviour
	{
		public Image selectable;
		public SelectableData data;

#if UNITY_EDITOR
		private void Reset()
		{
			selectable = GetComponent<Image>();
			data = AssetTools.FindAssetByType<SelectableData>();
		}

		private void Update()
		{
			selectable.color = data.colorBlock.normalColor;
		}
#endif

		private void Start()
		{
			selectable.color = data.colorBlock.normalColor;
		}
	}
}