#if ENABLE_INPUT_SYSTEM
using System;
using System.Linq;
using UnityEngine.InputSystem;

public static class InputExtensions
{
	public static InputBinding GetBinding(this InputAction action, string id)
	{
		return action.bindings.First(e => e.id.ToString() == id);
	}

	public static int GetBindingIndex(this InputAction action, string id)
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