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
using TMPro;
using JD;
using Freya;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tools = JD.Tools;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD
{
	public class CinemachinePathSimple : CinemachinePathBase
	{
		[SerializeField] [Range(0, 1)] private float radius = 0.5f;
		[SerializeField] private bool loop;
		[SerializeField] private Vector3[] waypoints;

		public override bool Looped => loop;
		public override int DistanceCacheSampleStepsPerSegment => m_Resolution;
		public override float MinPos => 0;

		public override float MaxPos
		{
			get
			{
				int count = waypoints.Length - 1;
				if (count < 1)
				{
					return 0;
				}

				return Looped ? count + 1 : count;
			}
		}

		private void Reset()
		{
			loop = false;
			waypoints = new[] { new Vector3(0, 0, -5), Vector3.zero, new Vector3(-5, 0, 0) };
			m_Appearance = new Appearance();
			InvalidateDistanceCache();
		}

		private void OnValidate()
		{
			InvalidateDistanceCache();
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = m_Appearance.pathColor;

			for (int i = 0; i < m_Resolution; i++)
			{
				float t1 = (i + 0f) / m_Resolution;
				float t2 = (i + 1f) / m_Resolution;
				Vector3 from = EvaluatePositionAtUnit(t1, PositionUnits.Normalized);
				Vector3 to = EvaluatePositionAtUnit(t2, PositionUnits.Normalized);
				Gizmos.DrawLine(from, to);
			}
		}

		public override Vector3 EvaluateTangent(float pos)
		{
			return Vector3.forward;
		}

		public override Quaternion EvaluateOrientation(float pos)
		{
			return Quaternion.Euler(EvaluateTangent(pos));
		}

		public override Vector3 EvaluatePosition(float pos)
		{
			pos = StandardizePos(pos);

			switch (waypoints.Length)
			{
				case > 2:
					(int a, int b, int c, float t) = GetIndexes3(pos);
					return transform.LocalToWorld(RoundCorner(waypoints[a], waypoints[b], waypoints[c], t));
				case > 1:
					(a, b, t) = GetIndexes2(pos);
					return transform.LocalToWorld(Vector3.Lerp(waypoints[a], waypoints[b], t));
				case > 0:
					return transform.LocalToWorld(waypoints[0]);
				default:
					return transform.LocalToWorld(Vector3.zero);
			}
		}

		private Vector3 RoundCorner(Vector3 a, Vector3 b, Vector3 c, float t)
		{
			Vector3 ab = Vector3.Lerp(a, b, t);
			Vector3 bc = Vector3.Lerp(b, c, t);
			Vector3 rounded = Vector3.Lerp(ab, bc, t);
			return rounded;
		}

		private (int, int, float) GetIndexes2(float pos)
		{
			int a = Mathf.FloorToInt(pos);
			int b = Mathf.CeilToInt(pos);

			if (Looped)
			{
				b = b.Mod((int)MaxPos);
			}

			float t = pos - a;
			return (a, b, t);
		}

		private (int a, int b, int c, float t) GetIndexes3(float pos)
		{
			return (0, 1, 2, pos);
		}
	}
}
