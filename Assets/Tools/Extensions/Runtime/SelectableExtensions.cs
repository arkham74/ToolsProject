using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JD
{
	public static class SelectableExtensions
	{
		public static void FindAndSetUp<T>(this T selectable) where T : Selectable
		{
			selectable.SetUp(selectable.FindSelectableOnUp());
		}

		public static void FindAndSetDown<T>(this T selectable) where T : Selectable
		{
			selectable.SetDown(selectable.FindSelectableOnDown());
		}

		public static void FindAndSetLeft<T>(this T selectable) where T : Selectable
		{
			selectable.SetLeft(selectable.FindSelectableOnLeft());
		}

		public static void FindAndSetRight<T>(this T selectable) where T : Selectable
		{
			selectable.SetRight(selectable.FindSelectableOnRight());
		}

		public static void SetUp<T0, T1>(this T0 button, T1 selectable, Navigation.Mode mode = Navigation.Mode.Explicit) where T0 : Selectable where T1 : Selectable
		{
			Navigation navdisplay = button.navigation;
			navdisplay.selectOnUp = selectable;
			navdisplay.mode = mode;
			button.navigation = navdisplay;
		}

		public static void SetDown<T0, T1>(this T0 button, T1 selectable, Navigation.Mode mode = Navigation.Mode.Explicit) where T0 : Selectable where T1 : Selectable
		{
			Navigation navdisplay = button.navigation;
			navdisplay.selectOnDown = selectable;
			navdisplay.mode = mode;
			button.navigation = navdisplay;
		}

		public static void SetLeft<T0, T1>(this T0 button, T1 selectable, Navigation.Mode mode = Navigation.Mode.Explicit) where T0 : Selectable where T1 : Selectable
		{
			Navigation navdisplay = button.navigation;
			navdisplay.selectOnLeft = selectable;
			navdisplay.mode = mode;
			button.navigation = navdisplay;
		}

		public static void SetRight<T0, T1>(this T0 button, T1 selectable, Navigation.Mode mode = Navigation.Mode.Explicit) where T0 : Selectable where T1 : Selectable
		{
			Navigation navdisplay = button.navigation;
			navdisplay.selectOnRight = selectable;
			navdisplay.mode = mode;
			button.navigation = navdisplay;
		}

		public static void SetMode<T>(this T button, Navigation.Mode mode) where T : Selectable
		{
			Navigation navdisplay = button.navigation;
			navdisplay.mode = mode;
			button.navigation = navdisplay;
		}

		public static void SelectDelayed<T>(this T selectable) where T : Selectable
		{
			if (selectable.gameObject.activeInHierarchy)
			{
				IEnumerator ButtonSelect()
				{
					yield return new WaitForEndOfFrame();
					bool interactable = selectable.interactable;
					bool isActiveAndEnabled = selectable.isActiveAndEnabled;
					if (isActiveAndEnabled && interactable)
					{
						selectable.Select();
					}
				}

				selectable.StartCoroutine(ButtonSelect());
			}
			else
			{
				selectable.Select();
			}
		}

		public static void SetupNavigationVertical<T>(this IList<T> selectables) where T : Selectable
		{
			for (int i = 0; i < selectables.Count; i++)
			{
				Selectable selectable = selectables[i];
				Navigation nav = selectable.navigation;
				nav.mode = Navigation.Mode.Explicit;
				nav.selectOnUp = selectables.Repeat(i - 1);
				nav.selectOnDown = selectables.Repeat(i + 1);
				selectable.navigation = nav;
			}
		}

		public static void SetupNavigationHorizontal<T>(this IList<T> selectables) where T : Selectable
		{
			for (int i = 0; i < selectables.Count; i++)
			{
				Selectable selectable = selectables[i];
				Navigation nav = selectable.navigation;
				nav.mode = Navigation.Mode.Explicit;
				nav.selectOnLeft = selectables.Repeat(i - 1);
				nav.selectOnRight = selectables.Repeat(i + 1);
				selectable.navigation = nav;
			}
		}
	}
}