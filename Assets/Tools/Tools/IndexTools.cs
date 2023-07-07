namespace JD
{
	public static class IndexTools
	{
		public static int Map2DTo1D(int x, int y, int width)
		{
			return x + width * y;
		}

		public static (int, int) Map1DTo2D(int i, int width)
		{
			return (i % width, i / width);
		}
	}
}
