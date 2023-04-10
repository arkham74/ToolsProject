using System;
using Object = UnityEngine.Object;

namespace JD
{
	public static class Sequencer
	{
		public static void Repeat(Action complete, Action<float> tick, float seconds)
		{
			SequencerBehaviour instance = SequencerBehaviour.Get();
			instance.StartCoroutine(instance.ReleaseAfter(instance.Repeat(complete, tick, seconds)));
		}

		public static void RepeatRealtime(Action complete, Action<float> tick, float seconds)
		{
			SequencerBehaviour instance = SequencerBehaviour.Get();
			instance.StartCoroutine(instance.ReleaseAfter(instance.RepeatRealtime(complete, tick, seconds)));
		}

		public static void Repeat(Action complete, Action<int> tick, int seconds)
		{
			SequencerBehaviour instance = SequencerBehaviour.Get();
			instance.StartCoroutine(instance.ReleaseAfter(instance.Repeat(complete, tick, seconds)));
		}

		public static void RepeatRealtime(Action complete, Action<int> tick, int seconds)
		{
			SequencerBehaviour instance = SequencerBehaviour.Get();
			instance.StartCoroutine(instance.ReleaseAfter(instance.RepeatRealtime(complete, tick, seconds)));
		}

		public static void DelayUntil(Action complete, Func<bool> untilTrue)
		{
			SequencerBehaviour instance = SequencerBehaviour.Get();
			instance.StartCoroutine(instance.ReleaseAfter(instance.DelayUntil(complete, untilTrue)));
		}

		public static void DelayWhile(Action complete, Func<bool> whileTrue)
		{
			SequencerBehaviour instance = SequencerBehaviour.Get();
			instance.StartCoroutine(instance.ReleaseAfter(instance.DelayWhile(complete, whileTrue)));
		}

		public static void Delay(Action complete, float seconds)
		{
			SequencerBehaviour instance = SequencerBehaviour.Get();
			instance.StartCoroutine(instance.ReleaseAfter(instance.Delay(complete, seconds)));
		}

		public static void DelayFrame(Action complete, int frames = 1)
		{
			SequencerBehaviour instance = SequencerBehaviour.Get();
			instance.StartCoroutine(instance.ReleaseAfter(instance.DelayFrame(complete, frames)));
		}

		public static void DelayRealtime(Action complete, float seconds)
		{
			SequencerBehaviour instance = SequencerBehaviour.Get();
			instance.StartCoroutine(instance.ReleaseAfter(instance.DelayRealtime(complete, seconds)));
		}
	}
}