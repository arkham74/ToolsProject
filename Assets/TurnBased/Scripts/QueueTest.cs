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
			GameQueue instance = GameQueue.Instance;

			if (Input.GetKeyDown(KeyCode.Space))
			{
				instance.AddAction(new WaitAction(0.5f));
				instance.AddAction(new LogAction("Log " + (instance.Actions.Count + 1) / 2));
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
			string text = GameQueue.Instance.Actions.Join("\n");
			GUILayout.Label(text, headStyle);
		}
	}
}