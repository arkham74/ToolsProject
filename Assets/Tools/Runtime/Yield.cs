using System.Collections.Generic;
using UnityEngine;

namespace JD
{
	public static class Yield
	{
		private readonly static WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
		private readonly static Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();
		private readonly static Dictionary<float, WaitForSecondsRealtime> waitForSecondsRealtime = new Dictionary<float, WaitForSecondsRealtime>();

		public static WaitForEndOfFrame WaitForEndOfFrame() => waitForEndOfFrame;

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