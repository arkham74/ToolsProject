using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace JD
{
	public static class BoundsExtensions
	{
		public static Vector3 RandomPosition(this Bounds bounds)
		{
			float x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
			float y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
			float z = UnityEngine.Random.Range(bounds.min.z, bounds.max.z);
			return new Vector3(x, y, z);
		}

		public static Vector3 Clamp(this Bounds bounds, Vector3 pos)
		{
			float posX = Mathf.Clamp(pos.x, bounds.min.x, bounds.max.x);
			float posY = Mathf.Clamp(pos.y, bounds.min.y, bounds.max.y);
			float posZ = Mathf.Clamp(pos.z, bounds.min.z, bounds.max.z);
			return new Vector3(posX, posY, posZ);
		}

		public static Vector3[] GetCorners(this Bounds bounds)
		{
			Vector3[] array = new Vector3[8];
			Vector3 min = bounds.min;
			Vector3 max = bounds.max;
			array[0] = new Vector3(min.x, min.y, min.z);
			array[1] = new Vector3(min.x, min.y, max.z);
			array[2] = new Vector3(max.x, min.y, max.z);
			array[3] = new Vector3(max.x, min.y, min.z);
			array[4] = new Vector3(min.x, max.y, min.z);
			array[5] = new Vector3(min.x, max.y, max.z);
			array[6] = new Vector3(max.x, max.y, max.z);
			array[7] = new Vector3(max.x, max.y, min.z);
			return array;
		}

		public static Vector3 ClosestCorner(this Bounds bounds, Vector3 point)
		{
			float Distance(Vector3 corner)
			{
				return Vector3.Distance(corner, point);
			}

			float Height(Vector3 corner)
			{
				return corner.y;
			}

			return bounds.GetCorners().OrderBy(Distance).ThenBy(Height).First();
		}

		public static Vector3 ClosestPointFromInside(this Bounds bounds, Vector3 point)
		{
			if (bounds.Contains(point))
			{
				float xt = Mathf.InverseLerp(bounds.min.x, bounds.max.x, point.x);
				float yt = Mathf.InverseLerp(bounds.min.y, bounds.max.y, point.y);
				float zt = Mathf.InverseLerp(bounds.min.z, bounds.max.z, point.z);

				float rx = Mathf.Round(xt);
				float ry = Mathf.Round(yt);
				float rz = Mathf.Round(zt);

				float x = Mathf.Lerp(bounds.min.x, bounds.max.x, rx);
				float y = Mathf.Lerp(bounds.min.y, bounds.max.y, ry);
				float z = Mathf.Lerp(bounds.min.z, bounds.max.z, rz);

				float dx = Mathf.Abs(point.x - x);
				float dy = Mathf.Abs(point.y - y);
				float dz = Mathf.Abs(point.z - z);

				float min = Mathf.Min(dx, dy, dz);

				float tolerance = 0.01f;

				if (Math.Abs(dx - min) < tolerance)
				{
					point.x = x;
				}
				else if (Math.Abs(dy - min) < tolerance)
				{
					point.y = y;
				}
				else if (Math.Abs(dz - min) < tolerance)
				{
					point.z = z;
				}

				return point;
			}

			return point;
		}

		public static Vector3Int Clamp(this BoundsInt bounds, Vector3 pos)
		{
			int posX = (int)Mathf.Clamp(pos.x, bounds.min.x, bounds.max.x);
			int posY = (int)Mathf.Clamp(pos.y, bounds.min.y, bounds.max.y);
			int posZ = (int)Mathf.Clamp(pos.z, bounds.min.z, bounds.max.z);
			return new Vector3Int(posX, posY, posZ);
		}
	}
}