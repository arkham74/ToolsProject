using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;

namespace CustomTools
{
	public class RandomCallback : BaseCallback
	{
		[Serializable]
		public class EventWithChance
		{
			public int probability = 1;
			public UnityEvent @event = new UnityEvent();
		}

		public EventWithChance[] events;

		private int Sum => events.Sum(e => e.probability);

		protected override void Trigger()
		{
			ChooseAtRandom().Invoke();
		}

		private UnityEvent ChooseAtRandom()
		{
			int p = Random.Range(0, Sum) + 1;
			int cumulativeProbability = 0;
			foreach (EventWithChance item in events.OrderBy(e => e.probability))
			{
				cumulativeProbability += item.probability;
				if (p <= cumulativeProbability) return item.@event;
			}

			return null;
		}
	}
}