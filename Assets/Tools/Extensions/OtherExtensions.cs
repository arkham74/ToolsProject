using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

public static class OtherExtensions
{
	public static void MarkDirty(this Object obj)
	{
#if UNITY_EDITOR
		EditorUtility.SetDirty(obj);
#endif
	}

	public static bool CompareTypes(this Type a, Type b)
	{
		if (a.Equals(b))
			return true;

		if (a.IsSubclassOf(b))
			return true;

		if (b.IsSubclassOf(a))
			return true;

		return false;
	}

	public static Quaternion Inverse(this Quaternion q)
	{
		return Quaternion.Inverse(q);
	}

	public static Quaternion RotateToDirection(this Quaternion quaternion, Vector3 direction, float time)
	{
		return Quaternion.Lerp(quaternion, Quaternion.LookRotation(direction), time);
	}

	public static void SetBaseMap(this Material material, Texture texture)
	{
		material.SetTexture(Shader.PropertyToID("_BaseMap"), texture);
	}

	public static void SetBool(this Material material, string name, bool value)
	{
		material.SetInt(name, Convert.ToInt32(value));
	}

	public static Vector4 Evaluate(this AnimationCurve curve, Vector4 time)
	{
		return time.normalized * curve.Evaluate(time.magnitude);
	}

	public static Vector3 Evaluate(this AnimationCurve curve, Vector3 time)
	{
		return time.normalized * curve.Evaluate(time.magnitude);
	}

	public static Vector2 Evaluate(this AnimationCurve curve, Vector2 time)
	{
		return time.normalized * curve.Evaluate(time.magnitude);
	}

	public static Vector2 GetExtents(this Rect rect)
	{
		return rect.size / 2f;
	}

	public static Vector2Int GetExtents(this RectInt rect)
	{
		return rect.size / 2;
	}

	public static Vector2 Clamp(this Rect bounds, Vector3 pos)
	{
		return bounds.Clamp((Vector2)pos);
	}

	public static Vector2Int Clamp(this RectInt bounds, Vector3 pos)
	{
		return bounds.Clamp((Vector2)pos);
	}

	public static Vector2 Clamp(this Rect bounds, Vector2 pos)
	{
		float posX = Mathf.Clamp(pos.x, bounds.min.x, bounds.max.x);
		float posY = Mathf.Clamp(pos.y, bounds.min.y, bounds.max.y);
		return new Vector2(posX, posY);
	}

	public static Vector2Int Clamp(this RectInt bounds, Vector2 pos)
	{
		int posX = (int)Mathf.Clamp(pos.x, bounds.min.x, bounds.max.x);
		int posY = (int)Mathf.Clamp(pos.y, bounds.min.y, bounds.max.y);
		return new Vector2Int(posX, posY);
	}

	public static Vector3 Clamp(this Bounds bounds, Vector3 pos)
	{
		float posX = Mathf.Clamp(pos.x, bounds.min.x, bounds.max.x);
		float posY = Mathf.Clamp(pos.y, bounds.min.y, bounds.max.y);
		float posZ = Mathf.Clamp(pos.z, bounds.min.z, bounds.max.z);
		return new Vector3(posX, posY, posZ);
	}

	public static Vector3Int Clamp(this BoundsInt bounds, Vector3 pos)
	{
		int posX = (int)Mathf.Clamp(pos.x, bounds.min.x, bounds.max.x);
		int posY = (int)Mathf.Clamp(pos.y, bounds.min.y, bounds.max.y);
		int posZ = (int)Mathf.Clamp(pos.z, bounds.min.z, bounds.max.z);
		return new Vector3Int(posX, posY, posZ);
	}

	public static Vector2 Random(this Rect rect)
	{
		float randX = UnityEngine.Random.value * rect.width;
		float randY = UnityEngine.Random.value * rect.height;
		Vector2 rand = new Vector2(randX, randY);
		rand += rect.position;
		return rand;
	}

	public static bool CheckKeyPress(this KeyCode main, params KeyCode[] mod)
	{
		return Input.GetKeyDown(main) && mod.All(Input.GetKey);
	}

	public static void Destroy(this Object componentOrGameObject)
	{
		componentOrGameObject.Destroy(0);
	}

	public static void Destroy(this Object componentOrGameObject, float time)
	{
		if (componentOrGameObject is Transform transform)
			Object.Destroy(transform.gameObject, time);
		else
			Object.Destroy(componentOrGameObject, time);
	}

	public static T Get<T>(this VolumeProfile profile) where T : VolumeComponent
	{
		return profile.TryGet(out T component) ? component : default;
	}

	public static TimeSpan Epoch(this DateTime today)
	{
		DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return today.Subtract(epoch);
	}

	public static Vector3[] GetCorners(this Bounds bounds)
	{
		Vector3[] corners = new Vector3[8];
		corners[0] = bounds.min;
		corners[1] = bounds.max;
		corners[2] = new Vector3(corners[0].x, corners[0].y, corners[1].z);
		corners[3] = new Vector3(corners[0].x, corners[1].y, corners[0].z);
		corners[4] = new Vector3(corners[1].x, corners[0].y, corners[0].z);
		corners[5] = new Vector3(corners[0].x, corners[1].y, corners[1].z);
		corners[6] = new Vector3(corners[1].x, corners[0].y, corners[1].z);
		corners[7] = new Vector3(corners[1].x, corners[1].y, corners[0].z);
		return corners;
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
}