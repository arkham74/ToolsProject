using System.Collections;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace RTS
{
	public struct SpawnUnitAction : IAction
	{
		public IEnumerator Wait()
		{
			yield return null;
		}
	}
}