using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace JD
{
	public class SequencerBehaviour : MonoBehaviour
	{
		internal readonly static ObjectPool<SequencerBehaviour> Pool = new ObjectPool<SequencerBehaviour>(Create, Get, Release, Kill);

		internal static SequencerBehaviour Create()
		{
			return new GameObject().AddComponent<SequencerBehaviour>();
		}

		private static void Get(SequencerBehaviour obj)
		{
			obj.SetActiveGameObject(true);
		}

		private static void Release(SequencerBehaviour obj)
		{
			obj.SetActiveGameObject(false);
		}

		private static void Kill(SequencerBehaviour obj)
		{
			Object.Destroy(obj.gameObject);
		}

		internal IEnumerator ReleaseAfter(Coroutine coroutine)
		{
			yield return coroutine;
			Pool.Release(this);
		}
	}

	public static class Sequencer
	{
		public static void Repeat(Action complete, Action<float> tick, float seconds)
		{
			SequencerBehaviour instance = SequencerBehaviour.Pool.Get();
			instance.StartCoroutine(instance.ReleaseAfter(instance.Repeat(complete, tick, seconds)));
		}

		public static void RepeatRealtime(Action complete, Action<float> tick, float seconds)
		{
			SequencerBehaviour instance = SequencerBehaviour.Pool.Get();
			instance.StartCoroutine(instance.ReleaseAfter(instance.RepeatRealtime(complete, tick, seconds)));
		}

		public static void Repeat(Action complete, Action<int> tick, int seconds)
		{
			SequencerBehaviour instance = SequencerBehaviour.Pool.Get();
			instance.StartCoroutine(instance.ReleaseAfter(instance.Repeat(complete, tick, seconds)));
		}

		public static void RepeatRealtime(Action complete, Action<int> tick, int seconds)
		{
			SequencerBehaviour instance = SequencerBehaviour.Pool.Get();
			instance.StartCoroutine(instance.ReleaseAfter(instance.RepeatRealtime(complete, tick, seconds)));
		}

		public static void DelayUntil(Action complete, Func<bool> untilTrue)
		{
			SequencerBehaviour instance = SequencerBehaviour.Pool.Get();
			instance.StartCoroutine(instance.ReleaseAfter(instance.DelayUntil(complete, untilTrue)));
		}

		public static void DelayWhile(Action complete, Func<bool> whileTrue)
		{
			SequencerBehaviour instance = SequencerBehaviour.Pool.Get();
			instance.StartCoroutine(instance.ReleaseAfter(instance.DelayWhile(complete, whileTrue)));
		}

		public static void Delay(Action complete, float seconds)
		{
			SequencerBehaviour instance = SequencerBehaviour.Pool.Get();
			instance.StartCoroutine(instance.ReleaseAfter(instance.Delay(complete, seconds)));
		}

		public static void DelayFrame(Action complete, int frames = 1)
		{
			SequencerBehaviour instance = SequencerBehaviour.Pool.Get();
			instance.StartCoroutine(instance.ReleaseAfter(instance.DelayFrame(complete, frames)));
		}

		public static void DelayRealtime(Action complete, float seconds)
		{
			SequencerBehaviour instance = SequencerBehaviour.Pool.Get();
			instance.StartCoroutine(instance.ReleaseAfter(instance.DelayRealtime(complete, seconds)));
		}
	}
}