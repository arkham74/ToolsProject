using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

public static class OtherExtensions
{
	public static Quaternion Inverse(this Quaternion q)
	{
		return Quaternion.Inverse(q);
	}

	public static string AddSpaceBeforeCapital(this string e)
	{
		return string.Concat(e.Select(x => char.IsUpper(x) ? " " + x : x.ToString()));
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

	public static Vector3 Clamp(this Bounds bounds, Vector3 pos)
	{
		float posX = Mathf.Clamp(pos.x, bounds.min.x, bounds.max.x);
		float posY = Mathf.Clamp(pos.y, bounds.min.y, bounds.max.y);
		float posZ = Mathf.Clamp(pos.z, bounds.min.z, bounds.max.z);
		return new Vector3(posX, posY, posZ);
	}

	public static Vector3Int Clamp(this BoundsInt bounds, Vector3 pos)
	{
		int posX = (int) Mathf.Clamp(pos.x, bounds.min.x, bounds.max.x);
		int posY = (int) Mathf.Clamp(pos.y, bounds.min.y, bounds.max.y);
		int posZ = (int) Mathf.Clamp(pos.z, bounds.min.z, bounds.max.z);
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
}