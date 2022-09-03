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

		public static void Destroy(this Object obj, float time = 0f)
		{
			if (time > 0)
			{
				Object.Destroy(obj, time);
			}
			else
			{
				Object.Destroy(obj);
			}
		}
	}
}
