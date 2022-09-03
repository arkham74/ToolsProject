// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// Credit Melang
// Sourced from - http://forum.unity3d.com/members/melang.593409/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UIExtensions
{
	//An outline that looks a bit nicer than the default one. It has less "holes" in the outline by drawing more copies of the effect
	[AddComponentMenu("UI/Effects/Extensions/Nicer Outline")]
	public class NicerOutline : BaseMeshEffect
	{
		[FormerlySerializedAs("m_EffectColor")][SerializeField] private Color effectColor = new Color(0f, 0f, 0f, 1.0f);
		[FormerlySerializedAs("m_EffectDistance")][SerializeField] private Vector2 effectDistance = new Vector2(1f, -1f);
		[FormerlySerializedAs("m_UseGraphicAlpha")][SerializeField] private bool useGraphicAlpha;

		private readonly List<UIVertex> mVerts = new List<UIVertex>();

		//
		// Properties
		//
		public Color EffectColor
		{
			get => effectColor;
			set
			{
				effectColor = value;
				if (graphic != null)
				{
					graphic.SetVerticesDirty();
				}
			}
		}

		public Vector2 EffectDistance
		{
			get => effectDistance;
			set
			{
				if (value.x > 600f)
				{
					value.x = 600f;
				}

				if (value.x < -600f)
				{
					value.x = -600f;
				}

				if (value.y > 600f)
				{
					value.y = 600f;
				}

				if (value.y < -600f)
				{
					value.y = -600f;
				}

				if (effectDistance == value)
				{
					return;
				}

				effectDistance = value;
				if (graphic != null)
				{
					graphic.SetVerticesDirty();
				}
			}
		}

		public bool UseGraphicAlpha
		{
			get => useGraphicAlpha;
			set
			{
				useGraphicAlpha = value;
				if (graphic != null)
				{
					graphic.SetVerticesDirty();
				}
			}
		}

		protected void ApplyShadowZeroAlloc(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			int neededCapacity = verts.Count * 2;
			if (verts.Capacity < neededCapacity)
			{
				verts.Capacity = neededCapacity;
			}

			for (int i = start; i < end; ++i)
			{
				UIVertex vt = verts[i];
				verts.Add(vt);

				Vector3 v = vt.position;
				v.x += x;
				v.y += y;
				vt.position = v;
				Color32 newColor = color;
				if (useGraphicAlpha)
				{
					newColor.a = (byte)(newColor.a * verts[i].color.a / 255);
				}

				vt.color = newColor;
				verts[i] = vt;
			}
		}

		protected void ApplyShadow(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			int neededCapacity = verts.Count * 2;
			if (verts.Capacity < neededCapacity)
			{
				verts.Capacity = neededCapacity;
			}

			ApplyShadowZeroAlloc(verts, color, start, end, x, y);
		}

		public override void ModifyMesh(VertexHelper vh)
		{
			if (!IsActive())
			{
				return;
			}

			mVerts.Clear();
			vh.GetUIVertexStream(mVerts);

			Text foundText = GetComponent<Text>();

			float bestFitAdjustment = 1f;

			if (foundText && foundText.resizeTextForBestFit)
			{
				bestFitAdjustment = (float)foundText.cachedTextGenerator.fontSizeUsedForBestFit /
														(foundText.resizeTextMaxSize - 1); //max size seems to be exclusive
			}

			float distanceX = EffectDistance.x * bestFitAdjustment;
			float distanceY = EffectDistance.y * bestFitAdjustment;

			int start = 0;
			int count = mVerts.Count;
			ApplyShadow(mVerts, EffectColor, start, mVerts.Count, distanceX, distanceY);
			start = count;
			count = mVerts.Count;
			ApplyShadow(mVerts, EffectColor, start, mVerts.Count, distanceX, -distanceY);
			start = count;
			count = mVerts.Count;
			ApplyShadow(mVerts, EffectColor, start, mVerts.Count, -distanceX, distanceY);
			start = count;
			count = mVerts.Count;
			ApplyShadow(mVerts, EffectColor, start, mVerts.Count, -distanceX, -distanceY);

			start = count;
			count = mVerts.Count;
			ApplyShadow(mVerts, EffectColor, start, mVerts.Count, distanceX, 0);
			start = count;
			count = mVerts.Count;
			ApplyShadow(mVerts, EffectColor, start, mVerts.Count, -distanceX, 0);

			start = count;
			count = mVerts.Count;
			ApplyShadow(mVerts, EffectColor, start, mVerts.Count, 0, distanceY);
			start = count;
			ApplyShadow(mVerts, EffectColor, start, mVerts.Count, 0, -distanceY);

			vh.Clear();
			vh.AddUIVertexTriangleStream(mVerts);
		}

		// protected override void OnValidate()
		// {
		// 	EffectDistance = effectDistance;
		// 	base.OnValidate();
		// }
	}
}
