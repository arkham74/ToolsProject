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
using UnityEditor;
using JD.Draw;
using Redcode.Extensions;
using UnityEngine.Tilemaps;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Village
{
	[ExecuteAlways]
	public class TEST_DRAW : MonoBehaviour
	{
		[SerializeField] private Color color1 = Color.white;
		[SerializeField] private Color color2 = Color.white;
		[SerializeField] private float width = 2f;
		[SerializeField] private float radius = 1f;
		[SerializeField] private int segments = 64;

		private void OnEnable()
		{
			Draw.OnUpdate.AddListener(DrawUpdate);
		}

		private void OnDisable()
		{
			Draw.OnUpdate.RemoveListener(DrawUpdate);
		}

		private void DrawUpdate()
		{
			Draw.Box(transform.position, transform.lossyScale, transform.rotation, color1, width);
			Draw.Circle(transform.position, transform.rotation, color2, radius, width, segments);
		}
	}
}