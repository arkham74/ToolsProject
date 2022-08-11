using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using UnityEditor.Rendering;
using System;
using Freya;

namespace JD.Splines
{
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

		// private const int samples = 10;
		// private float[] lut = new float[samples];

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

		public Segment[] Segments => segments;

		public float GetLength()
		{
			float len = 0;

			for (int i = 1; i < segments.Length; i++)
			{
				Segment seg1 = segments[i - 1];
				Segment seg2 = segments[i];
				len += seg1.point.Distance(seg2.point);
			}

			return len;
		}


		// public void CalcLUT()
		// {
		// 	for (int i = 1; i < samples; i++)
		// 	{
		// 		float t1 = (i - 1) / (samples - 1);
		// 		float t2 = i / (samples - 1);
		// 		Vector3 pos1 = Evaluate(t1);
		// 		Vector3 pos2 = Evaluate(t2);
		// 		float dist = pos1.Distance(pos2);
		// 		lut[i - 1] = dist;
		// 		Debug.LogWarning(dist);
		// 	}
		// }

		public Vector3 EvaluateNormalByDistance(float distance)
		{
			return EvaluateNormal(distance / GetLength());
		}

		public Vector3 EvaluateNormal(float t)
		{
			if (segments.Length > 0)
			{
				if (loop)
				{
					(int indexA, int indexB, float newT) = GetIndexesLoop(t);
					return Normal(segments[indexA], segments[indexB], newT);
				}
				else
				{
					(int indexA, int indexB, float newT) = GetIndexes(t);
					return Normal(segments[indexA], segments[indexB], newT);
				}
			}
			return Vector3.zero;
		}

		public Vector3 EvaluateByDistance(float distance)
		{
			return Evaluate(distance / GetLength());
		}

		public Vector3 Evaluate(float t)
		{
			if (segments.Length > 0)
			{
				if (loop)
				{
					(int indexA, int indexB, float newT) = GetIndexesLoop(t);
					return Bezier(segments[indexA], segments[indexB], newT);
				}
				else
				{
					(int indexA, int indexB, float newT) = GetIndexes(t);
					return Bezier(segments[indexA], segments[indexB], newT);
				}
			}
			return Vector3.zero;
		}

		private (int, int, float) GetIndexesLoop(float t)
		{
			t = t.Repeat(1f);
			int lines = segments.Length;
			float unNormalized = lines * t;
			int indexA = Mathf.FloorToInt(unNormalized).Clamp(0, lines - 1);
			int indexB = (indexA + 1) % (lines);
			float newT = unNormalized - indexA;
			return (indexA, indexB, newT);
		}

		private (int, int, float) GetIndexes(float t)
		{
			t = t.Clamp01();
			int lines = segments.Length - 1;
			float unNormalized = lines * t;
			int indexA = Mathf.FloorToInt(unNormalized);
			int indexB = Mathf.CeilToInt(unNormalized);
			float newT = unNormalized - indexA;
			return (indexA, indexB, newT);
		}

		private Vector3 Bezier(Segment s1, Segment s2, float t)
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

		private Vector3 Normal(Segment s1, Segment s2, float t)
		{
			Vector3 left1 = s1.left + s1.point;
			Vector3 right2 = s2.right + s2.point;
			return Normal(s1.point, left1, right2, s2.point, t);
		}

		public static Vector3 Normal(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			// float it = 1f - t;
			Vector3 a = ((-3 * t * t) + (6 * t) - 3) * p0;
			Vector3 b = ((9 * t * t) - (12 * t) + 3) * p1;
			Vector3 c = ((-9 * t * t) + (6 * t)) * p2;
			Vector3 d = 3 * t * t * p3;
			return a + b + c + d;
		}
	}
}