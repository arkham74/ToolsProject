#if TOOLS_CINEMACHINE
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using JD;
using Freya;
using Random = UnityEngine.Random;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD
{
	[ExecuteAlways]
	public class CinemachinePathPlacer : MonoBehaviour
	{
		[SerializeField] private CinemachinePathBase path;
		[SerializeField] private bool rotate;

		private void Reset()
		{
			path = GetComponent<CinemachinePathBase>();
		}

		private void Update()
		{
			if (!Application.isPlaying && path)
			{
				const CinemachinePathBase.PositionUnits units = CinemachinePathBase.PositionUnits.Normalized;

				int childCount = transform.childCount;

				for (int i = 0; i < childCount; i++)
				{
					float delta = i / (childCount + 0f);
					Transform child = transform.GetChild(i);
					Vector3 position = path.EvaluatePositionAtUnit(delta, units);
					child.localPosition = transform.WorldToLocal(position);

					if (rotate)
					{
						Quaternion rotation = path.EvaluateOrientationAtUnit(delta, units);
						child.localRotation = transform.rotation.Inverse() * rotation;
					}
				}
			}
		}
	}
}
#endif