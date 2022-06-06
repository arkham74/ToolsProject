using System;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public static class ComponentExtensions
{
	public static void SetTagRecursively(this Component go, string tag)
	{
		go.tag = tag;
		foreach (Transform transform in go.transform)
		{
			SetTagRecursively(transform, tag);
		}
	}

	public static bool TryGetComponentInChildren<T>(this Component component, out T result) where T : Component
	{
		return result = component.GetComponentInChildren<T>();
	}

	public static bool TryGetComponentInParent<T>(this Component component, out T result) where T : Component
	{
		return result = component.GetComponentInParent<T>();
	}

	public static bool CompareTags(this Component component, params string[] tags)
	{
		foreach (string tag in tags)
		{
			if (component.CompareTag(tag))
			{
				return true;
			}
		}

		return false;
	}

	public static float Distance(this Component a, Component b)
	{
		return Vector3.Distance(a.transform.position, b.transform.position);
	}

	public static Vector3 To(this Component s, Component t)
	{
		return s.transform.position.To(t.transform.position);
	}

	public static Vector3 DirTo(this Component s, Component t)
	{
		return s.transform.position.DirTo(t.transform.position);
	}

	public static void DisableGameObject(this Component go)
	{
		go.gameObject.SetActive(false);
	}

	public static void EnableGameObject(this Component go)
	{
		go.gameObject.SetActive(true);
	}

	public static void SetActiveGameObject(this Component go, bool state)
	{
		go.gameObject.SetActive(state);
	}

	public static void Destroy(this Component component, float time = 0f)
	{
		if (component == null) return;

		if (time > 0)
			Object.Destroy(@component.gameObject, time);
		else
			Object.Destroy(@component.gameObject);
	}

	public static void DestroyComponent(this Component component, float time = 0f)
	{
		if (component == null) return;

		if (time > 0)
			Object.Destroy(@component, time);
		else
			Object.Destroy(@component);
	}

	public static Vector3 To(this Component t1, Component t2, out float distance)
	{
		Vector3 dir = t1.transform.position.To(t2.transform.position);
		distance = dir.magnitude;
		return dir;
	}
}