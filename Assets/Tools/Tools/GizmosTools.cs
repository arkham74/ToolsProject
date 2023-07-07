using System;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace JD
{
	public static partial class GizmosTools
	{
		[Conditional("UNITY_EDITOR")]
		public static void SetColor(Color color)
		{
#if UNITY_EDITOR
			Gizmos.color = color;
			Handles.color = color;
#endif
		}

		[Conditional("UNITY_EDITOR")]
		public static void SetMatrix(Matrix4x4 matrix)
		{
#if UNITY_EDITOR
			Gizmos.matrix = matrix;
			Handles.matrix = matrix;
#endif
		}

		[Conditional("UNITY_EDITOR")]
		public static void DrawWireSphere(Vector3 position, Quaternion rotation, float radius)
		{
#if UNITY_EDITOR
			Handles.DrawWireDisc(position, rotation * Vector3.up, radius);
			Handles.DrawWireDisc(position, rotation * Vector3.right, radius);
			Handles.DrawWireDisc(position, rotation * Vector3.forward, radius);
#endif
		}

		[Conditional("UNITY_EDITOR")]
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

		[Conditional("UNITY_EDITOR")]
		public static void DrawHex(Vector3 center, float radius = 1f, float angle = 30f)
		{
#if UNITY_EDITOR
			DrawHex(center, Vector3.forward, Vector2.one * radius, angle);
#endif
		}

		[Conditional("UNITY_EDITOR")]
		public static void DrawHex(Vector3 center, Vector3 normal, float radius = 1f, float angle = 30f)
		{
#if UNITY_EDITOR
			DrawHex(center, normal, Vector2.one * radius, angle);
#endif
		}

		[Conditional("UNITY_EDITOR")]
		public static void DrawLabel(Vector3 position, string text)
		{
#if UNITY_EDITOR
			GUIStyle label = new GUIStyle("Label");
			label.normal.textColor = Handles.color;
			label.hover.textColor = Handles.color;
			Handles.Label(position, text, label);
#endif
		}



		[Conditional("UNITY_EDITOR")]
		public static void DrawHex(Vector3 center, Vector3 normal, Vector2 radius, float angle = 0f)
		{
#if UNITY_EDITOR
			Quaternion look = Quaternion.LookRotation(normal);
			for (int i = 1; i < 7; i++)
			{
				Vector3 corner = ShapeTools.Hex(i - 1, angle);
				Vector3 next = ShapeTools.Hex(i, angle);
				corner.Scale(radius);
				next.Scale(radius);
				corner = look * corner + center;
				next = look * next + center;
				Gizmos.DrawLine(corner, next);
			}
#endif
		}
	}
}
