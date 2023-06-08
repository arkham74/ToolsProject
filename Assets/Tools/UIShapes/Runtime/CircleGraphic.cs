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
	[AddComponentMenu("Shapes/Circle")]
	public class CircleGraphic : ShapeGraphic
	{
		[SerializeField][Range(0, 1)] private float radius = 1.0f;
		[SerializeField][Range(0, 1)] private float fill = 1.0f;

		public void SetRadius(float radius) => this.radius = radius;
		public void SetFill(float fill) => this.fill = fill;

		protected override void OnPopulateVert(ref UIVertex vert, Rect pixelAdjustedRect)
		{
			canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1;
			vert.uv1 = new Vector4(radius, fill, 0, 0);
		}
	}
}