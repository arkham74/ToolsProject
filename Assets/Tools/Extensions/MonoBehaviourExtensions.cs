using System;
using System.Collections;
using UnityEngine;

namespace JD
{
	public static class MonoBehaviourExtensions
	{
		public static void Repeat(this MonoBehaviour mb, Action complete, Action<float> tick, float seconds)
		{
			IEnumerator RepeatRoutine()
			{
				for (float i = 0; i < seconds; i += Time.deltaTime)
				{
					tick.Invoke(i);
					yield return new WaitForEndOfFrame();
				}
				complete.Invoke();
			}
			mb.StartCoroutine(RepeatRoutine());
		}

		public static void RepeatRealtime(this MonoBehaviour mb, Action complete, Action<float> tick, float seconds)
		{
			IEnumerator RepeatRoutine()
			{
				for (float i = 0; i < seconds; i += Time.unscaledDeltaTime)
				{
					tick.Invoke(i);
					yield return new WaitForEndOfFrame();
				}
				complete.Invoke();
			}
			mb.StartCoroutine(RepeatRoutine());
		}

		public static void Repeat(this MonoBehaviour mb, Action complete, Action<int> tick, int seconds)
		{
			IEnumerator RepeatRoutine()
			{
				for (int i = 0; i < seconds; i++)
				{
					tick.Invoke(i);
					yield return new WaitForSeconds(1f);
				}
				complete.Invoke();
			}
			mb.StartCoroutine(RepeatRoutine());
		}

		public static void RepeatRealtime(this MonoBehaviour mb, Action complete, Action<int> tick, int seconds)
		{
			IEnumerator RepeatRoutine()
			{
				for (int i = 0; i < seconds; i++)
				{
					tick.Invoke(i);
					yield return new WaitForSecondsRealtime(1f);
				}
				complete.Invoke();
			}
			mb.StartCoroutine(RepeatRoutine());
		}

		public static void DelayUntil(this MonoBehaviour mb, Action complete, Func<bool> untilTrue)
		{
			IEnumerator DelayRoutine()
			{
				yield return new WaitUntil(untilTrue);
				complete.Invoke();
			}
			mb.StartCoroutine(DelayRoutine());
		}

		public static void DelayWhile(this MonoBehaviour mb, Action complete, Func<bool> whileTrue)
		{
			IEnumerator DelayRoutine()
			{
				yield return new WaitWhile(whileTrue);
				complete.Invoke();
			}
			mb.StartCoroutine(DelayRoutine());
		}

		public static void Delay(this MonoBehaviour mb, Action complete, float seconds)
		{
			IEnumerator DelayRoutine()
			{
				yield return new WaitForSeconds(seconds);
				complete.Invoke();
			}
			mb.StartCoroutine(DelayRoutine());
		}

		public static void DelayFrame(this MonoBehaviour mb, Action complete, int frames)
		{
			IEnumerator DelayRoutine()
			{
				for (int i = 0; i < frames; i++)
				{
					yield return new WaitForEndOfFrame();
				}
				complete.Invoke();
			}
			mb.StartCoroutine(DelayRoutine());
		}

		public static void DelayRealtime(this MonoBehaviour mb, Action complete, float seconds)
		{
			IEnumerator DelayRoutine()
			{
				yield return new WaitForSecondsRealtime(seconds);
				complete.Invoke();
			}
			mb.StartCoroutine(DelayRoutine());
		}
	}
}