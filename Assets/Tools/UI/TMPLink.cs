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
using Text = TMPro.TMP_Text;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

[ExecuteAlways]
public class TMPLink : MonoBehaviour
{
	public Text parent;
	public Text target;

	private void Start()
	{
		parent = transform.parent.GetComponent<Text>();
		target = GetComponent<Text>();
	}

	private void LateUpdate()
	{
		target.SetText(parent.text);
	}
}