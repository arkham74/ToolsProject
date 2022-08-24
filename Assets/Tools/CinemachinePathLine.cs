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
		[SerializeField] private bool dynamic;
		[SerializeField] private CinemachinePathBase path;
		[SerializeField] private LineRenderer lineRenderer;
		[SerializeField][Min(1)] private float samplesPerUnit = 20;

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
