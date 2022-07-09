using UnityEngine;
using UnityEngine.UI;
#if TOOLS_NAUATTR
using NaughtyAttributes;
#endif

namespace CustomTools
{
	public class ColorBlockSync : MonoBehaviour
	{
		public Selectable selectable;
#if TOOLS_NAUATTR
		[Expandable]
#endif
		public ColorBlockData data;

#if UNITY_EDITOR
		private void Reset()
		{
			selectable = GetComponent<Selectable>();
			data = AssetTools.FindAssetByType<ColorBlockData>();
		}

		public void Sync()
		{
			selectable.colors = data.colorBlock;
			selectable.MarkDirty();
		}
#endif
	}
}