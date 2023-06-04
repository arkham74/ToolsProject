using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace JD
{
	[RequireComponent(typeof(CanvasRenderer))]
	public abstract class ShapeGraphic : MaskableGraphic
	{
		[SerializeField][ColorUsage(false, true)] private Color emission = Color.black;
		[SerializeField] private Sprite sourceImage;
		public override Texture mainTexture
		{
			get
			{
				if (sourceImage)
				{
					return sourceImage.texture;
				}
				return s_WhiteTexture;
			}
		}

		protected abstract void OnPopulateVert(ref UIVertex vert, Rect pixelAdjustedRect);

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord3;

			Rect pixelAdjustedRect = GetPixelAdjustedRect();
			Vector4 vector = new Vector4(pixelAdjustedRect.x, pixelAdjustedRect.y, pixelAdjustedRect.x + pixelAdjustedRect.width, pixelAdjustedRect.y + pixelAdjustedRect.height);

			UIVertex vert = new UIVertex();
			OnPopulateVert(ref vert, pixelAdjustedRect);
			vert.color = color;
			emission.a = 0;
			vert.uv3 = emission;

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