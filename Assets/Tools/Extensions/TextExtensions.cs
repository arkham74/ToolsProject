using System.Threading.Tasks;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JD
{
	public static class TextExtensions
	{
		public static void Register(this TMP_Dropdown dropdown, UnityAction<int> func)
		{
			dropdown.onValueChanged.RemoveAllListeners();
			dropdown.onValueChanged.AddListener(func);
		}

		public static void Register(this TMP_InputField input, UnityAction<string> func)
		{
			input.onValueChanged.RemoveAllListeners();
			input.onValueChanged.AddListener(func);
		}

		public static async void AnimateNumber(this TextMeshProUGUI text, float maxScore, string format = "{0:0}", int ms = 1000)
		{
			if (text == null)
			{
				return;
			}

			float increase = maxScore / (ms / 10f);
			for (float score = 0; score <= maxScore; score += increase)
			{
				text.text = string.Format(format, score);
				await Task.Delay(10);
			}
			text.text = string.Format(format, maxScore);
		}

		public static async void AnimateNumber(this Text text, float maxScore, int ms = 1000)
		{
			float increase = maxScore / (ms / 10f);
			for (float score = 0; score <= maxScore; score += increase)
			{
				if (!text)
				{
					return;
				}

				text.text = score.ToString("0");
				await Task.Delay(10);
			}
			text.text = maxScore.ToString("0");
		}
	}
}
