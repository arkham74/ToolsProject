using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Freya;
using TMPro;
using Text = TMPro.TextMeshProUGUI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace JD
{
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
			return component.transform.parent.gameObject.TryGetComponent(out result);
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
			go.gameObject.ChangeActive(false);
		}

		public static void EnableGameObject(this Component go)
		{
			go.gameObject.ChangeActive(true);
		}

		public static void SetActiveGameObject(this Component go, bool state)
		{
			go.gameObject.ChangeActive(state);
		}

		public static void Destroy(this Component component, float time = 0f)
		{
			if (component == null)
			{
				return;
			}

			if (time > 0)
			{
				Object.Destroy(component.gameObject, time);
			}
			else
			{
				Object.Destroy(component.gameObject);
			}
		}

		public static void DestroyComponent(this Component component, float time = 0f)
		{
			if (component == null)
			{
				return;
			}

			if (time > 0)
			{
				Object.Destroy(component, time);
			}
			else
			{
				Object.Destroy(component);
			}
		}

		public static Vector3 To(this Component t1, Component t2, out float distance)
		{
			Vector3 dir = t1.transform.position.To(t2.transform.position);
			distance = dir.magnitude;
			return dir;
		}

		public static Vector3 Average<T>(this IList<T> birds) where T : Component
		{
			Vector3 center = default;
			foreach (T bird in birds)
			{
				center += bird.transform.position;
			}

			center /= birds.Count;
			return center;
		}

		public static void GroupSetActive(this IEnumerable<Component> components, bool value)
		{
			foreach (Component item in components)
			{
				item.gameObject.SetActive(value);
			}
		}

		public static T Closest<T>(this IEnumerable<T> enumerable, Component target) where T : Component
		{
			T clos = null;
			float min = float.MaxValue;
			foreach (T item in enumerable)
			{
				float dist = Vector3.Distance(item.transform.position, target.transform.position);
				if (dist < min)
				{
					min = dist;
					clos = item;
				}
			}
			return clos;
		}
	}
}
