#if ENABLE_INPUT_SYSTEM
using System;
using System.Linq;
using UnityEngine.InputSystem;

namespace JD
{
	public static class InputActionExtensions
	{
		public static InputBinding GetActiveBinding(this InputAction action)
		{
			return action.GetBindingForControl(action.activeControl).Value;
		}

		public static InputBinding GetActiveBinding(this InputAction.CallbackContext ctx)
		{
			return ctx.action.GetActiveBinding();
		}

		public static InputBinding GetBindingByID(this InputAction action, string id)
		{
			return action.bindings[action.GetBindingIndexByID(id)];
		}

		public static int GetBindingIndexByID(this InputAction action, string id)
		{
			return action.bindings.IndexOf(e => e.id == Guid.Parse(id));
		}
	}
}
#endif