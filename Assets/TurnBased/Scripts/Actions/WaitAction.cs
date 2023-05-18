using System.Collections;
using System.Threading.Tasks;
using JD;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace RTS
{
	public struct WaitAction : IAction
	{
		private float duration;

		public WaitAction(float duration)
		{
			this.duration = duration;
		}

		public async Task Wait()
		{
			await WaitAction.Wait(duration);
		}

		public static async Task Wait(float duration)
		{
			float start = Time.time;
			while (Time.time < start + duration)
			{
				await Task.Yield();
			}
		}
	}
}