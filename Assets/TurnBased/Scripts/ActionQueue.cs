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
	public class ActionQueue : MonoBehaviour
	{
		public static readonly Queue<IAction> Actions = new Queue<IAction>();
		private static ActionQueue _instance;

		private static void StartQueue()
		{
			GetQueueRunner().StartCoroutine(QueueCoroutine());
			IEnumerator QueueCoroutine()
			{
				while (Actions.Count > 0)
				{
					yield return Actions.Peek().Wait();
					Actions.Dequeue();
				}
			}
		}

		private static ActionQueue GetQueueRunner()
		{
			if (_instance == null)
			{
				_instance = new GameObject("ActionQueueRunner").AddComponent<ActionQueue>();
				_instance.gameObject.hideFlags = HideFlags.HideInHierarchy;
			}
			return _instance;
		}

		public static void AddAction(IAction action)
		{
			Actions.Enqueue(action);
			if (Actions.Count == 1)
			{
				StartQueue();
			}
		}
	}
}