using System;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

public static class ComponentExtensions
{
	public static Vector3 To(this Component s, Component t)
	{
		return s.transform.position.To(t.transform.position);
	}

	public static Vector3 DirTo(this Component s, Component t)
	{
		return s.transform.position.DirTo(t.transform.position);
	}

	public static void Disable(this GameObject go)
	{
		go.SetActive(false);
	}

	public static void Enable(this GameObject go)
	{
		go.SetActive(true);
	}

	public static void DisableGameObject(this MonoBehaviour go)
	{
		go.gameObject.SetActive(false);
	}

	public static void EnableGameObject(this MonoBehaviour go)
	{
		go.gameObject.SetActive(true);
	}

	public static void SetX(this Transform t, float x)
	{
		Vector3 p = t.position;
		p.x = x;
		t.position = p;
	}

	public static void SetY(this Transform t, float y)
	{
		Vector3 p = t.position;
		p.y = y;
		t.position = p;
	}

	public static void SetZ(this Transform t, float z)
	{
		Vector3 p = t.position;
		p.z = z;
		t.position = p;
	}

	public static async void AnimateNumber(this TextMeshProUGUI text, float maxScore, int ms = 1000)
	{
		float increase = maxScore / (ms / 10f);
		for (float score = 0; score <= maxScore; score += increase)
		{
			if (!text) return;
			text.text = score.ToString("0");
			await Task.Delay(10);
		}

		text.text = maxScore.ToString("0");
	}

	public static async void AnimateNumber(this Text text, float maxScore, int ms = 1000)
	{
		float increase = maxScore / (ms / 10f);
		for (float score = 0; score <= maxScore; score += increase)
		{
			if (!text) return;
			text.text = score.ToString("0");
			await Task.Delay(10);
		}

		text.text = maxScore.ToString("0");
	}

	public static void Register(this Scrollbar button, UnityAction<float> func)
	{
		button.onValueChanged.RemoveAllListeners();
		button.onValueChanged.AddListener(func);
	}

	public static void Register(this Button button, UnityAction func)
	{
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(func);
	}

	public static void Register(this ButtonHold button, UnityAction func)
	{
		button.onHold.RemoveAllListeners();
		button.onHold.AddListener(func);
	}

	public static void Register<T>(this Button button, UnityAction<T> func, T param) where T : class
	{
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(() => func(param));
	}

	public static void Register(this Slider slider, UnityAction<float> func)
	{
		slider.onValueChanged.RemoveAllListeners();
		slider.onValueChanged.AddListener(func);
	}

	public static void Register(this Toggle toggle, UnityAction<bool> func)
	{
		toggle.onValueChanged.RemoveAllListeners();
		toggle.onValueChanged.AddListener(func);
	}

	public static void Register(this Dropdown dropdown, UnityAction<int> func)
	{
		dropdown.onValueChanged.RemoveAllListeners();
		dropdown.onValueChanged.AddListener(func);
	}

	public static void Register(this TMP_Dropdown dropdown, UnityAction<int> func)
	{
		dropdown.onValueChanged.RemoveAllListeners();
		dropdown.onValueChanged.AddListener(func);
	}

	public static void Register(this TMP_InputField input, UnityAction<string> func)
	{
		input.onValueChanged.RemoveAllListeners();
		input.onValueChanged.AddListener(func);
	}

	public static void SetNormalizedValueWithoutNotify(this Slider slider, float normalized)
	{
		float minValue = slider.minValue;
		float realValue = normalized * (slider.maxValue - minValue) + minValue;
		slider.SetValueWithoutNotify(realValue);
	}

	public static Transform RandomChild(this Transform transform)
	{
		return transform.GetChild(Random.Range(0, transform.childCount));
	}

	public static Transform LastChild(this Transform transform)
	{
		return transform.GetChild(transform.childCount - 1);
	}

	public static Transform FirstChild(this Transform transform)
	{
		return transform.GetChild(0);
	}

	public static float Distance(this Transform v1, Transform v2)
	{
		return Vector3.Distance(v1.position, v2.position);
	}

	public static float Distance(this Transform v1, Vector3 v2)
	{
		return Vector3.Distance(v1.position, v2);
	}

	public static void SetStringText(this TextMeshProUGUI text, string value)
	{
		text.text = value.SplitCamelCase();
	}

	public static void SetBoolText(this TextMeshProUGUI text, bool value)
	{
		text.text = value ? "ON" : "OFF";
	}

	public static void SetPercentText(this TextMeshProUGUI text, float value)
	{
		text.text = $"{value * 100:0}%";
	}

	public static void SetNumberText(this TextMeshProUGUI text, float value)
	{
		text.text = $"{value:0}";
	}

	public static void StartColor(this ParticleSystem particleSystem, Color color)
	{
		ParticleSystem.MainModule main = particleSystem.main;
		main.startColor = color;
	}

	public static void DestroyChildren(this Transform parent, int min = 0)
	{
		while (parent.childCount > min)
		{
			Transform child = parent.GetChild(min);
			child.SetParent(null);
			child.gameObject.Destroy();
		}
	}

	public static void DestroyChildrenImmediate(this Transform parent)
	{
		while (parent.childCount > 0)
		{
			Transform child = parent.GetChild(0);
			child.SetParent(null);
			Object.DestroyImmediate(child.gameObject);
		}
	}

	public static Transform[] GetChildren(this Transform transform)
	{
		Transform[] children = new Transform[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
		{
			children[i] = transform.GetChild(i);
		}

		return children;
	}

	public static Vector3 To(this Component t1, Component t2, out float distance)
	{
		Vector3 dir = t1.transform.position.To(t2.transform.position);
		distance = dir.magnitude;
		return dir;
	}

	public static void ShowRect(this ScrollRect instance, RectTransform child)
	{
		Canvas.ForceUpdateCanvases();
		Vector2 viewportLocalPosition = instance.viewport.localPosition;
		Vector2 childLocalPosition = child.localPosition;
		instance.content.localPosition = new Vector2(0 - (viewportLocalPosition.x + childLocalPosition.x),
			0 - (viewportLocalPosition.y + childLocalPosition.y));
	}

	public static void SnapTo(this ScrollRect scroller, RectTransform child)
	{
		Canvas.ForceUpdateCanvases();
		Vector3 contentPos = scroller.transform.InverseTransformPoint(scroller.content.position);
		Vector3 childPos = scroller.transform.InverseTransformPoint(child.position);
		Vector3 endPos = contentPos - childPos;
		endPos.y -= 540 / 2 - 32 / 2;
		endPos.x = 0;
		scroller.content.anchoredPosition = endPos;
	}

	public static void SelectButton(this Selectable selectable)
	{
		selectable.StartCoroutine(SelectButtonE(selectable));
	}

	private static IEnumerator SelectButtonE(Selectable selectable)
	{
		EventSystem.current.SetSelectedGameObject(null);
		yield return new WaitForEndOfFrame();
		if (selectable && selectable.isActiveAndEnabled && selectable.interactable)
		{
			if (selectable.gameObject && selectable.gameObject.activeInHierarchy)
			{
				selectable.Select();
			}
		}
	}

	public static void SetSelectableUp(this Selectable selectable, Selectable up)
	{
		Navigation nav = selectable.navigation;
		nav.selectOnUp = up;
		selectable.navigation = nav;
	}

	public static void SetSelectableDown(this Selectable selectable, Selectable down)
	{
		Navigation nav = selectable.navigation;
		nav.selectOnDown = down;
		selectable.navigation = nav;
	}

	public static void SetSelectableLeft(this Selectable selectable, Selectable left)
	{
		Navigation nav = selectable.navigation;
		nav.selectOnLeft = left;
		selectable.navigation = nav;
	}

	public static void SetSelectableRight(this Selectable selectable, Selectable right)
	{
		Navigation nav = selectable.navigation;
		nav.selectOnRight = right;
		selectable.navigation = nav;
	}

	public static Transform FindChildByName(this Transform parent, string name)
	{
		Transform[] kids = parent.GetComponentsInChildren<Transform>();
		return kids.FirstOrDefault(child => string.Equals(child.name, name, StringComparison.OrdinalIgnoreCase));
	}

	public static void SetStiffness(this WheelCollider wheelCollider, float mult)
	{
		WheelFrictionCurve forwardFriction = wheelCollider.forwardFriction;
		forwardFriction.stiffness = mult;
		wheelCollider.forwardFriction = forwardFriction;

		WheelFrictionCurve sidewaysFriction = wheelCollider.sidewaysFriction;
		sidewaysFriction.stiffness = mult;
		wheelCollider.sidewaysFriction = sidewaysFriction;
	}
}