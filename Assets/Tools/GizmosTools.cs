using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace JD
{
	public static class GizmosTools
	{
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void DrawWireSphere(Vector3 position, Quaternion rotation, float radius)
		{
			Handles.DrawWireDisc(position, rotation * Vector3.up, radius);
			Handles.DrawWireDisc(position, rotation * Vector3.right, radius);
			Handles.DrawWireDisc(position, rotation * Vector3.forward, radius);
		}

		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void DrawWireCapsule(Vector3 position, Quaternion rotation, float radius, float height)
		{
			Matrix4x4 angleMatrix = Matrix4x4.TRS(position, rotation, Handles.matrix.lossyScale);
			using (new Handles.DrawingScope(angleMatrix))
			{
				float pointOffset = (height - radius * 2) / 2;

				//draw sideways
				Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.left, Vector3.back, -180, radius);
				Handles.DrawLine(new Vector3(0, pointOffset, -radius), new Vector3(0, -pointOffset, -radius));
				Handles.DrawLine(new Vector3(0, pointOffset, radius), new Vector3(0, -pointOffset, radius));
				Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.left, Vector3.back, 180, radius);
				//draw front
				Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.back, Vector3.left, 180, radius);
				Handles.DrawLine(new Vector3(-radius, pointOffset, 0), new Vector3(-radius, -pointOffset, 0));
				Handles.DrawLine(new Vector3(radius, pointOffset, 0), new Vector3(radius, -pointOffset, 0));
				Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.back, Vector3.left, -180, radius);
				//draw center
				Handles.DrawWireDisc(Vector3.up * pointOffset, Vector3.up, radius);
				Handles.DrawWireDisc(Vector3.down * pointOffset, Vector3.up, radius);
			}
		}

		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void DrawHex(Vector3 center, Vector3 normal, float radius, float angle = 0f)
		{
			DrawHex(center, normal, Vector2.one * radius, angle);
		}

		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void DrawHex(Vector3 center, Vector3 normal, Vector2 radius, float angle = 0f)
		{
			Quaternion look = Quaternion.LookRotation(normal);
			for (int i = 1; i < 7; i++)
			{
				Vector3 corner = Tools.GetHexCorner(i - 1, angle);
				Vector3 next = Tools.GetHexCorner(i, angle);
				corner.Scale(radius);
				next.Scale(radius);
				corner = look * corner + center;
				next = look * next + center;
				Gizmos.DrawLine(corner, next);
			}
		}
	}
}
