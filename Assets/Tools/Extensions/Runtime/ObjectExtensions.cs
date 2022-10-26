using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JD
{
	public static class ObjectExtensions
	{
		[Conditional("UNITY_EDITOR")]
		public static void MarkDirty(this Object target)
		{
#if UNITY_EDITOR
			EditorUtility.SetDirty(target);
#endif
		}

		public static void Destroy(this Object target, float time)
		{
			Object.Destroy(target, time);
		}

		public static void Destroy(this Object target)
		{
			Object.Destroy(target);
		}

		public static void DestroyImmediate(this Object target, bool allowDestroyingAssets)
		{
			Object.DestroyImmediate(target, allowDestroyingAssets);
		}

		public static void DestroyImmediate(this Object target)
		{
			Object.DestroyImmediate(target);
		}

		public static void DestroyImmediateWhenNotPlaying(this Object target)
		{
			if (Application.isPlaying)
			{
				Object.Destroy(target);
			}
			else
			{
				Object.DestroyImmediate(target);
			}
		}

		public static void DontDestroyOnLoad(this Object target)
		{
			Object.DontDestroyOnLoad(target);
		}

		public static T Instantiate<T>(this T target, Transform parent) where T : Object
		{
			return Object.Instantiate<T>(target, parent);
		}

		public static T Instantiate<T>(this T target, Vector3 position, Quaternion rotation) where T : Object
		{
			return Object.Instantiate<T>(target, position, rotation);
		}

		public static T Instantiate<T>(this T target, Vector3 position, Quaternion rotation, Transform parent) where T : Object
		{
			return Object.Instantiate<T>(target, position, rotation, parent);
		}
	}
}
