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
	[AddComponentMenu("Shapes/Star")]
	public class StarGraphic : ShapeGraphic
	{
		[SerializeField][Range(3, 12)] private int sides = 5;
		[SerializeField][Range(0, 2)] private float star = 0.6f;
		[SerializeField][Range(0, 1)] private float fill = 0.02f;
		[SerializeField] private bool empty = false;
		[SerializeField][Range(0, 1)] private float round = 0.1f;
		[SerializeField][Range(0, 1)] private float radius = 0.9f;

		public void SetSides(int sides) => this.sides = sides;
		public void SetStar(float star) => this.star = star;
		public void SetFill(float fill) => this.fill = fill;
		public void SetEmpty(bool empty) => this.empty = empty;
		public void SetRound(float round) => this.round = round;
		public void SetRadius(float radius) => this.radius = radius;

		protected override void OnPopulateVert(ref UIVertex vert, Rect pixelAdjustedRect)
		{
			if (!canvas) return;
			canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1 | AdditionalCanvasShaderChannels.TexCoord2;
			vert.uv1 = new Vector4(fill, round, radius, sides);
			vert.uv2 = new Vector4(star, Convert.ToSingle(empty), 0, 0);
		}
	}
}