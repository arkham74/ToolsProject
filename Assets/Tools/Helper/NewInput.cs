#if ENABLE_INPUT_SYSTEM
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Text = TMPro.TextMeshProUGUI;
using Tag = NaughtyAttributes.TagAttribute;

// ReSharper disable InconsistentNaming
public static class NewInput
{
	public enum MouseButton
	{
		leftButton,
		rightButton,
		middleButton,
		forwardButton,
		backButton
	}

	public static Vector3 MouseDelta => Mouse.current.delta.ReadValue();
	public static Vector3 MousePosition => Mouse.current.position.ReadValue();

	public static bool GetKey(Key key)
	{
		return Keyboard.current[key].isPressed;
	}

	public static bool GetMouseButton(MouseButton button)
	{
		return button switch
		{
			MouseButton.leftButton => Mouse.current.leftButton.isPressed,
			MouseButton.rightButton => Mouse.current.rightButton.isPressed,
			MouseButton.middleButton => Mouse.current.middleButton.isPressed,
			MouseButton.forwardButton => Mouse.current.forwardButton.isPressed,
			MouseButton.backButton => Mouse.current.backButton.isPressed,
			_ => throw new ArgumentOutOfRangeException(nameof(button), button, null)
		};
	}
}
#endif