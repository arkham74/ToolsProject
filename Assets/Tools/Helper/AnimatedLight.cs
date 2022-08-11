using Freya;
using UnityEngine;
using Text = TMPro.TextMeshProUGUI;

namespace JD
{
	public class AnimatedLight : MonoBehaviour
	{
		[SerializeField] private Light target;
		[SerializeField] private float lenght = 20;
		[SerializeField] private AnimationCurve curve = AnimationCurve.Constant(0, 1, 1);

		private float intensity;

		private void Start()
		{
			intensity = target.intensity;
		}

		private void Update()
		{
			float time = (Time.realtimeSinceStartup / lenght).Repeat(1);
			float value = curve.Evaluate(time);
			target.intensity = value * intensity;
		}
	}
}