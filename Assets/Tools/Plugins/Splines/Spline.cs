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
using System;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class Spline : MonoBehaviour
{
	[Serializable]
	public struct Segment
	{
		public Vector3 point;
		public Vector3 left;
		public Vector3 right;
	}

#pragma warning disable CS0414
	[SerializeField] private bool mirrorTangents = true;
	[SerializeField] private bool loop = false;
#pragma warning restore CS0414
	[SerializeField]
	private Segment[] segments = new Segment[]
	{
		new Segment()
		{
			point = new Vector3(-1,0,0),
			left = new Vector3(-1,0,0),
			right = new Vector3(1,0,0)
		},
		new Segment()
		{
			point = new Vector3(1,0,0),
			left = new Vector3(-1,0,0),
			right = new Vector3(1,0,0)
		}
	};

	public Vector3 Evaluate(float t)
	{
		if (segments.Length > 0)
		{
			if (loop)
			{
				t = t.Repeat(1f);
				int lines = segments.Length;
				float unNormalized = lines * t;
				int indexA = Mathf.FloorToInt(unNormalized).Clamp(0, lines - 1);
				int indexB = (indexA + 1) % (lines);
				float newT = unNormalized - indexA;
				return Cubic(segments[indexA], segments[indexB], newT);
			}
			else
			{
				t = t.Clamp01();
				int lines = segments.Length - 1;
				float unNormalized = lines * t;
				int indexA = Mathf.FloorToInt(unNormalized);
				int indexB = Mathf.CeilToInt(unNormalized);
				float newT = unNormalized - indexA;
				return Cubic(segments[indexA], segments[indexB], newT);
			}
		}
		return Vector3.zero;
	}

	private Vector3 Cubic(Segment s1, Segment s2, float t)
	{
		Vector3 left1 = s1.left + s1.point;
		Vector3 right2 = s2.right + s2.point;
		return Bezier(s1.point, left1, right2, s2.point, t);
	}

	public static Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		float it = 1f - t;
		Vector3 a = it * it * it * p0;
		Vector3 b = 3 * it * it * t * p1;
		Vector3 c = 3 * it * t * t * p2;
		Vector3 d = t * t * t * p3;
		return a + b + c + d;
	}
}