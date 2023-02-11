using UnityEngine;

namespace JD
{
	public static class RectExtensions
	{
		public static Vector2[] GetCorners(this Rect rect)
		{
			Vector2[] array = new Vector2[4];
			array[0] = rect.min;
			array[1] = rect.min + Vector2.right * rect.width;
			array[2] = rect.max;
			array[3] = rect.min + Vector2.up * rect.height;
			return array;
		}

		public static Vector2Int GetExtents(this RectInt rect)
		{
			return rect.size / 2;
		}

		public static Vector2Int Clamp(this RectInt bounds, Vector3 pos)
		{
			return bounds.Clamp((Vector2)pos);
		}

		public static Vector2Int Clamp(this RectInt bounds, Vector2 pos)
		{
			int posX = (int)Mathf.Clamp(pos.x, bounds.min.x, bounds.max.x);
			int posY = (int)Mathf.Clamp(pos.y, bounds.min.y, bounds.max.y);
			return new Vector2Int(posX, posY);
		}

		public static Vector2 GetExtents(this Rect rect)
		{
			return rect.size / 2f;
		}

		public static Vector2 Clamp(this Rect bounds, Vector3 pos)
		{
			return bounds.Clamp((Vector2)pos);
		}

		public static Vector2 Clamp(this Rect bounds, Vector2 pos)
		{
			float posX = Mathf.Clamp(pos.x, bounds.min.x, bounds.max.x);
			float posY = Mathf.Clamp(pos.y, bounds.min.y, bounds.max.y);
			return new Vector2(posX, posY);
		}

		public static Vector2 Random(this Rect rect)
		{
			float randX = UnityEngine.Random.value * rect.width;
			float randY = UnityEngine.Random.value * rect.height;
			Vector2 rand = new Vector2(randX, randY);
			rand += rect.position;
			return rand;
		}

		public static Rect OrderMinMax(this Rect rect)
		{
			if (rect.xMin > rect.xMax)
			{
				float num = rect.xMin;
				rect.xMin = rect.xMax;
				rect.xMax = num;
			}

			if (rect.yMin > rect.yMax)
			{
				float num2 = rect.yMin;
				rect.yMin = rect.yMax;
				rect.yMax = num2;
			}
			
			return rect;
		}
	}
}