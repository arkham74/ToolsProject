using System.Collections;
using System.Threading.Tasks;
using JD;
using UnityEngine;
using UnityEngine.Assertions;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace RTS
{
	public struct LogAction : IAction
	{
		private object message;

		public LogAction(object message) : this()
		{
			this.message = message;
		}

		public IEnumerator Wait()
		{
			Assert.IsNotNull(message);
			Debug.Log(message);
			yield return Yield.WaitForEndOfFrame();
		}

		public override string ToString()
		{
			return $"Log: {message}";
		}
	}
}