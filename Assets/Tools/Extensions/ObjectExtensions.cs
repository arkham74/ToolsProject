using UnityEditor;
using Object = UnityEngine.Object;

namespace JD
{
	public static class ObjectExtensions
	{
		public static void MarkDirty(this Object obj)
		{
#if UNITY_EDITOR
			EditorUtility.SetDirty(obj);
#endif
		}

		public static void Destroy(this Object obj, float time)
		{
			Object.Destroy(obj, time);
		}

		public static void Destroy(this Object obj)
		{
			Object.Destroy(obj);
		}
	}
}
