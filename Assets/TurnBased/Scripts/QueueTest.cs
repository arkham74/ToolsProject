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
using System.Text;
using Redcode.Pools;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace RTS
{
	public class QueueTest : MonoBehaviour
	{
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				ActionQueue.AddAction(new WaitAction(0.5f));
				ActionQueue.AddAction(new LogAction("Log " + (ActionQueue.Actions.Count + 1) / 2));
			}
		}

		private void OnGUI()
		{
			GUIStyle headStyle = new GUIStyle("Label")
			{
				stretchHeight = true,
				stretchWidth = true,
				padding = new RectOffset(7, 7, 7, 7),
				fontSize = (int)(24f * Screen.height / 1080f),
			};
			headStyle.normal.background = Resources.Load<Texture2D>("transparent_1x1");
			string text = ActionQueue.Actions.Join("\n");
			GUILayout.Label(text, headStyle);
		}
	}
}