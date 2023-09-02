using UnityEngine;
using UnityEngine.UI;

namespace JD
{
	[ExecuteAlways]
	public class ColorSyncColor : ColorSync<ColorDataColor>
	{
		[SerializeField] private Graphic graphic;

		public override void Apply()
		{
			graphic.color = colorData;
		}
	}
}