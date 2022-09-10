using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Freya;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using TMPro;
using Text = TMPro.TextMeshProUGUI;
using Random = UnityEngine.Random;

#if TOOLS_NAUATTR
using NaughtyAttributes;
#endif

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

#if TOOLS_CINEMACHINE
using Cinemachine;

namespace JD
{
	public class CinemachinePathCatmullRom : CinemachinePathCustom
	{
		public override Vector3 EvaluateTangent(float pos)
		{
			return transform.forward;
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
				case > 1:
					(int a, int b, int c, int d, float t) = GetIndexes(pos);
					return transform.LocalToWorld(CatmullRom(waypoints[a], waypoints[b], waypoints[c], waypoints[d], t));
				case > 0:
					return transform.LocalToWorld(waypoints[0]);
				default:
					return transform.LocalToWorld(Vector3.zero);
			}
		}

		private (int, int, int, int, float) GetIndexes(float pos)
		{
			int b = Mathf.FloorToInt(pos);
			int a = b - 1;
			int c = b + 1;
			int d = b + 2;

			if (loop)
			{
				a = Mathfs.Mod(a, waypoints.Length);
				c = Mathfs.Mod(c, waypoints.Length);
				d = Mathfs.Mod(d, waypoints.Length);
			}
			else
			{
				a = Mathfs.Clamp(a, 0, waypoints.Length - 1);
				c = Mathfs.Clamp(c, 0, waypoints.Length - 1);
				d = Mathfs.Clamp(d, 0, waypoints.Length - 1);
			}

			float t = pos - b;
			return (a, b, c, d, t);
		}

		//Returns a position between 4 Vector3 with Catmull-Rom spline algorithm
		//http://www.iquilezles.org/www/articles/minispline/minispline.htm
		//https://www.habrador.com/tutorials/interpolation/1-catmull-rom-splines/
		public static Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			//The coefficients of the cubic polynomial (except the 0.5f * which I added later for performance)
			Vector3 a = 2f * p1;
			Vector3 b = p2 - p0;
			Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
			Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;
			//The cubic polynomial: a + b * t + c * t^2 + d * t^3
			return 0.5f * (a + b * t + c * (t * t) + d * (t * t * t));
		}
	}
}
#endif
