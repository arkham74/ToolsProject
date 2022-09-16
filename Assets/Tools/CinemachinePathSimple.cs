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
using JD;
using Freya;
using UnityEditor;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tools = JD.Tools;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD
{
	public class CinemachinePathSimple : CinemachinePathCustom
	{
		public override Vector3 EvaluateTangent(float pos)
		{
			pos = StandardizePos(pos);
			int a = Mathf.FloorToInt(pos);
			int b = Mathfs.Mod(a + 1, waypoints.Length);
			Vector3 from = waypoints[a];
			Vector3 to = waypoints[b];
			return transform.TransformDirection(from.DirTo(to));
		}

		public override Quaternion EvaluateOrientation(float pos)
		{
			return Quaternion.LookRotation(EvaluateTangent(pos));
		}

		public override Vector3 EvaluatePosition(float pos)
		{
			pos = StandardizePos(pos);

			switch (waypoints.Length)
			{
				case > 1:
					(int a, int b, float t) = GetIndexes2(pos);
					return transform.LocalToWorld(Vector3.Lerp(waypoints[a], waypoints[b], t));
				case > 0:
					return transform.LocalToWorld(waypoints[0]);
				default:
					return transform.LocalToWorld(Vector3.zero);
			}
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

		// case > 2:
		// 	(int a, int b, int c, float t) = GetIndexes3(pos);
		// 	return transform.LocalToWorld(RoundCorner(waypoints[a], waypoints[b], waypoints[c], t, pos));
		// private Vector3 RoundCorner(Vector3 a, Vector3 b, Vector3 c, float t, float pos)
		// {
		// 	Vector3 ba = b.To(a);
		// 	Vector3 bc = b.To(c);
		// 	Vector3 axis = Vector3.Cross(ba, bc);
		//
		// 	float angle = Mathfs.AngleBetween(ba, bc);
		// 	Vector3 halfVector = Vector3.Slerp(ba, bc, 0.5f);
		//
		// 	float distance = radius / Mathfs.Sin(angle / 2f);
		//
		// 	Vector3 center = b + halfVector.normalized * distance;
		//
		// 	float part = pos * 3f;
		//
		// 	Vector3 res = part switch
		// 	{
		// 		> 2 => Vector3.Lerp(b, c, part - 2),
		// 		> 1 => DrawCircle(center, axis, part - 1),
		// 		> 0 => Vector3.Lerp(a, b, part),
		// 		_ => Vector3.zero
		// 	};
		//
		// 	return res;
		// }
		// private Vector3 DrawCircle(Vector3 center, Vector3 normal, float t)
		// {
		// 	float x = Mathfs.Sin(t * Mathfs.TAU) * radius;
		// 	float y = Mathfs.Cos(t * Mathfs.TAU) * radius;
		// 	Quaternion rotation = Quaternion.FromToRotation(Vector3.up, normal);
		// 	Vector3 circle = new Vector3(x, 0, y);
		// 	return rotation * circle + center;
		// }
		// private (int a, int b, int c, float t) GetIndexes3(float pos)
		// {
		// 	return (0, 1, 2, pos);
		// }
	}
}
