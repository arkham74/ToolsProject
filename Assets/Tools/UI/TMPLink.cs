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
using Text = TMPro.TextMeshProUGUI;
using Random = UnityEngine.Random;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD
{
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
}