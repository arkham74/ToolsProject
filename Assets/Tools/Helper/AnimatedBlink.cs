using UnityEngine;
using UnityEngine.Events;
using Text = TMPro.TextMeshProUGUI;

public class AnimatedBlink : MonoBehaviour
{
	[SerializeField] private bool invert;
	[SerializeField] private float speed = 1f;
	[SerializeField] private float threshold = 0.5f;
	[SerializeField] private UnityEvent<bool> callback;

	private void Update()
	{
		float time = Time.realtimeSinceStartup;
		float value = Mathf.Sin(time * speed);
		if (invert)
			callback.Invoke(value < threshold);
		else
			callback.Invoke(value > threshold);
	}
}