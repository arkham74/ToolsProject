using Cinemachine;
using Freya;
using JD;
using NaughtyAttributes;
using SebastianLague;
using UnityEditor;
using UnityEngine;
using Text = TMPro.TextMeshProUGUI;
using Tag = NaughtyAttributes.TagAttribute;
using Tools = JD.Tools;

namespace CordBot
{
	[ExecuteAlways]
	public class CinemachinePathCircle : CinemachinePathBase
	{
		[SerializeField] [Range(0, 10)] private float radius = 1f;

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
	}
}
