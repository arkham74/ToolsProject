#if TOOLS_CINEMACHINE
// https://forum.unity.com/threads/follow-only-along-a-certain-axis.544511/#post-3591751
using UnityEngine;
using Cinemachine;

namespace JD
{
	/// <summary>
	/// An add-on module for Cinemachine Virtual Camera that locks the camera's Z co-ordinate
	/// </summary>
	[ExecuteInEditMode]
	[SaveDuringPlay]
	[AddComponentMenu("")]
	public class CinemachineLockAxis : CinemachineExtension
	{
		public enum Axis
		{
			X = 0,
			Y = 1,
			Z = 2
		}

		[Tooltip("Lock the camera's axis position to this value")]
		[SerializeField] private CinemachineCore.Stage stage = CinemachineCore.Stage.Body;
		[SerializeField] private Axis axis = Axis.Z;
		[SerializeField] private float position = -1;

		protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage camerasStage, ref CameraState state, float deltaTime)
		{
			if (camerasStage == stage)
			{
				Vector3 pos = state.RawPosition;
				pos[(int)axis] = position;
				state.RawPosition = pos;
			}
		}
	}
}
#endif
