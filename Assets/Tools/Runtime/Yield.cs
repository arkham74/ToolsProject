using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace JD
{
	public static class Yield
	{
		private static readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
		private static readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

		private static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();
		private static readonly Dictionary<float, WaitForSecondsRealtime> waitForSecondsRealtime = new Dictionary<float, WaitForSecondsRealtime>();

		public static WaitForFixedUpdate WaitForFixedUpdate()
		{
			return waitForFixedUpdate;
		}

		public static WaitForEndOfFrame WaitForEndOfFrame()
		{
			return waitForEndOfFrame;
		}

		public static IEnumerator WaitForEndOfFrame(int frames)
		{
			for (int i = 0; i < frames; i++)
			{
				yield return waitForEndOfFrame;
			}
		}

		public static async Task TaskYield(int frames = 1)
		{
			for (int i = 0; i < frames; i++)
			{
				await Task.Yield();
			}
		}

		public static async Task TaskDelay(float seconds)
		{
			await Task.Delay((int)(seconds * 1000));
		}

		public static WaitForSeconds WaitForSeconds(float seconds)
		{
			if (!waitForSeconds.ContainsKey(seconds))
			{
				waitForSeconds.Add(seconds, new WaitForSeconds(seconds));
			}
			return waitForSeconds[seconds];
		}

		public static WaitForSecondsRealtime WaitForSecondsRealtime(float seconds)
		{
			if (!waitForSecondsRealtime.ContainsKey(seconds))
			{
				waitForSecondsRealtime.Add(seconds, new WaitForSecondsRealtime(seconds));
			}
			return waitForSecondsRealtime[seconds];
		}
	}
}