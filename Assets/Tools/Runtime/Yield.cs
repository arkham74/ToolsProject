using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace JD
{
	public static class Yield
	{
		private static readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
		private static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();
		private static readonly Dictionary<float, WaitForSecondsRealtime> waitForSecondsRealtime = new Dictionary<float, WaitForSecondsRealtime>();

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

		public static async void WaitForEndOfFrameAsync(int frames = 1)
		{
			for (int i = 0; i < frames; i++)
			{
				await Task.Yield();
			}
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