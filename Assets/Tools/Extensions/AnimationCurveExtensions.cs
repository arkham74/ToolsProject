using UnityEngine;

public static class AnimationCurveExtensions
{
	public static Vector2 Evaluate(this AnimationCurve curve, Vector2 time)
	{
		return time.normalized * curve.Evaluate(time.magnitude);
	}

	public static Vector3 Evaluate(this AnimationCurve curve, Vector3 time)
	{
		return time.normalized * curve.Evaluate(time.magnitude);
	}

	public static Vector4 Evaluate(this AnimationCurve curve, Vector4 time)
	{
		return time.normalized * curve.Evaluate(time.magnitude);
	}
}
