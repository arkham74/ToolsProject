#if TOOLS_CINEMACHINE
using Cinemachine;
using Freya;
using JD;
using NaughtyAttributes;
using Random = UnityEngine.Random;
using SebastianLague;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tag = NaughtyAttributes.TagAttribute;
using Text = TMPro.TextMeshProUGUI;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CordBot
{
	[ExecuteAlways]
	[RequireComponent(typeof(LineRenderer))]
	public class CinemachinePathLine : MonoBehaviour
	{
		[SerializeField] [Min(1)] private float samplesPerUnit = 20;
		[SerializeField] [Range(0, 1)] private float thickness = 0.05f;
		[SerializeField] private bool dynamic;
		[SerializeField] private CinemachinePathBase path;
		[SerializeField] private LineRenderer lineRenderer;

		private void Reset()
		{
			path = GetComponentInParent<CinemachinePathBase>();
			lineRenderer = GetComponentInParent<LineRenderer>();
		}

		private void Update()
		{
			if (dynamic || !Application.isPlaying)
			{
				int samples = Mathf.RoundToInt(path.PathLength * samplesPerUnit);
				lineRenderer.positionCount = samples;
				lineRenderer.startWidth = thickness;
				lineRenderer.endWidth = thickness;
				for (int i = 0; i < samples; i++)
				{
					float t = i / (samples - 1f);
					Vector3 position = path.EvaluatePositionAtUnit(t, CinemachinePathBase.PositionUnits.Normalized);
					lineRenderer.SetPosition(i, position);
				}
			}
		}
	}
}
#endif
