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
	[AddComponentMenu("Shapes/Rectangle")]
	public class RectangleGraphic : ShapeGraphic
	{
		[SerializeField][Range(0, 1)] private float width = 1.0f;
		[SerializeField][Range(0, 1)] private float height = 1.0f;
		[SerializeField][Range(0, 1)] private float radius1 = 0.5f;
		[SerializeField][Range(0, 1)] private float radius2 = 0.5f;
		[SerializeField][Range(0, 1)] private float radius3 = 0.5f;
		[SerializeField][Range(0, 1)] private float radius4 = 0.5f;
		[SerializeField][Range(0, 1)] private float fill = 1.0f;

		protected override void OnPopulateVert(ref UIVertex vert, Rect pixelAdjustedRect)
		{
			canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1 | AdditionalCanvasShaderChannels.TexCoord2;
			float aspect = pixelAdjustedRect.width / pixelAdjustedRect.height;
			vert.uv1 = new Vector4(width, height, fill, aspect);
			vert.uv2 = new Vector4(radius1, radius2, radius3, radius4);
		}
	}
}