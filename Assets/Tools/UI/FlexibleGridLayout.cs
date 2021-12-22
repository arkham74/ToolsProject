using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.UI.Extensions
{
	public class FlexibleGridLayout : LayoutGroup
	{
		public enum FitType
		{
			UNIFORM,
			WIDTH,
			HEIGHT,
			FIXED_ROWS,
			FIXED_COLUMNS
		}

		public int rows, columns;
		public FitType fitType;
		public Vector2 spacing;

		// protected override void OnValidate()
		// {
		// 	base.OnValidate();
		// 	RectOffset pad = padding;
		// 	rows = Mathf.Max(rows, 1);
		// 	columns = Mathf.Max(columns, 1);
		// 	spacing.x = Mathf.Max(spacing.x, 0);
		// 	spacing.y = Mathf.Max(spacing.y, 0);
		// 	pad.left = Mathf.Max(pad.left, 0);
		// 	pad.right = Mathf.Max(pad.right, 0);
		// 	pad.top = Mathf.Max(pad.top, 0);
		// 	pad.bottom = Mathf.Max(pad.bottom, 0);
		// }

		public override void SetLayoutHorizontal()
		{
			Calc();
		}

		public override void CalculateLayoutInputVertical()
		{
		}

		public override void SetLayoutVertical()
		{
		}

		private void Calc()
		{
			if (fitType == FitType.WIDTH || fitType == FitType.HEIGHT || fitType == FitType.UNIFORM)
			{
				float squareRoot = Mathf.Sqrt(rectChildren.Count);
				rows = Mathf.CeilToInt(squareRoot);
				columns = Mathf.CeilToInt(squareRoot);
			}

			switch (fitType)
			{
				case FitType.WIDTH:
				case FitType.FIXED_COLUMNS:
					rows = Mathf.CeilToInt(rectChildren.Count / (float) columns);
					break;
				case FitType.HEIGHT:
				case FitType.FIXED_ROWS:
					columns = Mathf.CeilToInt(rectChildren.Count / (float) rows);
					break;
			}

			Rect rect = rectTransform.rect;
			float parentWidth = rect.width;
			float parentHeight = rect.height;

			RectOffset pad = padding;
			float parentX = parentWidth / columns;
			float spacingX = spacing.x / columns * (columns - 1);
			float paddingLeft = pad.left / (float) columns;
			float paddingRight = pad.right / (float) columns;
			float cellWidth = parentX - spacingX - paddingLeft - paddingRight;

			float parentY = parentHeight / rows;
			float spacingY = spacing.y / rows * (rows - 1);
			float paddingTop = pad.top / (float) rows;
			float paddingBottom = pad.bottom / (float) rows;
			float cellHeight = parentY - spacingY - paddingTop - paddingBottom;

			for (int i = 0; i < rectChildren.Count; i++)
			{
				int rowCount = i / columns;
				int columnCount = i % columns;

				RectTransform item = rectChildren[i];

				float xPos = cellWidth * columnCount + spacing.x * columnCount + pad.left;
				float yPos = cellHeight * rowCount + spacing.y * rowCount + pad.top;

				SetChildAlongAxis(item, 0, xPos, cellWidth);
				SetChildAlongAxis(item, 1, yPos, cellHeight);
			}
		}
	}
}