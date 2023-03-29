using System.Diagnostics;
using UnityEngine;

namespace JD
{
	/// <summary>
	/// Debug Extension
	/// 	- Static class that extends Unity's debugging functionallity.
	/// 	- Attempts to mimic Unity's existing debugging behaviour for ease-of-use.
	/// 	- Includes gizmo drawing methods for less memory-intensive debug visualization.
	/// </summary>
	public static partial class GizmosTools
	{
		#region GizmoDrawFunctions

		/// <summary>
		/// 	- Draws a point.
		/// </summary>
		/// <param name='position'>
		/// 	- The point to draw.
		/// </param>
		///  <param name='color'>
		/// 	- The color of the drawn point.
		/// </param>
		/// <param name='scale'>
		/// 	- The size of the drawn point.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawPoint(Vector3 position, Color color, float scale = 1.0f)
		{
			Color oldColor = Gizmos.color;

			Gizmos.color = color;
			Gizmos.DrawRay(position + (Vector3.up * (scale * 0.5f)), -Vector3.up * scale);
			Gizmos.DrawRay(position + (Vector3.right * (scale * 0.5f)), -Vector3.right * scale);
			Gizmos.DrawRay(position + (Vector3.forward * (scale * 0.5f)), -Vector3.forward * scale);

			Gizmos.color = oldColor;
		}

		/// <summary>
		/// 	- Draws a point.
		/// </summary>
		/// <param name='position'>
		/// 	- The point to draw.
		/// </param>
		/// <param name='scale'>
		/// 	- The size of the drawn point.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawPoint(Vector3 position, float scale = 1.0f)
		{
			DrawPoint(position, Color.white, scale);
		}

		/// <summary>
		/// 	- Draws an axis-aligned bounding box.
		/// </summary>
		/// <param name='bounds'>
		/// 	- The bounds to draw.
		/// </param>
		/// <param name='color'>
		/// 	- The color of the bounds.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawBounds(Bounds bounds, Color color)
		{
			Vector3 center = bounds.center;

			float x = bounds.extents.x;
			float y = bounds.extents.y;
			float z = bounds.extents.z;

			Vector3 ruf = center + new Vector3(x, y, z);
			Vector3 rub = center + new Vector3(x, y, -z);
			Vector3 luf = center + new Vector3(-x, y, z);
			Vector3 lub = center + new Vector3(-x, y, -z);

			Vector3 rdf = center + new Vector3(x, -y, z);
			Vector3 rdb = center + new Vector3(x, -y, -z);
			Vector3 lfd = center + new Vector3(-x, -y, z);
			Vector3 lbd = center + new Vector3(-x, -y, -z);

			Color oldColor = Gizmos.color;
			Gizmos.color = color;

			Gizmos.DrawLine(ruf, luf);
			Gizmos.DrawLine(ruf, rub);
			Gizmos.DrawLine(luf, lub);
			Gizmos.DrawLine(rub, lub);

			Gizmos.DrawLine(ruf, rdf);
			Gizmos.DrawLine(rub, rdb);
			Gizmos.DrawLine(luf, lfd);
			Gizmos.DrawLine(lub, lbd);

			Gizmos.DrawLine(rdf, lfd);
			Gizmos.DrawLine(rdf, rdb);
			Gizmos.DrawLine(lfd, lbd);
			Gizmos.DrawLine(lbd, rdb);

			Gizmos.color = oldColor;
		}

		/// <summary>
		/// 	- Draws an axis-aligned bounding box.
		/// </summary>
		/// <param name='bounds'>
		/// 	- The bounds to draw.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawBounds(Bounds bounds)
		{
			DrawBounds(bounds, Color.white);
		}

		/// <summary>
		/// 	- Draws a local cube.
		/// </summary>
		/// <param name='transform'>
		/// 	- The transform the cube will be local to.
		/// </param>
		/// <param name='size'>
		/// 	- The local size of the cube.
		/// </param>
		/// <param name='center'>
		///		- The local position of the cube.
		/// </param>
		/// <param name='color'>
		/// 	- The color of the cube.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawLocalCube(Transform transform, Vector3 size, Color color, Vector3 center = default(Vector3))
		{
			Color oldColor = Gizmos.color;
			Gizmos.color = color;

			Vector3 lbb = transform.TransformPoint(center + ((-size) * 0.5f));
			Vector3 rbb = transform.TransformPoint(center + (new Vector3(size.x, -size.y, -size.z) * 0.5f));

			Vector3 lbf = transform.TransformPoint(center + (new Vector3(size.x, -size.y, size.z) * 0.5f));
			Vector3 rbf = transform.TransformPoint(center + (new Vector3(-size.x, -size.y, size.z) * 0.5f));

			Vector3 lub = transform.TransformPoint(center + (new Vector3(-size.x, size.y, -size.z) * 0.5f));
			Vector3 rub = transform.TransformPoint(center + (new Vector3(size.x, size.y, -size.z) * 0.5f));

			Vector3 luf = transform.TransformPoint(center + ((size) * 0.5f));
			Vector3 ruf = transform.TransformPoint(center + (new Vector3(-size.x, size.y, size.z) * 0.5f));

			Gizmos.DrawLine(lbb, rbb);
			Gizmos.DrawLine(rbb, lbf);
			Gizmos.DrawLine(lbf, rbf);
			Gizmos.DrawLine(rbf, lbb);

			Gizmos.DrawLine(lub, rub);
			Gizmos.DrawLine(rub, luf);
			Gizmos.DrawLine(luf, ruf);
			Gizmos.DrawLine(ruf, lub);

			Gizmos.DrawLine(lbb, lub);
			Gizmos.DrawLine(rbb, rub);
			Gizmos.DrawLine(lbf, luf);
			Gizmos.DrawLine(rbf, ruf);

			Gizmos.color = oldColor;
		}

		/// <summary>
		/// 	- Draws a local cube.
		/// </summary>
		/// <param name='transform'>
		/// 	- The transform the cube will be local to.
		/// </param>
		/// <param name='size'>
		/// 	- The local size of the cube.
		/// </param>
		/// <param name='center'>
		///		- The local position of the cube.
		/// </param>	
		[Conditional("UNITY_EDITOR")]
		public static void DrawLocalCube(Transform transform, Vector3 size, Vector3 center = default(Vector3))
		{
			DrawLocalCube(transform, size, Color.white, center);
		}

		/// <summary>
		/// 	- Draws a local cube.
		/// </summary>
		/// <param name='space'>
		/// 	- The space the cube will be local to.
		/// </param>
		/// <param name='size'>
		/// 	- The local size of the cube.
		/// </param>
		/// <param name='center'>
		/// 	- The local position of the cube.
		/// </param>
		/// <param name='color'>
		/// 	- The color of the cube.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawLocalCube(Matrix4x4 space, Vector3 size, Color color, Vector3 center = default(Vector3))
		{
			Color oldColor = Gizmos.color;
			Gizmos.color = color;

			Vector3 lbb = space.MultiplyPoint3x4(center + ((-size) * 0.5f));
			Vector3 rbb = space.MultiplyPoint3x4(center + (new Vector3(size.x, -size.y, -size.z) * 0.5f));

			Vector3 lbf = space.MultiplyPoint3x4(center + (new Vector3(size.x, -size.y, size.z) * 0.5f));
			Vector3 rbf = space.MultiplyPoint3x4(center + (new Vector3(-size.x, -size.y, size.z) * 0.5f));

			Vector3 lub = space.MultiplyPoint3x4(center + (new Vector3(-size.x, size.y, -size.z) * 0.5f));
			Vector3 rub = space.MultiplyPoint3x4(center + (new Vector3(size.x, size.y, -size.z) * 0.5f));

			Vector3 luf = space.MultiplyPoint3x4(center + ((size) * 0.5f));
			Vector3 ruf = space.MultiplyPoint3x4(center + (new Vector3(-size.x, size.y, size.z) * 0.5f));

			Gizmos.DrawLine(lbb, rbb);
			Gizmos.DrawLine(rbb, lbf);
			Gizmos.DrawLine(lbf, rbf);
			Gizmos.DrawLine(rbf, lbb);

			Gizmos.DrawLine(lub, rub);
			Gizmos.DrawLine(rub, luf);
			Gizmos.DrawLine(luf, ruf);
			Gizmos.DrawLine(ruf, lub);

			Gizmos.DrawLine(lbb, lub);
			Gizmos.DrawLine(rbb, rub);
			Gizmos.DrawLine(lbf, luf);
			Gizmos.DrawLine(rbf, ruf);

			Gizmos.color = oldColor;
		}

		/// <summary>
		/// 	- Draws a local cube.
		/// </summary>
		/// <param name='space'>
		/// 	- The space the cube will be local to.
		/// </param>
		/// <param name='size'>
		/// 	- The local size of the cube.
		/// </param>
		/// <param name='center'>
		/// 	- The local position of the cube.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawLocalCube(Matrix4x4 space, Vector3 size, Vector3 center = default(Vector3))
		{
			DrawLocalCube(space, size, Color.white, center);
		}

		/// <summary>
		/// 	- Draws a circle.
		/// </summary>
		/// <param name='position'>
		/// 	- Where the center of the circle will be positioned.
		/// </param>
		/// <param name='up'>
		/// 	- The direction perpendicular to the surface of the circle.
		/// </param>
		/// <param name='color'>
		/// 	- The color of the circle.
		/// </param>
		/// <param name='radius'>
		/// 	- The radius of the circle.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawCircle(Vector3 position, Vector3 up, Color color, float radius = 1.0f)
		{
			up = ((up == Vector3.zero) ? Vector3.up : up).normalized * radius;
			Vector3 _forward = Vector3.Slerp(up, -up, 0.5f);
			Vector3 _right = Vector3.Cross(up, _forward).normalized * radius;

			Matrix4x4 matrix = new Matrix4x4();

			matrix[0] = _right.x;
			matrix[1] = _right.y;
			matrix[2] = _right.z;

			matrix[4] = up.x;
			matrix[5] = up.y;
			matrix[6] = up.z;

			matrix[8] = _forward.x;
			matrix[9] = _forward.y;
			matrix[10] = _forward.z;

			Vector3 _lastPoint = position + matrix.MultiplyPoint3x4(new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0)));
			Vector3 _nextPoint = Vector3.zero;

			Color oldColor = Gizmos.color;
			Gizmos.color = (color == default(Color)) ? Color.white : color;

			for (var i = 0; i < 91; i++)
			{
				_nextPoint.x = Mathf.Cos((i * 4) * Mathf.Deg2Rad);
				_nextPoint.z = Mathf.Sin((i * 4) * Mathf.Deg2Rad);
				_nextPoint.y = 0;

				_nextPoint = position + matrix.MultiplyPoint3x4(_nextPoint);

				Gizmos.DrawLine(_lastPoint, _nextPoint);
				_lastPoint = _nextPoint;
			}

			Gizmos.color = oldColor;
		}

		/// <summary>
		/// 	- Draws a circle.
		/// </summary>
		/// <param name='position'>
		/// 	- Where the center of the circle will be positioned.
		/// </param>
		/// <param name='color'>
		/// 	- The color of the circle.
		/// </param>
		/// <param name='radius'>
		/// 	- The radius of the circle.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawCircle(Vector3 position, Color color, float radius = 1.0f)
		{
			DrawCircle(position, Vector3.up, color, radius);
		}

		/// <summary>
		/// 	- Draws a circle.
		/// </summary>
		/// <param name='position'>
		/// 	- Where the center of the circle will be positioned.
		/// </param>
		/// <param name='up'>
		/// 	- The direction perpendicular to the surface of the circle.
		/// </param>
		/// <param name='radius'>
		/// 	- The radius of the circle.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawCircle(Vector3 position, Vector3 up, float radius = 1.0f)
		{
			DrawCircle(position, position, Color.white, radius);
		}

		/// <summary>
		/// 	- Draws a circle.
		/// </summary>
		/// <param name='position'>
		/// 	- Where the center of the circle will be positioned.
		/// </param>
		/// <param name='radius'>
		/// 	- The radius of the circle.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawCircle(Vector3 position, float radius = 1.0f)
		{
			DrawCircle(position, Vector3.up, Color.white, radius);
		}

		//Wiresphere already exists

		/// <summary>
		/// 	- Draws a cylinder.
		/// </summary>
		/// <param name='start'>
		/// 	- The position of one end of the cylinder.
		/// </param>
		/// <param name='end'>
		/// 	- The position of the other end of the cylinder.
		/// </param>
		/// <param name='color'>
		/// 	- The color of the cylinder.
		/// </param>
		/// <param name='radius'>
		/// 	- The radius of the cylinder.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawCylinder(Vector3 start, Vector3 end, Color color, float radius = 1.0f)
		{
			Vector3 up = (end - start).normalized * radius;
			Vector3 forward = Vector3.Slerp(up, -up, 0.5f);
			Vector3 right = Vector3.Cross(up, forward).normalized * radius;

			//Radial circles
			DrawCircle(start, up, color, radius);
			DrawCircle(end, -up, color, radius);
			DrawCircle((start + end) * 0.5f, up, color, radius);

			Color oldColor = Gizmos.color;
			Gizmos.color = color;

			//Side lines
			Gizmos.DrawLine(start + right, end + right);
			Gizmos.DrawLine(start - right, end - right);

			Gizmos.DrawLine(start + forward, end + forward);
			Gizmos.DrawLine(start - forward, end - forward);

			//Start endcap
			Gizmos.DrawLine(start - right, start + right);
			Gizmos.DrawLine(start - forward, start + forward);

			//End endcap
			Gizmos.DrawLine(end - right, end + right);
			Gizmos.DrawLine(end - forward, end + forward);

			Gizmos.color = oldColor;
		}

		/// <summary>
		/// 	- Draws a cylinder.
		/// </summary>
		/// <param name='start'>
		/// 	- The position of one end of the cylinder.
		/// </param>
		/// <param name='end'>
		/// 	- The position of the other end of the cylinder.
		/// </param>
		/// <param name='radius'>
		/// 	- The radius of the cylinder.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawCylinder(Vector3 start, Vector3 end, float radius = 1.0f)
		{
			DrawCylinder(start, end, Color.white, radius);
		}

		/// <summary>
		/// 	- Draws a cone.
		/// </summary>
		/// <param name='position'>
		/// 	- The position for the tip of the cone.
		/// </param>
		/// <param name='direction'>
		/// 	- The direction for the cone to get wider in.
		/// </param>
		/// <param name='color'>
		/// 	- The color of the cone.
		/// </param>
		/// <param name='angle'>
		/// 	- The angle of the cone.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawCone(Vector3 position, Vector3 direction, Color color, float angle = 45)
		{
			float length = direction.magnitude;

			Vector3 _forward = direction;
			Vector3 _up = Vector3.Slerp(_forward, -_forward, 0.5f);
			Vector3 _right = Vector3.Cross(_forward, _up).normalized * length;

			direction = direction.normalized;

			Vector3 slerpedVector = Vector3.Slerp(_forward, _up, angle / 90.0f);

			float dist;
			var farPlane = new Plane(-direction, position + _forward);
			var distRay = new Ray(position, slerpedVector);

			farPlane.Raycast(distRay, out dist);

			Color oldColor = Gizmos.color;
			Gizmos.color = color;

			Gizmos.DrawRay(position, slerpedVector.normalized * dist);
			Gizmos.DrawRay(position, Vector3.Slerp(_forward, -_up, angle / 90.0f).normalized * dist);
			Gizmos.DrawRay(position, Vector3.Slerp(_forward, _right, angle / 90.0f).normalized * dist);
			Gizmos.DrawRay(position, Vector3.Slerp(_forward, -_right, angle / 90.0f).normalized * dist);

			DrawCircle(position + _forward, direction, color, (_forward - (slerpedVector.normalized * dist)).magnitude);
			DrawCircle(position + (_forward * 0.5f), direction, color, ((_forward * 0.5f) - (slerpedVector.normalized * (dist * 0.5f))).magnitude);

			Gizmos.color = oldColor;
		}

		/// <summary>
		/// 	- Draws a cone.
		/// </summary>
		/// <param name='position'>
		/// 	- The position for the tip of the cone.
		/// </param>
		/// <param name='direction'>
		/// 	- The direction for the cone to get wider in.
		/// </param>
		/// <param name='angle'>
		/// 	- The angle of the cone.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawCone(Vector3 position, Vector3 direction, float angle = 45)
		{
			DrawCone(position, direction, Color.white, angle);
		}

		/// <summary>
		/// 	- Draws a cone.
		/// </summary>
		/// <param name='position'>
		/// 	- The position for the tip of the cone.
		/// </param>
		/// <param name='color'>
		/// 	- The color of the cone.
		/// </param>
		/// <param name='angle'>
		/// 	- The angle of the cone.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawCone(Vector3 position, Color color, float angle = 45)
		{
			DrawCone(position, Vector3.up, color, angle);
		}

		/// <summary>
		/// 	- Draws a cone.
		/// </summary>
		/// <param name='position'>
		/// 	- The position for the tip of the cone.
		/// </param>
		/// <param name='angle'>
		/// 	- The angle of the cone.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawCone(Vector3 position, float angle = 45)
		{
			DrawCone(position, Vector3.up, Color.white, angle);
		}

		/// <summary>
		/// 	- Draws an arrow.
		/// </summary>
		/// <param name='position'>
		/// 	- The start position of the arrow.
		/// </param>
		/// <param name='direction'>
		/// 	- The direction the arrow will point in.
		/// </param>
		/// <param name='color'>
		/// 	- The color of the arrow.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawArrow(Vector3 position, Vector3 direction, Color color)
		{
			Color oldColor = Gizmos.color;
			Gizmos.color = color;

			Gizmos.DrawRay(position, direction);
			DrawCone(position + direction, -direction * 0.333f, color, 15);

			Gizmos.color = oldColor;
		}

		/// <summary>
		/// 	- Draws an arrow.
		/// </summary>
		/// <param name='position'>
		/// 	- The start position of the arrow.
		/// </param>
		/// <param name='direction'>
		/// 	- The direction the arrow will point in.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawArrow(Vector3 position, Vector3 direction)
		{
			DrawArrow(position, direction, Color.white);
		}

		/// <summary>
		/// 	- Draws a capsule.
		/// </summary>
		/// <param name='start'>
		/// 	- The position of one end of the capsule.
		/// </param>
		/// <param name='end'>
		/// 	- The position of the other end of the capsule.
		/// </param>
		/// <param name='color'>
		/// 	- The color of the capsule.
		/// </param>
		/// <param name='radius'>
		/// 	- The radius of the capsule.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawCapsule(Vector3 start, Vector3 end, Color color, float radius = 1)
		{
			Vector3 up = (end - start).normalized * radius;
			Vector3 forward = Vector3.Slerp(up, -up, 0.5f);
			Vector3 right = Vector3.Cross(up, forward).normalized * radius;

			Color oldColor = Gizmos.color;
			Gizmos.color = color;

			float height = (start - end).magnitude;
			float sideLength = Mathf.Max(0, (height * 0.5f) - radius);
			Vector3 middle = (end + start) * 0.5f;

			start = middle + ((start - middle).normalized * sideLength);
			end = middle + ((end - middle).normalized * sideLength);

			//Radial circles
			DrawCircle(start, up, color, radius);
			DrawCircle(end, -up, color, radius);

			//Side lines
			Gizmos.DrawLine(start + right, end + right);
			Gizmos.DrawLine(start - right, end - right);

			Gizmos.DrawLine(start + forward, end + forward);
			Gizmos.DrawLine(start - forward, end - forward);

			for (int i = 1; i < 26; i++)
			{

				//Start endcap
				Gizmos.DrawLine(Vector3.Slerp(right, -up, i / 25.0f) + start, Vector3.Slerp(right, -up, (i - 1) / 25.0f) + start);
				Gizmos.DrawLine(Vector3.Slerp(-right, -up, i / 25.0f) + start, Vector3.Slerp(-right, -up, (i - 1) / 25.0f) + start);
				Gizmos.DrawLine(Vector3.Slerp(forward, -up, i / 25.0f) + start, Vector3.Slerp(forward, -up, (i - 1) / 25.0f) + start);
				Gizmos.DrawLine(Vector3.Slerp(-forward, -up, i / 25.0f) + start, Vector3.Slerp(-forward, -up, (i - 1) / 25.0f) + start);

				//End endcap
				Gizmos.DrawLine(Vector3.Slerp(right, up, i / 25.0f) + end, Vector3.Slerp(right, up, (i - 1) / 25.0f) + end);
				Gizmos.DrawLine(Vector3.Slerp(-right, up, i / 25.0f) + end, Vector3.Slerp(-right, up, (i - 1) / 25.0f) + end);
				Gizmos.DrawLine(Vector3.Slerp(forward, up, i / 25.0f) + end, Vector3.Slerp(forward, up, (i - 1) / 25.0f) + end);
				Gizmos.DrawLine(Vector3.Slerp(-forward, up, i / 25.0f) + end, Vector3.Slerp(-forward, up, (i - 1) / 25.0f) + end);
			}

			Gizmos.color = oldColor;
		}

		/// <summary>
		/// 	- Draws a capsule.
		/// </summary>
		/// <param name='start'>
		/// 	- The position of one end of the capsule.
		/// </param>
		/// <param name='end'>
		/// 	- The position of the other end of the capsule.
		/// </param>
		/// <param name='radius'>
		/// 	- The radius of the capsule.
		/// </param>
		[Conditional("UNITY_EDITOR")]
		public static void DrawCapsule(Vector3 start, Vector3 end, float radius = 1)
		{
			DrawCapsule(start, end, Color.white, radius);
		}
		#endregion
	}
}