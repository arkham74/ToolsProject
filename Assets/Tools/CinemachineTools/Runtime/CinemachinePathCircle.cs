#if TOOLS_CINEMACHINE
using Cinemachine;
using Freya;
using JD;
using SebastianLague;
using UnityEditor;
using UnityEngine;
using Tools = JD.Tools;

namespace JD
{
	[ExecuteAlways]
	public class CinemachinePathCircle : CinemachinePathBase
	{
		[SerializeField][Range(0, 10)] private float radius = 1f;

		public override float MinPos => 0f;
		public override float MaxPos => 1f;
		public override bool Looped => true;
		public override int DistanceCacheSampleStepsPerSegment => m_Resolution;

		public override Quaternion EvaluateOrientation(float pos)
		{
			return Quaternion.LookRotation(EvaluateTangent(pos));
		}

		public override Vector3 EvaluateTangent(float pos)
		{
			pos = StandardizePos(pos);
			float f = pos * Mathfs.TAU;
			float cos = Mathf.Cos(f);
			float sin = Mathf.Sin(f);
			Vector3 tangent = Quaternion.Euler(0, 90, 0) * new Vector3(sin, 0, cos);
			return transform.TransformDirection(tangent);
		}

		public override Vector3 EvaluatePosition(float pos)
		{
			pos = StandardizePos(pos);
			float f = pos * Mathfs.TAU;
			float cos = Mathf.Cos(f) * radius;
			float sin = Mathf.Sin(f) * radius;
			Vector3 local = new Vector3(sin, 0, cos);
			return transform.LocalToWorld(local);
		}

#if UNITY_2022_3_OR_NEWER
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
#endif
	}
}
#endif