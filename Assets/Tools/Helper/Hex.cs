using System;
using System.Collections.Generic;
using System.Linq;
using Freya;
using UnityEngine;

namespace JD
{
	public static class Hex
	{
		private const float SQRT3 = 1.732050807568877293527446341505872366942805253810380628055806f;

		public static Vector2 HexToWorld(Vector2 hex, float radius)
		{
			return HexToWorld(hex, Vector2.one * radius);
		}

		public static Vector2 HexToWorld(Vector2 hex, Vector2 radius)
		{
			return HexToWorld((Vector3)hex, radius);
		}

		public static Vector2 HexToWorld(Vector3 hex, float radius)
		{
			return HexToWorld(hex, Vector2.one * radius);
		}

		public static Vector2 HexToWorld(Vector3 hex, Vector2 radius)
		{
			float x = radius.x * (SQRT3 * hex.x + SQRT3 / 2f * hex.y);
			float y = radius.y * (3f / 2f * hex.y);
			return new Vector2(x, y);
		}

		public static Vector3 WorldToHex(Vector2 point, float radius)
		{
			return WorldToHex(point, Vector2.one * radius);
		}

		public static Vector3 WorldToHex(Vector2 point, Vector2 radius)
		{
			float q = SQRT3 / 3f * (point.x / radius.x) - 1f / 3f * (point.y / radius.y);
			float r = 2f / 3f * (point.y / radius.y);
			return new Vector3(q, r, -q - r);
		}

		public static Vector2Int HexToOffset(Vector2Int hex)
		{
			return HexToOffset((Vector3Int)hex);
		}

		public static Vector2Int HexToOffset(Vector3Int hex)
		{
			int col = hex.x + (hex.y + (hex.y & 1)) / 2;
			int row = hex.y;
			return new Vector2Int(col, row);
		}

		public static Vector2Int OffsetToHex(Vector2Int hex)
		{
			return OffsetToHex((Vector3Int)hex);
		}

		public static Vector2Int OffsetToHex(Vector3Int hex)
		{
			int q = hex.x - (hex.y + (hex.y & 1)) / 2;
			int r = hex.y;
			return new Vector2Int(q, r);
		}

		public static Vector3Int RoundToInt(Vector2 hex)
		{
			return Round(CalcS(hex)).RoundToInt();
		}

		public static Vector3Int RoundToInt(Vector3 hex)
		{
			return Round(hex).RoundToInt();
		}

		public static Vector3 Round(Vector3 hex)
		{
			float q = Mathf.Round(hex.x);
			float r = Mathf.Round(hex.y);
			float s = Mathf.Round(hex.z);

			float qDiff = Mathf.Abs(q - hex.x);
			float rDiff = Mathf.Abs(r - hex.y);
			float sDiff = Mathf.Abs(s - hex.z);

			if (qDiff > rDiff && qDiff > sDiff)
			{
				q = -r - s;
			}
			else if (rDiff > sDiff)
			{
				r = -q - s;
			}
			else
			{
				s = -q - r;
			}

			return new Vector3(q, r, s);
		}

		public static Vector3Int[] GetNeighboursHex(Vector3Int start)
		{
			return new Vector3Int[6]
			{
				CalcS(new Vector2Int(1, 0)) + start,
				CalcS(new Vector2Int(-1, 0)) + start,
				CalcS(new Vector2Int(0, 1)) + start,
				CalcS(new Vector2Int(0, -1)) + start,
				CalcS(new Vector2Int(1, -1)) + start,
				CalcS(new Vector2Int(-1, 1)) + start,
			};
		}

		public static float Distance(Vector2 start, Vector2 end)
		{
			return Distance(CalcS(start), CalcS(end));
		}

		public static float Distance(Vector3 start, Vector3 end)
		{
			Vector3 vec = start - end;
			float x = Mathf.Abs(vec.x);
			float y = Mathf.Abs(vec.y);
			float z = Mathf.Abs(vec.z);
			return (x + y + z) / 2f;
		}

		public static Vector3Int CalcS(Vector3Int hex)
		{
			return CalcS((Vector3)hex).RoundToInt();
		}

		public static Vector3Int CalcS(Vector2Int hex)
		{
			return CalcS((Vector2)hex).RoundToInt();
		}

		public static Vector3 CalcS(Vector2 hex)
		{
			Vector3 cube = (Vector3)hex;
			cube.z = -hex.x - hex.y;
			return cube;
		}
	}
}
