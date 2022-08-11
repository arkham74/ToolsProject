using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;

namespace JD
{
	public class RandomTrigger : BaseTrigger
	{
		[Serializable]
		public class EventWithChance
		{
			public int probability = 1;
			public UnityEvent @event = new UnityEvent();
		}

		[SerializeField] private EventWithChance[] events;

		private int Sum => events.Sum(e => e.probability); //100

		protected override void Trigger()
		{
			ChooseAtRandom().Invoke();
		}

		private UnityEvent ChooseAtRandom()
		{
			int p = Random.Range(0, Sum) + 1; //1..100
			int cumulativeProbability = 0;

			foreach (EventWithChance item in events.OrderBy(e => e.probability))
			{
				cumulativeProbability += item.probability;
				if (p <= cumulativeProbability)
				{
					return item.@event;
				}
			}

			return null;
		}
	}
}