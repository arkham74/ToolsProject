using System;
using System.Linq;
using Freya;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace JD
{
	public static class TransformExtensions
	{
		/// <summary>
		/// Transforms Vector2 position from world space to local space.
		/// </summary>
		public static Vector2 WorldToLocal(this Transform transform, Vector2 world)
		{
			return transform.InverseTransformPoint(world);
		}

		/// <summary>
		/// Transforms Vector2 position from local space to world space.
		/// </summary>
		public static Vector2 LocalToWorld(this Transform transform, Vector2 local)
		{
			return transform.TransformPoint(local);
		}

		/// <summary>
		/// Transforms Vector3 position from world space to local space.
		/// </summary>
		public static Vector3 WorldToLocal(this Transform transform, Vector3 world)
		{
			return transform.InverseTransformPoint(world);
		}

		/// <summary>
		/// Transforms Vector3 position from local space to world space.
		/// </summary>
		public static Vector3 LocalToWorld(this Transform transform, Vector3 local)
		{
			return transform.TransformPoint(local);
		}

		public static Transform GetSibling(this Transform transform, int offset = 1)
		{
			int index = transform.GetSiblingIndex() + offset;
			int lastIndex = transform.parent.childCount - 1;
			int siblingIndex = Mathf.Clamp(index, 0, lastIndex);
			return transform.parent.GetChild(siblingIndex);
		}

		public static Transform GetSiblingLoop(this Transform transform, int offset = 1)
		{
			int index = transform.GetSiblingIndex() + offset;
			int siblingIndex = Mathfs.Mod(index, transform.parent.childCount);
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

		public static void SetLocalX(this Transform t, float x)
		{
			Vector3 p = t.localPosition;
			p.x = x;
			t.localPosition = p;
		}

		public static void SetLocalY(this Transform t, float y)
		{
			Vector3 p = t.localPosition;
			p.y = y;
			t.localPosition = p;
		}

		public static void SetLocalZ(this Transform t, float z)
		{
			Vector3 p = t.localPosition;
			p.z = z;
			t.localPosition = p;
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
}