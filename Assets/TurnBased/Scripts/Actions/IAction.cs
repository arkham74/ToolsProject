using System.Collections;
using System.Threading.Tasks;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace RTS
{
	public interface IAction
	{
		public Task Wait();
	}
}