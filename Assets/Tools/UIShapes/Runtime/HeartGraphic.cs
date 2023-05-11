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
	[AddComponentMenu("Shapes/Heart")]
	public class HeartGraphic : ShapeGraphic
	{
		[SerializeField][Range(0, 1)] private float fill = 1.0f;
		[SerializeField][Range(0, 1)] private float round = 1.0f;

		protected override void OnPopulateVert(ref UIVertex vert, Rect pixelAdjustedRect)
		{
			canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1;
			vert.uv1 = new Vector4(fill, round, 0, 0);
		}
	}
}