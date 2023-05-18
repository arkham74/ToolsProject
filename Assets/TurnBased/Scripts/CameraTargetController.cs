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

public class CameraTargetController : MonoBehaviour
{
	[SerializeField] private float speed = 10;

	private void Update()
	{
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		Vector3 direction = new Vector3(x, 0, z).ClampMagnitude(1f);
		Quaternion rotation = Quaternion.Euler(0, 45, 0);
		transform.Translate(rotation * direction * speed * Time.deltaTime);
	}
}