using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

public static class GameObjectExtensions
{
	// public static T FindObjectOfType<T>(bool includeInactive) where T : Object
	// {

	// }

	public static void SetChildrenActive(this GameObject go, bool value)
	{
		foreach (Transform child in go.transform)
		{
			child.gameObject.SetActive(value);
		}
	}

	public static IEnumerable<GameObject> GetChildren(this GameObject go)
	{
		return Enumerable.Range(0, go.transform.childCount).Select(e => go.transform.GetChild(e).gameObject);
	}

	public static void SetLayerRecursively(this GameObject go, string layerName)
	{
		SetLayerRecursively(go, LayerMask.NameToLayer(layerName));
	}

	public static void SetLayerRecursively(this GameObject go, int layerId)
	{
		go.layer = layerId;
		foreach (Transform child in go.transform)
		{
			SetLayerRecursively(child.gameObject, layerId);
		}
	}

	public static void SetLayerToAllChildren(this GameObject go, string layerName)
	{
		SetLayerToAllChildren(go, LayerMask.NameToLayer(layerName));
	}

	public static void SetLayerToAllChildren(this GameObject go, int layerId)
	{
		foreach (Transform child in go.GetComponentsInChildren<Transform>(true))
		{
			child.gameObject.layer = layerId;
		}
	}

	public static void SetTagRecursively(this Component go, string tag)
	{
		go.tag = tag;
		foreach (Transform transform in go.transform)
		{
			SetTagRecursively(transform, tag);
		}
	}
}