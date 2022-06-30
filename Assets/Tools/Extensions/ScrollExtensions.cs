using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class ScrollExtensions
{
	public static void ShowRect(this ScrollRect instance, RectTransform child)
	{
		Canvas.ForceUpdateCanvases();
		Vector2 viewportLocalPosition = instance.viewport.localPosition;
		Vector2 childLocalPosition = child.localPosition;
		instance.content.localPosition = new Vector2(0 - (viewportLocalPosition.x + childLocalPosition.x), 0 - (viewportLocalPosition.y + childLocalPosition.y));
	}

	public static void SnapTo(this ScrollRect scroller, RectTransform target)
	{
		Canvas.ForceUpdateCanvases();

		Vector2 contentPos = scroller.transform.InverseTransformPoint(scroller.content.position);
		Vector2 childPos = scroller.transform.InverseTransformPoint(target.position);
		Vector2 endPos = contentPos - childPos;

		if (!scroller.horizontal)
		{
			endPos.x = contentPos.x;
		}

		if (!scroller.vertical)
		{
			endPos.y = contentPos.y;
		}

		scroller.content.anchoredPosition = endPos;
	}

	public static void Register(this Scrollbar button, UnityAction<float> func)
	{
		button.onValueChanged.RemoveAllListeners();
		button.onValueChanged.AddListener(func);
	}
}
