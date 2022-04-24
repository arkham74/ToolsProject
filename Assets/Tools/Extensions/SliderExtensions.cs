using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class SliderExtensions
{
	public static void Register(this Slider slider, UnityAction<float> func)
	{
		slider.onValueChanged.RemoveAllListeners();
		slider.onValueChanged.AddListener(func);
	}

	public static void SetNormalizedValueWithoutNotify(this Slider slider, float normalized)
	{
		float realValue = Mathf.Lerp(slider.minValue, slider.maxValue, normalized);
		slider.SetValueWithoutNotify(realValue);
	}
}
