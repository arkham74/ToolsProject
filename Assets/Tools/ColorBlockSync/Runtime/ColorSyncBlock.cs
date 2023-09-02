using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
#if TOOLS_NAUATTR
using NaughtyAttributes;
#endif

namespace JD
{
	[ExecuteAlways]
	public class ColorSyncBlock : ColorSync<ColorDataBlock>
	{
		[SerializeField] private Selectable selectable;

		protected void Reset()
		{
			colorData = AssetTools.FindAssetByType<ColorDataBlock>();
			selectable = GetComponentInChildren<Selectable>(true);
		}

		public override void Apply()
		{
			if (selectable)
			{
				selectable.colors = colorData;
			}
		}
	}
}