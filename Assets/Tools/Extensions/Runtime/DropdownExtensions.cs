using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JD
{
	public static class DropdownExtensions
	{
		public static void ReplaceListener(this TMP_Dropdown dropdown, UnityAction<int> func)
		{
			dropdown.onValueChanged.RemoveAllListeners();
			dropdown.onValueChanged.AddListener(func);
		}

		public static void AddListener(this TMP_Dropdown dropdown, UnityAction<int> func)
		{
			dropdown.onValueChanged.AddListener(func);
		}

		public static void ReplaceListener(this Dropdown dropdown, UnityAction<int> func)
		{
			dropdown.onValueChanged.RemoveAllListeners();
			dropdown.onValueChanged.AddListener(func);
		}

		public static void AddListener(this Dropdown dropdown, UnityAction<int> func)
		{
			dropdown.onValueChanged.AddListener(func);
		}
	}
}
