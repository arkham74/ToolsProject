using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using JD;
using Random = UnityEngine.Random;

namespace JD
{
	[AddComponentMenu("Shapes/Slider")]
	public class SliderGraphic : ShapeGraphic
	{
		[SerializeField][ColorUsage(true, true)] private Color fillColor = Color.white;
		[SerializeField] private Slider.Direction direction = Slider.Direction.LeftToRight;
		[SerializeField][Range(0, 1)] private float fill = 1.0f;

		public void SetFill(float fill) => this.fill = fill;

		protected override void OnPopulateVert(ref UIVertex vert, Rect pixelAdjustedRect)
		{
			canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1 | AdditionalCanvasShaderChannels.TexCoord2;
			vert.uv1 = new Vector4(pixelAdjustedRect.width, pixelAdjustedRect.height, fill, (float)direction);
			vert.uv2 = fillColor;
		}
	}
}