using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class SelectableExtensions
{
	public static void SetUp(this Selectable button, Selectable selectable)
	{
		Navigation navdisplay = button.navigation;
		navdisplay.selectOnUp = selectable;
		button.navigation = navdisplay;
	}

	public static void SetDown(this Selectable button, Selectable selectable)
	{
		Navigation navdisplay = button.navigation;
		navdisplay.selectOnDown = selectable;
		button.navigation = navdisplay;
	}

	public static void SetLeft(this Selectable button, Selectable selectable)
	{
		Navigation navdisplay = button.navigation;
		navdisplay.selectOnLeft = selectable;
		button.navigation = navdisplay;
	}

	public static void SetRight(this Selectable button, Selectable selectable)
	{
		Navigation navdisplay = button.navigation;
		navdisplay.selectOnRight = selectable;
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
		IEnumerator ButtonSelect()
		{
			EventSystem.current.SetSelectedGameObject(null);
			yield return new WaitForEndOfFrame();
			bool activeInHierarchy = selectable.gameObject.activeInHierarchy;
			bool interactable = selectable.interactable;
			bool isActiveAndEnabled = selectable.isActiveAndEnabled;
			if (selectable && isActiveAndEnabled && interactable && activeInHierarchy)
			{
				selectable.Select();
			}
		}

		selectable.StartCoroutine(ButtonSelect());
	}
}
