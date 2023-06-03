#if TOOLS_CINEMACHINE

using Cinemachine;
using Freya;
using UnityEngine;

namespace JD
{
	public abstract class CinemachinePathCustom : CinemachinePathBase
	{
		[SerializeField] protected bool loop;
		[SerializeField] protected Vector3[] waypoints;

		public override float MinPos => 0;
		public override bool Looped => loop;
		public override int DistanceCacheSampleStepsPerSegment => m_Resolution;

		public override float MaxPos
		{
			get
			{
				int count = waypoints.Length - 1;
				if (count < 1)
				{
					return 0;
				}

				return Looped ? count + 1 : count;
			}
		}

		protected virtual void Reset()
		{
			loop = false;
			waypoints = new[] { new Vector3(0, 0, -5), Vector3.zero, new Vector3(-5, 0, 0) };
			m_Appearance = new Appearance();
			InvalidateDistanceCache();
		}

		protected virtual void OnValidate()
		{
			InvalidateDistanceCache();
		}

		public override Vector3 EvaluateLocalPosition(float pos)
		{
			return transform.InverseTransformPoint(EvaluatePosition(pos));
		}
		public override Vector3 EvaluateLocalTangent(float pos)
		{
			return transform.InverseTransformDirection(EvaluateTangent(pos));
		}
		public override Quaternion EvaluateLocalOrientation(float pos)
		{
			return transform.InverseTransformRotation(EvaluateOrientation(pos));
		}
	}
}
#endif