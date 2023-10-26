using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JD
{
	public static class EventExtensions
	{
		public static void ReplaceListener<T>(this UnityEvent<T> unityEvent, UnityAction<T> unityAction)
		{
			unityEvent.RemoveAllListeners();
			unityEvent.AddListener(unityAction);
		}

		public static void ReplaceListener(this UnityEvent unityEvent, UnityAction unityAction)
		{
			unityEvent.RemoveAllListeners();
			unityEvent.AddListener(unityAction);
		}

		public static void ReplaceListener(this Toggle toggle, UnityAction<bool> func)
		{
			toggle.onValueChanged.RemoveAllListeners();
			toggle.onValueChanged.AddListener(func);
		}

		public static void AddListener(this Toggle toggle, UnityAction<bool> func)
		{
			toggle.onValueChanged.AddListener(func);
		}

		public static void ReplaceListener(this TMP_InputField input, UnityAction<string> func)
		{
			input.onValueChanged.RemoveAllListeners();
			input.onValueChanged.AddListener(func);
		}

		public static void AddListener(this TMP_InputField input, UnityAction<string> func)
		{
			input.onValueChanged.AddListener(func);
		}
	}
}