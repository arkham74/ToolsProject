using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JD
{
	public static class SliderExtensions
	{
		public static void ReplaceListener(this Slider slider, UnityAction<float> call)
		{
			slider.onValueChanged.RemoveAllListeners();
			slider.onValueChanged.AddListener(call);
		}

		public static void AddListener(this Slider slider, UnityAction<float> call)
		{
			slider.onValueChanged.AddListener(call);
		}

		public static void SetNormalizedValueWithoutNotify(this Slider slider, float normalized)
		{
			float realValue = Mathf.Lerp(slider.minValue, slider.maxValue, normalized);
			slider.SetValueWithoutNotify(realValue);
		}
	}
}