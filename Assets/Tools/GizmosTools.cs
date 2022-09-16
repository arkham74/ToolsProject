﻿using UnityEditor;
using UnityEngine;

namespace JD
{
	public static class GizmosTools
	{
		public static void DrawWireSphere(Vector3 position, Quaternion rotation, float radius)
		{
#if UNITY_EDITOR
			Handles.DrawWireDisc(position, rotation * Vector3.up, radius);
			Handles.DrawWireDisc(position, rotation * Vector3.right, radius);
			Handles.DrawWireDisc(position, rotation * Vector3.forward, radius);
#endif
		}

		public static void DrawWireCapsule(Vector3 position, Quaternion rotation, float radius, float height)
		{
#if UNITY_EDITOR
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
#endif
		}

		public static void DrawHex(Vector2 position, float radius, int pointTop = 0)
		{
#if UNITY_EDITOR
			for (int i = 1; i < 7; i++)
			{
				Vector2 corner = Tools.GetHexCorner(position, radius, i - 1, pointTop);
				Vector2 next = Tools.GetHexCorner(position, radius, i, pointTop);
				Gizmos.DrawLine(corner, next);
			}
#endif
		}

		public static void DrawHex(Vector3 position, float radius)
		{
#if UNITY_EDITOR
			for (int i = 1; i < 7; i++)
			{
				Vector3 corner = Tools.GetHexCorner(position, radius, i - 1);
				Vector3 next = Tools.GetHexCorner(position, radius, i);
				Gizmos.DrawLine(corner, next);
			}
#endif
		}

		public static void DrawHex(Vector3 position, float radius, float height)
		{
#if UNITY_EDITOR
			if (Mathf.Approximately(height, 0))
			{
				DrawHex(position, radius);
			}

			Vector3 offset = 0.5f * height * Vector3.up;
			for (int i = 1; i < 7; i++)
			{
				Vector3 cornerTop = Tools.GetHexCorner(position, radius, i - 1) + offset;
				Vector3 nextTop = Tools.GetHexCorner(position, radius, i) + offset;
				Vector3 cornerBot = Tools.GetHexCorner(position, radius, i - 1) - offset;
				Vector3 nextBot = Tools.GetHexCorner(position, radius, i) - offset;

				Gizmos.DrawLine(cornerTop, nextTop);
				Gizmos.DrawLine(cornerBot, nextBot);
				Gizmos.DrawLine(cornerTop, cornerBot);
			}
#endif
		}
	}
}
