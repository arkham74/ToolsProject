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
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace CustomTools
{
	public class DemoOnly : MonoBehaviour
	{
#if DEMO
		private void Start()
		{
			this.EnableGameObject();
		}
#else
		private void Start()
		{
			this.DisableGameObject();
		}
#endif
	}
}