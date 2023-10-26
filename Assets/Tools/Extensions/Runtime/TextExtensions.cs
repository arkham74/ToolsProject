using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;

namespace JD
{
	public static class TextExtensions
	{
		public static void SetText(this TextMeshProUGUI text, string format, params object[] args)
		{
			text.text = string.Format(format, args);
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