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

		public IEnumerator Wait()
		{
			yield return Yield.WaitForSeconds(duration);
		}

		public override string ToString()
		{
			return $"Wait: {duration}s";
		}
	}
}