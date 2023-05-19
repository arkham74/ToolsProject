using System.Collections;
using System.Threading.Tasks;
using JD;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace RTS
{
	public struct LogAction : IAction
	{
		private object message;

		public LogAction(object message)
		{
			this.message = message;
		}

		public IEnumerator Wait()
		{
			Debug.Log(message);
			yield return Yield.WaitForEndOfFrame();
		}

		public override string ToString()
		{
			return $"Log: {message}";
		}
	}
}