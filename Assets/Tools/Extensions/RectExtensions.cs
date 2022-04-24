using UnityEngine;

public static class RectExtensions
{
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
}
