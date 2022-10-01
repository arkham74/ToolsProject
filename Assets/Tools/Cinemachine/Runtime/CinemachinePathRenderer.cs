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
	public class CinemachinePathRenderer : MonoBehaviour
	{
		[SerializeField] [Min(2)] private int resolution = 10;
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
				int samples = resolution + 1;
				// Mathf.RoundToInt(path.PathLength * path.m_Resolution);
				lineRenderer.loop = path.Looped;
				lineRenderer.positionCount = samples;
				lineRenderer.startWidth = path.m_Appearance.width;
				lineRenderer.endWidth = path.m_Appearance.width;
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
