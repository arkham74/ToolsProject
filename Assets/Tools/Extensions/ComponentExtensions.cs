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
		float realValue = Mathf.Lerp(slider.minValue, slider.maxValue, normalized);
		slider.SetValueWithoutNotify(realValue);
	}

	public static Transform RandomChild(this Transform transform)
	{
		return transform.GetChild(Random.Range(0, transform.childCount));
	}

	public static float Distance(this Transform v1, Transform v2)
	{
		return Vector3.Distance(v1.position, v2.position);
	}

	public static float Distance(this Transform v1, Vector3 v2)
	{
		return Vector3.Distance(v1.position, v2);
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

	public static void DestroyChildrenImmediate(this Transform parent, int min = 0)
	{
		while (parent.childCount > min)
		{
			Transform child = parent.GetChild(min);
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

	public static Transform GetLastChild(this Transform transform)
	{
		return transform.GetChild(transform.childCount - 1);
	}

	public static Transform GetFirstChild(this Transform transform)
	{
		return transform.GetChild(0);
	}

	public static Transform GetPrevSibling(this Transform transform, int offset = 1)
	{
		int index = transform.GetSiblingIndex();
		return transform.parent.GetChild(index - offset);
	}

	public static Transform GetNextSibling(this Transform transform, int offset = 1)
	{
		int index = transform.GetSiblingIndex();
		return transform.parent.GetChild(index + offset);
	}
}