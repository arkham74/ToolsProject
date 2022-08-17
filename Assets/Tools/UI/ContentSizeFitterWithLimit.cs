using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JD
{
	[RequireComponent(typeof(RectTransform))]
	[ExecuteAlways]
	public class ContentSizeFitterWithLimit : ContentSizeFitter
	{
		[SerializeField] private bool limitWidth;
		[SerializeField] private bool limitHeight;
		[SerializeField] private float maxWidth = 500f;
		[SerializeField] private float maxHeight = 500f;

		public bool LimitWidth
		{
			get
			{
				return limitWidth;
			}
			set
			{
				limitWidth = value;
				ForceRebuild();
			}
		}

		public float MaxWidth
		{
			get
			{
				return maxWidth;
			}
			set
			{
				maxWidth = value;
				ForceRebuild();
			}
		}

		public bool LimitHeight
		{
			get
			{
				return limitHeight;
			}
			set
			{
				limitHeight = value;
				ForceRebuild();
			}
		}

		public float MaxHeight
		{
			get
			{
				return maxHeight;
			}
			set
			{
				maxHeight = value;
				ForceRebuild();
			}
		}

		private RectTransform rect;

		public void ForceRebuild()
		{
			SetLayoutHorizontal();
			SetLayoutVertical();
		}

		public override void SetLayoutHorizontal()
		{
			base.SetLayoutHorizontal();
			RefreshSize();
		}

		public override void SetLayoutVertical()
		{
			base.SetLayoutVertical();
			RefreshSize();
		}

		public void RefreshSize()
		{
			if (rect == null)
			{
				rect = GetComponent<RectTransform>();
			}

			if (rect != null)
			{
				if (limitWidth && rect.rect.width > maxWidth)
				{
					RefreshWidth();
				}

				if (limitHeight && rect.rect.height > maxHeight)
				{
					RefreshHeight();
				}
			}
		}

		public void RefreshWidth()
		{
			if (rect != null)
			{
				rect.sizeDelta = new Vector2(maxWidth, rect.sizeDelta.y);
			}
		}

		public void RefreshHeight()
		{
			if (rect != null)
			{
				rect.sizeDelta = new Vector2(rect.sizeDelta.x, maxHeight);
			}
		}

	}
}