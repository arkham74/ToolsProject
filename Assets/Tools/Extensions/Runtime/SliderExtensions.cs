using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JD
{
	public static class SliderExtensions
	{
		public static void ReplaceListener(this Slider slider, UnityAction<float> func)
		{
			slider.onValueChanged.RemoveAllListeners();
			slider.onValueChanged.AddListener(func);
		}

		public static void AddListener(this Slider slider, UnityAction<float> func)
		{
			slider.onValueChanged.AddListener(func);
		}

		public static void SetNormalizedValueWithoutNotify(this Slider slider, float normalized)
		{
			float realValue = Mathf.Lerp(slider.minValue, slider.maxValue, normalized);
			slider.SetValueWithoutNotify(realValue);
		}
	}
}