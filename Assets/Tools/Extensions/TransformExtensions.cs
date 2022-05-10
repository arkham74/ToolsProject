using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public static class TransformExtensions
{
	public static Transform GetSibling(this Transform transform, int offset = 1)
	{
		int index = transform.GetSiblingIndex() + offset;
		int lastIndex = transform.parent.childCount - 1;
		int siblingIndex = Mathf.Clamp(index, 0, lastIndex);
		return transform.parent.GetChild(siblingIndex);
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

	public static void DestroyChildren(this Transform parent, int min = 0)
	{
		while (parent.childCount > min)
		{
			Transform child = parent.GetChild(min);
			child.SetParent(null);
			Object.Destroy(child.gameObject);
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

	public static Transform FindChildByName(this Transform parent, string name)
	{
		Transform[] kids = parent.GetComponentsInChildren<Transform>();
		return kids.FirstOrDefault(child => string.Equals(child.name, name, StringComparison.OrdinalIgnoreCase));
	}

	public static Transform GetLastChild(this Transform transform)
	{
		return transform.GetChild(transform.childCount - 1);
	}

	public static Transform GetFirstChild(this Transform transform)
	{
		return transform.GetChild(0);
	}
}
