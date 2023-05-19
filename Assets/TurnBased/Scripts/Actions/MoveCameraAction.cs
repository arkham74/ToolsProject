using System.Collections;
using JD;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace RTS
{
	public struct MoveCameraAction : IAction
	{
		private Vector3 position;

		public MoveCameraAction(Vector3 position)
		{
			this.position = position;
		}

		public IEnumerator Wait()
		{
			yield return CameraManager.Instance.MoveToWait(GridTools.WorldToGrid(position));
		}

		public override string ToString()
		{
			return $"Move Camera: {GridTools.WorldToGrid(position)}";
		}
	}
}