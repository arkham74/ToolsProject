using UnityEngine;
using UnityEngine.UI;

namespace UIExtensions
{
	public class FlexibleGridLayout : LayoutGroup
	{
		public enum FitType
		{
			Uniform,
			Width,
			Height,
			FixedRows,
			FixedColumns
		}

		[SerializeField] private GridLayoutGroup.Corner startCorner;
		[SerializeField] private GridLayoutGroup.Axis startAxis;
		[SerializeField] private Vector2 spacing;
		[SerializeField] private FitType fitType;
		[SerializeField][Min(1)] private int rowsColumns;

		public override void CalculateLayoutInputVertical()
		{
		}

		public override void SetLayoutHorizontal()
		{
			Calc();
		}

		public override void SetLayoutVertical()
		{
		}

		private void Calc()
		{
			int rows = rowsColumns;
			int columns = rowsColumns;

			if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
			{
				float squareRoot = Mathf.Sqrt(rectChildren.Count);
				rows = Mathf.CeilToInt(squareRoot);
				columns = Mathf.CeilToInt(squareRoot);
			}

			switch (fitType)
			{
				case FitType.Width:
				case FitType.FixedColumns:
					rows = Mathf.CeilToInt(rectChildren.Count / (float)columns);
					break;
				case FitType.Height:
				case FitType.FixedRows:
					columns = Mathf.CeilToInt(rectChildren.Count / (float)rows);
					break;
			}

			Rect rect = rectTransform.rect;
			float parentWidth = rect.width;
			float parentHeight = rect.height;

			RectOffset pad = padding;
			float parentX = parentWidth / columns;
			float spacingX = spacing.x / columns * (columns - 1);
			float paddingLeft = pad.left / (float)columns;
			float paddingRight = pad.right / (float)columns;
			float cellWidth = parentX - spacingX - paddingLeft - paddingRight;

			float parentY = parentHeight / rows;
			float spacingY = spacing.y / rows * (rows - 1);
			float paddingTop = pad.top / (float)rows;
			float paddingBottom = pad.bottom / (float)rows;
			float cellHeight = parentY - spacingY - paddingTop - paddingBottom;

			for (int i = 0; i < rectChildren.Count; i++)
			{
				int rowT = i / columns;
				int columnT = i % columns;

				switch (startCorner)
				{
					case GridLayoutGroup.Corner.UpperLeft: break;
					case GridLayoutGroup.Corner.UpperRight:
						switch (startAxis)
						{
							case GridLayoutGroup.Axis.Horizontal:
								columnT = 1 - columnT;
								break;
							case GridLayoutGroup.Axis.Vertical:
								rowT = 1 - rowT;
								break;
						}
						break;
					case GridLayoutGroup.Corner.LowerLeft:
						switch (startAxis)
						{
							case GridLayoutGroup.Axis.Horizontal:
								rowT = 1 - rowT;
								break;
							case GridLayoutGroup.Axis.Vertical:
								columnT = 1 - columnT;
								break;
						}
						break;
					case GridLayoutGroup.Corner.LowerRight:
						rowT = 1 - rowT;
						columnT = 1 - columnT;
						break;
				}


				RectTransform item = rectChildren[i];

				float xPos = cellWidth * columnT + spacing.x * columnT + pad.left;
				float yPos = cellHeight * rowT + spacing.y * rowT + pad.top;

				float xOffset = cellWidth * columns * (1f - 2f / columns);
				float yOffset = cellHeight * rows * (1f - 2f / rows);

				switch (startCorner)
				{
					case GridLayoutGroup.Corner.UpperLeft: break;
					case GridLayoutGroup.Corner.UpperRight:
						xPos += xOffset + spacing.x * (columns - 2);
						break;
					case GridLayoutGroup.Corner.LowerLeft:
						yPos += yOffset + spacing.y * (rows - 2);
						break;
					case GridLayoutGroup.Corner.LowerRight:
						xPos += xOffset + spacing.x * (columns - 2);
						yPos += yOffset + spacing.y * (rows - 2);
						break;
				}

				switch (startAxis)
				{
					case GridLayoutGroup.Axis.Horizontal:
						SetChildAlongAxis(item, 0, xPos, cellWidth);
						SetChildAlongAxis(item, 1, yPos, cellHeight);
						break;
					case GridLayoutGroup.Axis.Vertical:
						SetChildAlongAxis(item, 1, xPos, cellWidth);
						SetChildAlongAxis(item, 0, yPos, cellHeight);
						break;
				}
			}
		}
	}
}