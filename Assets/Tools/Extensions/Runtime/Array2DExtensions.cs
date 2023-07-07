using UnityEngine;

namespace JD
{
	public static class Array2DExtensions
	{
		public static T Get<T>(this T[,] grid, Vector2Int index)
		{
			return grid[index.x, index.y];
		}

		public static void Set<T>(this T[,] grid, Vector2Int index, T type)
		{
			grid[index.x, index.y] = type;
		}

		public static int Map2DTo1D<T>(this T[,] grid, int x, int y)
		{
			int width = grid.GetLength(0);
			return x + width * y;
		}

		public static (int, int) Map1DTo2D<T>(this T[,] grid, int i)
		{
			int width = grid.GetLength(0);
			return (i % width, i / width);
		}

		public static T At1D<T>(this T[,] array, int index)
		{
			(int x, int y) = IndexTools.Map1DTo2D(index, array.GetLength(0));
			return array[x, y];
		}

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
			(int x, int y) = IndexTools.Map1DTo2D(i, array.GetLength(0));
			return array.AtOrDefault(x, y, def);
		}

		public static T AtOrDefault<T>(this T[,] array, int x, int y, T def = default)
		{
			if (x >= 0 && x < array.GetLength(0))
			{
				if (y >= 0 && y < array.GetLength(1))
				{
					return array[x, y];
				}
			}

			return def;
		}

		public static void SafeSet<T>(this T[,] array, int x, int y, T value)
		{
			if (x >= 0 && x < array.GetLength(0))
			{
				if (y >= 0 && y < array.GetLength(1))
				{
					array[x, y] = value;
				}
			}
		}

		public static bool InRange<T>(this T[,] array, int x, int y)
		{
			int w = array.GetLength(0);
			int h = array.GetLength(1);
			return x >= 0 && x < w && y >= 0 && y < h;
		}

		public static bool InRange<T>(this T[,] array, Vector2Int index)
		{
			int w = array.GetLength(0);
			int h = array.GetLength(1);
			return index.x >= 0 && index.x < w && index.y >= 0 && index.y < h;
		}
	}
}