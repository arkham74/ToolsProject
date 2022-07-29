using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;

namespace CustomTools
{
	public class RandomTriggerSimple : BaseTrigger
	{
		[SerializeField] private UnityEvent<bool> onTrue;
		[SerializeField] private UnityEvent<bool> onFalse;

		protected override void Trigger()
		{
			bool value = Random.value > 0.5f;
			onTrue.Invoke(value);
			onFalse.Invoke(!value);
		}
	}
}