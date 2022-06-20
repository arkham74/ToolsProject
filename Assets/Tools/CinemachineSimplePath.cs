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
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;

#if TOOLS_NAUATTR
using NaughtyAttributes;
#endif

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

#if TOOLS_CINEMACHINE
using Cinemachine;
public class CinemachineSimplePath : CinemachinePathBase
{
	[SerializeField] private Vector3[] waypoints;
	[SerializeField] private bool loop;

#if TOOL_NAUATTR
	[ShowNativeProperty]
#endif
	public float Length => waypoints.PathLength();

	public override bool Looped => loop;
	public override int DistanceCacheSampleStepsPerSegment => m_Resolution;
	public override float MinPos => 0;
	public override float MaxPos
	{
		get
		{
			int count = waypoints.Length - 1;
			if (count < 1)
				return 0;
			return loop ? count + 1 : count;
		}
	}

	private void Reset()
	{
		loop = false;
		waypoints = new Vector3[2] { new Vector3(0, 0, -5), new Vector3(0, 0, 5) };
		m_Appearance = new Appearance();
		InvalidateDistanceCache();
	}

	private void OnValidate()
	{
		m_Appearance.width = Mathf.Max(m_Appearance.width, 0.1f);
		InvalidateDistanceCache();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = m_Appearance.inactivePathColor;
		int max = (int)(Length * m_Appearance.width * 10);

		for (int i = 0; i <= max; i++)
		{
			float t = (float)i / max;
			Gizmos.DrawSphere(EvaluatePositionAtUnit(t, PositionUnits.Normalized), 0.01f);
		}

		Gizmos.color = m_Appearance.pathColor;
		Gizmos.matrix = transform.localToWorldMatrix;
		for (int i = 1; i < waypoints.Length; i++)
		{
			Gizmos.DrawLine(waypoints[i - 1], waypoints[i]);
		}
	}

	private float GetBoundingIndices(float pos, out int indexA, out int indexB)
	{
		pos = StandardizePos(pos);

		indexA = Mathf.FloorToInt(pos);
		if (indexA >= waypoints.Length)
		{
			pos -= MaxPos;
			indexA = 0;
		}
		indexB = indexA + 1;
		if (indexB >= waypoints.Length)
			indexB = 0;

		return pos - indexA;
	}

	public override Quaternion EvaluateOrientation(float pos)
	{
		Quaternion rotation = Quaternion.Euler(0, 0, 0);

		// if (waypoints.Length > 1)
		// {
		// 	pos = GetBoundingIndices(pos, out int indexA, out int indexB);
		// 	Vector3 dir = waypoints[indexA].DirTo(waypoints[indexB]);
		// 	if (dir != Vector3.zero)
		// 		rotation = Quaternion.LookRotation(dir, Vector3.forward);
		// }

		return rotation;
	}

	public override Vector3 EvaluatePosition(float pos)
	{
		Vector3 result = Vector3.zero;

		if (waypoints.Length > 1)
		{
			pos = GetBoundingIndices(pos, out int indexA, out int indexB);
			result = Vector3.Lerp(waypoints[indexA], waypoints[indexB], pos);
		}
		else if (waypoints.Length == 1)
		{
			result = waypoints[0];
		}

		return transform.TransformPoint(result);
	}

	public override Vector3 EvaluateTangent(float pos)
	{
		return Vector3.up;
	}
}
#endif