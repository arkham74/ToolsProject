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
			position.x = position.x.Floor();
			position.y = position.y.Round();
			position.z = position.z.Floor();
			return position + new Vector3(0.5f, 0, 0.5f);
		}
	}
}