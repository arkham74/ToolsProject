using UnityEngine;
using Text = TMPro.TextMeshProUGUI;

namespace JD
{
	public class VersionNumber : MonoBehaviour
	{
		[SerializeField] private Text text;

		private void Reset()
		{
			text = GetComponent<Text>();
		}

		private void Start()
		{
			text.text = Application.version;
		}
	}
}