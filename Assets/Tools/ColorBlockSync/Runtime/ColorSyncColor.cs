using UnityEngine;
using UnityEngine.UI;

namespace JD
{
	[ExecuteAlways]
	public class ColorSyncColor : ColorSync<ColorDataColor>
	{
		[SerializeField] private Graphic graphic;

		private void Reset()
		{
			graphic = GetComponentInChildren<Graphic>(true);
		}
		
		public override void Apply()
		{
			if (graphic)
			{
				graphic.color = colorData;
			}
		}
	}
}