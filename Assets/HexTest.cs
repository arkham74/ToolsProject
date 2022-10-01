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

public class HexTest : MonoBehaviour
{
	[SerializeField] private float size = 2f;

	private void OnDrawGizmos()
	{
		foreach (Transform child in transform)
		{
			Vector3 world = child.position;

			Hex hex = HexUtils.FromWorld(world / size);
			hex = hex.Round();
			world = hex.ToWorld() * size;

			child.position = world;
			GizmosTools.DrawHex(world, size);
		}
	}
}