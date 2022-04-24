public static class Array2DExtensions
{
	public static T Random<T>(this T[,] array)
	{
		int l1 = array.GetLength(0);
		int l2 = array.GetLength(1);
		int x = UnityEngine.Random.Range(0, l1);
		int y = UnityEngine.Random.Range(0, l2);
		return array[x, y];
	}

	public static T AtOrDefault<T>(this T[,] array, int i, T def = default)
	{
		(int x, int y) = Tools.Map1DTo2D(i, array.GetLength(0));
		return array.AtOrDefault(x, y, def);
	}

	public static T AtOrDefault<T>(this T[,] array, int x, int y, T def = default)
	{
		if (x >= 0 && x < array.GetLength(0))
		{
			if (y >= 0 && y < array.GetLength(1)) return array[x, y];
		}

		return def;
	}
}
