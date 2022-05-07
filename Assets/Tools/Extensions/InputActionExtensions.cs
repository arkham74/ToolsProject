#if ENABLE_INPUT_SYSTEM
using System;
using System.Linq;
using UnityEngine.InputSystem;

public static class InputActionExtensions
{
	public static InputBinding GetBindingByID(this InputAction action, string id)
	{
		return action.bindings[action.GetBindingIndexByID(id)];
	}

	public static int GetBindingIndexByID(this InputAction action, string id)
	{
		return action.bindings.IndexOf(e => e.id.ToString() == id);
	}

	public static InputBinding GetActiveBinding(this InputAction action)
	{
		InputBinding? ib = action.GetBindingForControl(action.activeControl);
		return ib ?? throw new NullReferenceException($"{action} has no binding");
	}
}
#endif