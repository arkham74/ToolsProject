#if ENABLE_INPUT_SYSTEM
using System;
using UnityEngine.InputSystem;

public static class InputExtensions
{
	public static InputBinding GetActiveBinding(this InputAction action)
	{
		InputBinding? ib = action.GetBindingForControl(action.activeControl);
		return ib ?? throw new NullReferenceException($"{action} has no binding");
	}
}
#endif