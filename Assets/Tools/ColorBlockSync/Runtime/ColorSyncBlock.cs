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

		public override void Apply()
		{
			selectable.colors = colorData;
		}
	}
}