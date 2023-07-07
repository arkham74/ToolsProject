using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace JD
{
	public static class DrawTools
	{
		public static List<Vector2Int> DrawLine(Vector2Int start, Vector2Int end)
		{
			List<Vector2Int> list = new List<Vector2Int>();
			DrawLine(start, end, ref list);
			return list;
		}

		public static void DrawLine(Vector2Int start, Vector2Int end, ref List<Vector2Int> list)
		{
			list.Clear();

			int x0 = start.x;
			int y0 = start.y;

			int x1 = end.x;
			int y1 = end.y;

			int w = x1 - x0;
			int h = y1 - y0;
			int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
			if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
			if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
			if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
			int longest = Math.Abs(w);
			int shortest = Math.Abs(h);
			if (!(longest > shortest))
			{
				longest = Math.Abs(h);
				shortest = Math.Abs(w);
				if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
				dx2 = 0;
			}
			int numerator = longest >> 1;
			for (int i = 0; i <= longest; i++)
			{
				list.Add(new Vector2Int(x0, y0));
				numerator += shortest;
				if (!(numerator < longest))
				{
					numerator -= longest;
					x0 += dx1;
					y0 += dy1;
				}
				else
				{
					x0 += dx2;
					y0 += dy2;
				}
			}
		}
	}
}
