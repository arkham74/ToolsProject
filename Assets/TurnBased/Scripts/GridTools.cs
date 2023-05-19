#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

using System;
using Freya;
using UnityEngine;

namespace RTS
{
	public static class GridTools
	{
		public static Vector3 WorldToGrid(Vector3 position)
		{
			return position.Floor() + new Vector3(0.5f, 0, 0.5f);
		}
	}
}