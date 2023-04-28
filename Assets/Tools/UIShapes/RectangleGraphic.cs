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
	[RequireComponent(typeof(CanvasRenderer))]
	[AddComponentMenu("UI/Rectangle")]
	public class RectangleGraphic : MaskableGraphic
	{
		[SerializeField] private Texture2D texture;
		[SerializeField][Range(0, 1)] private float width = 1.0f;
		[SerializeField][Range(0, 1)] private float height = 1.0f;
		[SerializeField][Range(0, 1)] private float radius1 = 0.5f;
		[SerializeField][Range(0, 1)] private float radius2 = 0.5f;
		[SerializeField][Range(0, 1)] private float radius3 = 0.5f;
		[SerializeField][Range(0, 1)] private float radius4 = 0.5f;
		[SerializeField][Range(0, 1)] private float fill = 1.0f;

		public override Texture mainTexture => texture;

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1;
			canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord2;
			canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord3;

			Rect pixelAdjustedRect = GetPixelAdjustedRect();
			Vector4 vector = new Vector4(pixelAdjustedRect.x, pixelAdjustedRect.y, pixelAdjustedRect.x + pixelAdjustedRect.width, pixelAdjustedRect.y + pixelAdjustedRect.height);

			UIVertex vert = new UIVertex();
			vert.uv1 = new Vector4(width, height, fill, 0);
			vert.uv2 = new Vector4(radius1, radius2, radius3, radius4);
			vert.uv3 = new Vector4(pixelAdjustedRect.width, pixelAdjustedRect.height, 0, 0);
			vert.color = color;

			vh.Clear();

			vert.position = new Vector3(vector.x, vector.y);
			vert.uv0 = new Vector2(0f, 0f);
			vh.AddVert(vert);

			vert.position = new Vector3(vector.x, vector.w);
			vert.uv0 = new Vector2(0f, 1f);
			vh.AddVert(vert);

			vert.position = new Vector3(vector.z, vector.w);
			vert.uv0 = new Vector2(1f, 1f);
			vh.AddVert(vert);

			vert.position = new Vector3(vector.z, vector.y);
			vert.uv0 = new Vector2(1f, 0f);
			vh.AddVert(vert);

			vh.AddTriangle(0, 1, 2);
			vh.AddTriangle(2, 3, 0);
		}
	}
}