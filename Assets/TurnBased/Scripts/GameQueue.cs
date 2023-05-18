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
	public class GameQueue
	{
		public static readonly GameQueue Instance = new GameQueue();
		public readonly Queue<IAction> Actions = new Queue<IAction>();

		private async void StartQueue()
		{
			while (Actions.Count > 0)
			{
				await Actions.Peek().Wait();
				Actions.Dequeue();
			}
		}

		public void AddAction(IAction action)
		{
			Actions.Enqueue(action);
			if (Actions.Count == 1)
			{
				StartQueue();
			}
		}
	}
}