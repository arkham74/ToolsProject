using System.Text;
using UnityEngine;
using UnityEngine.Rendering;

#if TOOLS_LOCALIZATION
using UnityEngine.Localization.Settings;
#endif

namespace JD
{
	public static class Pools
	{
		private static ObjectPool<StringBuilder> sbPool = new ObjectPool<StringBuilder>(null, x => x.Clear());
		private static ObjectPool<MaterialPropertyBlock> mbpPool = new ObjectPool<MaterialPropertyBlock>(null, x => x.Clear());

		public static StringBuilder GetStringBuilder() => sbPool.Get();
		public static MaterialPropertyBlock GetMaterialPropertyBlock() => mbpPool.Get();

		public static void Release(MaterialPropertyBlock buffer) => mbpPool.Release(buffer);
		public static void Release(StringBuilder buffer) => sbPool.Release(buffer);
	}
}
