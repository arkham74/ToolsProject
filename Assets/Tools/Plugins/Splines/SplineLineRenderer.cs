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
using UnityEngine.Profiling;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

[RequireComponent(typeof(LineRenderer))]
public class SplineLineRenderer : SplineSampler
{
	[SerializeField] private LineRenderer lineRenderer;

	protected override void Reset()
	{
		base.Reset();
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.useWorldSpace = false;
		lineRenderer.startWidth = 0.1f;
		lineRenderer.endWidth = 0.1f;
		lineRenderer.sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Particles/Unlit"));
	}

	protected override void Positions(Span<Vector3> positions)
	{
		lineRenderer.positionCount = samples;
		for (int i = 0; i < positions.Length; i++)
		{
			lineRenderer.SetPosition(i, positions[i]);
		}
	}
}