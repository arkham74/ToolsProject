using System.Collections.Generic;
using UnityEngine;

namespace JD
{
	public static class Yield
	{
		private readonly static WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
		private readonly static Dictionary<int, WaitForSeconds> waitForSeconds = new Dictionary<int, WaitForSeconds>();

		public static WaitForEndOfFrame WaitForEndOfFrame() => waitForEndOfFrame;
		public static WaitForSeconds WaitForSeconds(int seconds)
		{
			if (!waitForSeconds.ContainsKey(seconds))
			{
				waitForSeconds.Add(seconds, new WaitForSeconds(seconds));
			}
			return waitForSeconds[seconds];
		}
	}
}