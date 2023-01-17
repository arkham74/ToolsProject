using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JD
{
	public static class SelectableExtensions
	{
		public static void FindAndSetUp(this Selectable selectable)
		{
			selectable.SetUp(selectable.FindSelectableOnUp());
		}

		public static void FindAndSetDown(this Selectable selectable)
		{
			selectable.SetDown(selectable.FindSelectableOnDown());
		}

		public static void FindAndSetLeft(this Selectable selectable)
		{
			selectable.SetLeft(selectable.FindSelectableOnLeft());
		}

		public static void FindAndSetRight(this Selectable selectable)
		{
			selectable.SetRight(selectable.FindSelectableOnRight());
		}

		public static void SetUp(this Selectable button, Selectable selectable, Navigation.Mode mode = Navigation.Mode.Explicit)
		{
			Navigation navdisplay = button.navigation;
			navdisplay.selectOnUp = selectable;
			navdisplay.mode = mode;
			button.navigation = navdisplay;
		}

		public static void SetDown(this Selectable button, Selectable selectable, Navigation.Mode mode = Navigation.Mode.Explicit)
		{
			Navigation navdisplay = button.navigation;
			navdisplay.selectOnDown = selectable;
			navdisplay.mode = mode;
			button.navigation = navdisplay;
		}

		public static void SetLeft(this Selectable button, Selectable selectable, Navigation.Mode mode = Navigation.Mode.Explicit)
		{
			Navigation navdisplay = button.navigation;
			navdisplay.selectOnLeft = selectable;
			navdisplay.mode = mode;
			button.navigation = navdisplay;
		}

		public static void SetRight(this Selectable button, Selectable selectable, Navigation.Mode mode = Navigation.Mode.Explicit)
		{
			Navigation navdisplay = button.navigation;
			navdisplay.selectOnRight = selectable;
			navdisplay.mode = mode;
			button.navigation = navdisplay;
		}

		public static void SetMode(this Selectable button, Navigation.Mode mode)
		{
			Navigation navdisplay = button.navigation;
			navdisplay.mode = mode;
			button.navigation = navdisplay;
		}

		public static void SelectButton(this Selectable selectable)
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
	}
}
