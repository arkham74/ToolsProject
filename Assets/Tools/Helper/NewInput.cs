#if ENABLE_INPUT_SYSTEM
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JD
{
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

		public static bool GetKey(KeyCode keyCode)
		{
			return Keyboard.current[ConvertKeyCode(keyCode)].isPressed;
		}

		public static bool GetKeyDown(KeyCode keyCode)
		{
			return Keyboard.current[ConvertKeyCode(keyCode)].wasPressedThisFrame;
		}

		public static bool GetKeyUp(KeyCode keyCode)
		{
			return Keyboard.current[ConvertKeyCode(keyCode)].wasReleasedThisFrame;
		}

		public static Key ConvertKeyCode(KeyCode keyCode) => keyCode switch
		{
			KeyCode.Space => Key.Space,
			KeyCode.Return => Key.Enter,
			KeyCode.Tab => Key.Tab,
			KeyCode.BackQuote => Key.Backquote,
			KeyCode.Quote => Key.Quote,
			KeyCode.Semicolon => Key.Semicolon,
			KeyCode.Comma => Key.Comma,
			KeyCode.Period => Key.Period,
			KeyCode.Slash => Key.Slash,
			KeyCode.Backslash => Key.Backslash,
			KeyCode.LeftBracket => Key.LeftBracket,
			KeyCode.RightBracket => Key.RightBracket,
			KeyCode.Minus => Key.Minus,
			KeyCode.Equals => Key.Equals,
			KeyCode.A => Key.A,
			KeyCode.B => Key.B,
			KeyCode.C => Key.C,
			KeyCode.D => Key.D,
			KeyCode.E => Key.E,
			KeyCode.F => Key.F,
			KeyCode.G => Key.G,
			KeyCode.H => Key.H,
			KeyCode.I => Key.I,
			KeyCode.J => Key.J,
			KeyCode.K => Key.K,
			KeyCode.L => Key.L,
			KeyCode.M => Key.M,
			KeyCode.N => Key.N,
			KeyCode.O => Key.O,
			KeyCode.P => Key.P,
			KeyCode.Q => Key.Q,
			KeyCode.R => Key.R,
			KeyCode.S => Key.S,
			KeyCode.T => Key.T,
			KeyCode.U => Key.U,
			KeyCode.V => Key.V,
			KeyCode.W => Key.W,
			KeyCode.X => Key.X,
			KeyCode.Y => Key.Y,
			KeyCode.Z => Key.Z,
			KeyCode.Alpha1 => Key.Digit1,
			KeyCode.Alpha2 => Key.Digit2,
			KeyCode.Alpha3 => Key.Digit3,
			KeyCode.Alpha4 => Key.Digit4,
			KeyCode.Alpha5 => Key.Digit5,
			KeyCode.Alpha6 => Key.Digit6,
			KeyCode.Alpha7 => Key.Digit7,
			KeyCode.Alpha8 => Key.Digit8,
			KeyCode.Alpha9 => Key.Digit9,
			KeyCode.Alpha0 => Key.Digit0,
			KeyCode.LeftShift => Key.LeftShift,
			KeyCode.RightShift => Key.RightShift,
			KeyCode.LeftAlt => Key.LeftAlt,
			KeyCode.RightAlt => Key.RightAlt,
			KeyCode.AltGr => Key.AltGr,
			KeyCode.LeftControl => Key.LeftCtrl,
			KeyCode.RightControl => Key.RightCtrl,
			KeyCode.LeftCommand => Key.LeftCommand,
			KeyCode.RightCommand => Key.RightCommand,
			KeyCode.LeftWindows => Key.LeftWindows,
			KeyCode.RightWindows => Key.RightWindows,
			KeyCode.Menu => Key.ContextMenu,
			KeyCode.Escape => Key.Escape,
			KeyCode.LeftArrow => Key.LeftArrow,
			KeyCode.RightArrow => Key.RightArrow,
			KeyCode.UpArrow => Key.UpArrow,
			KeyCode.DownArrow => Key.DownArrow,
			KeyCode.Backspace => Key.Backspace,
			KeyCode.PageDown => Key.PageDown,
			KeyCode.PageUp => Key.PageUp,
			KeyCode.Home => Key.Home,
			KeyCode.End => Key.End,
			KeyCode.Insert => Key.Insert,
			KeyCode.Delete => Key.Delete,
			KeyCode.CapsLock => Key.CapsLock,
			KeyCode.Numlock => Key.NumLock,
			KeyCode.Print => Key.PrintScreen,
			KeyCode.ScrollLock => Key.ScrollLock,
			KeyCode.Pause => Key.Pause,
			KeyCode.KeypadEnter => Key.NumpadEnter,
			KeyCode.KeypadDivide => Key.NumpadDivide,
			KeyCode.KeypadMultiply => Key.NumpadMultiply,
			KeyCode.KeypadPlus => Key.NumpadPlus,
			KeyCode.KeypadMinus => Key.NumpadMinus,
			KeyCode.KeypadPeriod => Key.NumpadPeriod,
			KeyCode.KeypadEquals => Key.NumpadEquals,
			KeyCode.Keypad0 => Key.Numpad0,
			KeyCode.Keypad1 => Key.Numpad1,
			KeyCode.Keypad2 => Key.Numpad2,
			KeyCode.Keypad3 => Key.Numpad3,
			KeyCode.Keypad4 => Key.Numpad4,
			KeyCode.Keypad5 => Key.Numpad5,
			KeyCode.Keypad6 => Key.Numpad6,
			KeyCode.Keypad7 => Key.Numpad7,
			KeyCode.Keypad8 => Key.Numpad8,
			KeyCode.Keypad9 => Key.Numpad9,
			KeyCode.F1 => Key.F1,
			KeyCode.F2 => Key.F2,
			KeyCode.F3 => Key.F3,
			KeyCode.F4 => Key.F4,
			KeyCode.F5 => Key.F5,
			KeyCode.F6 => Key.F6,
			KeyCode.F7 => Key.F7,
			KeyCode.F8 => Key.F8,
			KeyCode.F9 => Key.F9,
			KeyCode.F10 => Key.F10,
			KeyCode.F11 => Key.F11,
			KeyCode.F12 => Key.F12,
			_ => Key.None,
		};

		public static float GetAxisVertical()
		{
			float pos = Convert.ToSingle(Keyboard.current[Key.W].isPressed);
			float neg = -Convert.ToSingle(Keyboard.current[Key.S].isPressed);
			return pos + neg;
		}

		public static float GetAxisHorizontal()
		{
			float pos = Convert.ToSingle(Keyboard.current[Key.D].isPressed);
			float neg = -Convert.ToSingle(Keyboard.current[Key.A].isPressed);
			return pos + neg;
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
}
#endif