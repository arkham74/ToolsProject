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

[ExecuteAlways]
[RequireComponent(typeof(LineRenderer))]
public class Spline : MonoBehaviour
{
	[Serializable]
	private struct Segment
	{
		public Vector3 point;
		public Vector3 left;
		public Vector3 right;
	}

	[SerializeField][Min(1)] private int samples = 50;
	[SerializeField] private bool mirrorTangents = true;
	[SerializeField] private Segment[] segments = new Segment[2];

	[SerializeField] private LineRenderer lineRenderer;

	private void OnEnable()
	{
		if (lineRenderer == null)
			lineRenderer = GetComponent<LineRenderer>();

		if (lineRenderer == null)
			lineRenderer = gameObject.AddComponent<LineRenderer>();
	}

	// private void OnDisable()
	// {
	// 	Debug.LogWarning("OnDisable");
	// }

	private void LateUpdate()
	{
		lineRenderer.positionCount = samples;
		for (int i = 0; i < samples; i++)
		{
			float t = (float)i / samples;
			Vector3 pos = Evaluate(segments[0], segments[1], t);
			lineRenderer.SetPosition(i, pos);
		}
	}

	private Vector3 Evaluate(Segment s1, Segment s2, float t)
	{
		Vector3 left1 = s1.left + s1.point;
		Vector3 right2 = s2.right + s2.point;
		return Cubic(s1.point, left1, right2, s2.point, t);
	}

	private Vector3 Quad(Vector3 a, Vector3 b, Vector3 c, float t)
	{
		Vector3 start = Vector3.Lerp(a, b, t);
		Vector3 end = Vector3.Lerp(b, c, t);
		return Vector3.Lerp(start, end, t);
	}

	private Vector3 Cubic(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
	{
		Vector3 start = Quad(a, b, c, t);
		Vector3 end = Quad(b, c, d, t);
		return Vector3.Lerp(start, end, t);
	}
}