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
using JD;
using Freya;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tools = JD.Tools;

namespace JD
{
	public class SliderLabel : MonoBehaviour
	{
		[SerializeField] private Slider slider;
		[SerializeField] private ButtonNoSelectable nextButton;
		[SerializeField] private ButtonNoSelectable prevButton;
		[SerializeField] private Text label;

		public bool interactable
		{
			get => slider.interactable && nextButton.interactable && prevButton.interactable;
			set => slider.interactable = nextButton.interactable = prevButton.interactable = value;
		}

		private void Reset()
		{
			slider = GetComponentInChildren<Slider>();
			label = GetComponentsInChildren<Text>()[2];

			prevButton = GetComponentsInChildren<ButtonNoSelectable>()[0];
			nextButton = GetComponentsInChildren<ButtonNoSelectable>()[1];
		}

		public void Init(int def, int min, int max, Action<int> onChange)
		{
			Init(def, min, max, onChange, e => e.ToStringNonAllocation());
		}

		public void Init(int def, int min, int max, Action<int> onChange, Func<int, string> map)
		{
			void Refresh(int value)
			{
				slider.SetValueWithoutNotify(value);
				label.text = map(value).ToString();
			}

			void Slider_Change(float value)
			{
				int start = (int)value;
				start = start.Mod(min, max);
				onChange(start);
				Refresh(start);
			}

			void Button_Next()
			{
				Slider_Change(Mathf.Clamp(slider.value + 1, slider.minValue, slider.maxValue));
			}

			void Button_Prev()
			{
				Slider_Change(Mathf.Clamp(slider.value - 1, slider.minValue, slider.maxValue));
			}

			slider.minValue = min - 1;
			slider.maxValue = max + 1;
			Refresh(def);

			slider.ReplaceListener(Slider_Change);
			nextButton.ReplaceListener(Button_Next);
			prevButton.ReplaceListener(Button_Prev);

			if (slider is SliderMultiGraphics multi)
			{
				multi.onClick.ReplaceListener(Button_Next);
			}
		}
	}
}