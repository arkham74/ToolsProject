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
	[AddComponentMenu("Shapes/Squircle")]
	public class SquircleGraphic : ShapeGraphic
	{
		[SerializeField] private float radius = 16.0f;
		[SerializeField] private bool fill = true;
		[SerializeField] private float width = 4.0f;
		[SerializeField] private float scale = 1;

		public void SetScale(float scale) => this.scale = scale;
		public void SetFill(float fill) => this.width = fill;
		public void SetRadius(float radius) => this.radius = radius;

		protected override void OnPopulateVert(ref UIVertex vert, Rect pixelAdjustedRect)
		{
			if (!canvas) return;
			canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1 | AdditionalCanvasShaderChannels.TexCoord2;
			float width = pixelAdjustedRect.width;
			float height = pixelAdjustedRect.height;
			vert.uv1 = new Vector4(width, height, this.width, radius);
			vert.uv2 = new Vector4(scale, fill ? 0 : 1, 0, 0);
		}
	}
}