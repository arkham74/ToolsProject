using UnityEngine;
using Text = TMPro.TextMeshProUGUI;

public class VersionNumberTMP : MonoBehaviour
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