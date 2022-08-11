using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace JD
{
	public static class ButtonExtensions
	{
		public static void Register(this Button button, UnityAction func)
		{
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(func);
		}

		public static void Register<T>(this Button button, UnityAction<T> func, T param) where T : class
		{
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => func(param));
		}

		public static void Register(this ButtonHold button, UnityAction func)
		{
			button.onHold.RemoveAllListeners();
			button.onHold.AddListener(func);
		}
	}
}