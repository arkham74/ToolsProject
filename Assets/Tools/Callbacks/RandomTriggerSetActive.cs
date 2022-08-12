using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


namespace JD
{
	public class RandomTriggerSetActive : BaseTrigger
	{
		[SerializeField] private GameObject[] objects;

		private void Reset()
		{
			objects = transform.GetChildren().Select(e => e.gameObject).ToArray();
		}

		protected override void Trigger()
		{
			objects.GroupSetActive(false);
			objects.RandomOrDefault()?.SetActive(true);
		}
	}
}