using System;
using UnityEngine;
using UnityEngine.UI;
#if TOOLS_NAUATTR
using NaughtyAttributes;
#endif

namespace JD
{
	public class ColorBlockSync : MonoBehaviour
	{
		public Selectable selectable;
		#if TOOLS_NAUATTR
		[Expandable]
		#endif
		public ColorBlockData data;

		private void Start()
		{
			Sync();
		}

		#if UNITY_EDITOR
		private void Reset()
		{
			selectable = GetComponent<Selectable>();
			data = AssetTools.FindAssetByType<ColorBlockData>();
		}
		#endif

		public void Sync()
		{
			if (selectable)
			{
				selectable.colors = data.colorBlock;
				selectable.MarkDirty();
			}
		}
	}
}