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
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tools = JD.Tools;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace RTS
{
	public class PointerTracker : MonoBehaviour
	{
		public static Vector3 WorldPosition { get; private set; }
		private static readonly int PointerPositionID = Shader.PropertyToID("_PointerPosition");

		private void LateUpdate()
		{
			Plane plane = new Plane(Vector3.up, Vector3.zero);
			Vector3 screenPoint = Input.mousePosition;
			Ray ray = Camera.main.ScreenPointToRay(screenPoint);
			if (plane.Raycast(ray, out RaycastHit hit))
			{
				WorldPosition = hit.point;
				Shader.SetGlobalVector(PointerPositionID, hit.point);
			}
		}
	}

	public static class PlaneExtensions
	{
		public static bool Raycast(this Plane plane, Ray ray, out RaycastHit hit)
		{
			hit = default;

			if (plane.Raycast(ray, out float distance))
			{
				hit.distance = distance;
				hit.point = ray.GetPoint(distance);
				hit.normal = plane.normal;
				return true;
			}

			return false;
		}
	}
}