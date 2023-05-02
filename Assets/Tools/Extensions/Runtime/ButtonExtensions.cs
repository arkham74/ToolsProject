using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JD
{
	public static class ButtonExtensions
	{
		public static IEnumerator WaitForClick(this Button button)
		{
			bool click = false;
			void Click() => click = true;
			button.onClick.AddListener(Click);
			yield return new WaitUntil(() => click);
			button.onClick.RemoveListener(Click);
		}

		public static void RemoveAllListeners(this Button button)
		{
			button.onClick.RemoveAllListeners();
		}

		public static void ReplaceListener(this Button button, UnityAction func)
		{
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(func);
		}

		public static void ReplaceListener<T>(this Button button, UnityAction<T> func, T param) where T : class
		{
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => func(param));
		}

		public static void ReplaceListener(this ButtonHold button, UnityAction func)
		{
			button.onHold.RemoveAllListeners();
			button.onHold.AddListener(func);
		}

		public static void AddListener(this Button button, UnityAction func)
		{
			button.onClick.AddListener(func);
		}

		public static void AddListener<T>(this Button button, UnityAction<T> func, T param) where T : class
		{
			button.onClick.AddListener(() => func(param));
		}

		public static void AddListener(this ButtonHold button, UnityAction func)
		{
			button.onHold.AddListener(func);
		}
	}
}