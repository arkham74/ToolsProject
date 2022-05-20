using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using TMPro;
using NaughtyAttributes;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tag = NaughtyAttributes.TagAttribute;
using UnityEditor.Rendering;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class Spline : MonoBehaviour
{
	[Serializable]
	private struct Segment
	{
		public Vector3 point;
		public Vector3 left;
		public Vector3 right;
	}

#pragma warning disable CS0414
	[SerializeField] private bool mirrorTangents = true;
#pragma warning restore CS0414
	[SerializeField] private Segment[] segments = new Segment[2];

	public Vector3 Evaluate(float t)
	{
		if (segments.Length > 0)
		{
			int lines = segments.Length - 1;
			float unNormalized = lines * t;
			int indexA = Mathf.FloorToInt(unNormalized);
			int indexB = Mathf.CeilToInt(unNormalized);
			float newT = unNormalized - indexA;
			return Cubic(segments[indexA], segments[indexB], newT);
		}
		return Vector3.zero;
	}

	private Vector3 Cubic(Segment s1, Segment s2, float t)
	{
		Vector3 left1 = s1.left + s1.point;
		Vector3 right2 = s2.right + s2.point;
		return Cubic(s1.point, left1, right2, s2.point, t);
	}

	private Vector3 Cubic(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
	{
		Vector3 start = Quad(a, b, c, t);
		Vector3 end = Quad(b, c, d, t);
		return Vector3.Lerp(start, end, t);
	}

	private Vector3 Quad(Vector3 a, Vector3 b, Vector3 c, float t)
	{
		Vector3 start = Vector3.Lerp(a, b, t);
		Vector3 end = Vector3.Lerp(b, c, t);
		return Vector3.Lerp(start, end, t);
	}
}