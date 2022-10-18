using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JD
{
	public static class GameObjectExtensions
	{
		public static bool TryGetComponentInChildren<T>(this GameObject gameObject, out T result) where T : Component
		{
			return result = gameObject.GetComponentInChildren<T>();
		}

		public static bool TryGetComponentInParent<T>(this GameObject gameObject, out T result) where T : Component
		{
			if (gameObject.TryGetComponent(out result))
			{
				return true;
			}

			if (gameObject.transform.parent.TryGetComponent(out result))
			{
				return true;
			}
			return false;
		}

		public static bool CompareTags(this GameObject gameObject, params string[] tags)
		{
			foreach (string tag in tags)
			{
				if (gameObject.CompareTag(tag))
				{
					return true;
				}
			}

			return false;
		}

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

		public static void SetLayerToAllChildren(this GameObject go, string layerName)
		{
			SetLayerToAllChildren(go, LayerMask.NameToLayer(layerName));
		}

		public static void SetLayerRecursively(this GameObject go, int layerId)
		{
			go.layer = layerId;
			foreach (Transform child in go.transform)
			{
				SetLayerRecursively(child.gameObject, layerId);
			}
		}

		public static void SetLayerToAllChildren(this GameObject go, int layerId)
		{
			foreach (Transform child in go.GetComponentsInChildren<Transform>(true))
			{
				child.gameObject.layer = layerId;
			}
		}

		public static void Disable(this GameObject go)
		{
			go.ChangeActive(false);
		}

		public static void Enable(this GameObject go)
		{
			go.ChangeActive(true);
		}

		public static void GroupSetActive(this IEnumerable<GameObject> components, bool value)
		{
			foreach (GameObject item in components)
			{
				item.ChangeActive(value);
			}
		}

		public static void ChangeActive(this GameObject gameObject, bool value)
		{
			if (gameObject.activeSelf != value)
			{
				gameObject.SetActive(value);
			}
		}
	}
}