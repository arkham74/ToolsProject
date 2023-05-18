using System.Collections;
using System.Threading.Tasks;
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

		public async Task Wait()
		{
			Debug.Log(message);
			await Task.Yield();
		}

		public override string ToString()
		{
			return $"Log: {message}";
		}
	}
}