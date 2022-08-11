using System;
using System.Collections;
using System.Collections.Generic;
using JD;
using UnityEngine;

namespace SebastianLague
{
	public static class CylinderGenerator
	{
		private struct PathVertex
		{
			public Vector3 tangent;
			public Vector3 normal;
		}

		public static void CreateMesh(ref Mesh mesh, Span<Vector3> points, int resolutionU = 10, float thickness = 2)
		{
			CreateMesh(ref mesh, points, Color.red, resolutionU, thickness);
		}

		public static void CreateMesh(ref Mesh mesh, Span<Vector3> points, Color color, int resolutionU = 10, float thickness = 2)
		{
			Gradient gradient = new Gradient();

			GradientColorKey[] colorKeys = new GradientColorKey[]
			{
			new GradientColorKey()
			{
				color = color,
				time = 0
			},
			new GradientColorKey()
			{
				color = color,
				time = 1
			}
			};

			GradientAlphaKey[] alphaKeys = new GradientAlphaKey[]
			{
			new GradientAlphaKey()
			{
				alpha = color.a,
				time = 0
			},
			new GradientAlphaKey()
			{
				alpha = color.a,
				time = 1
			}
			};

			gradient.SetKeys(colorKeys, alphaKeys);
			CreateMesh(ref mesh, points, gradient, resolutionU, thickness);
		}

		public static void CreateMesh(ref Mesh mesh, Span<Vector3> points, Gradient gradient, int resolutionU = 10, float thickness = 2)
		{
			int numCircles = points.Length;

			int[] triangles = new int[(numCircles - 1) * resolutionU * 6];
			Vector3[] verts = new Vector3[numCircles * resolutionU];
			Vector2[] uvs = new Vector2[numCircles * resolutionU];
			Vector2[] uvs2 = new Vector2[numCircles * resolutionU];
			Color[] colors = new Color[numCircles * resolutionU];

			Span<PathVertex> v = stackalloc PathVertex[points.Length];
			CalcNormals(points, ref v);

			float len = points.PathLength();
			int index = 0;
			int index2 = 0;
			for (int s = 0; s < numCircles; s++)
			{
				float segmentPercent = s / (numCircles - 1f);
				Vector2 uv = new Vector2(segmentPercent, 0);
				Vector2 uv2 = new Vector2(segmentPercent * len, 0);
				Vector3 centerPos = points[s];
				Vector3 forward = v[s].tangent;
				Vector3 norm = v[s].normal;
				Vector3 cross = Vector3.Cross(norm, forward).normalized;
				Color color = gradient.Evaluate(segmentPercent);

				for (int currentRes = 0; currentRes < resolutionU; currentRes++)
				{
					float t = currentRes / (resolutionU - 1f);
					float angle = t * Mathf.PI * 2.0f;
					uv.y = t;
					uv2.y = t;

					float xVal = Mathf.Sin(angle) * thickness;
					float yVal = Mathf.Cos(angle) * thickness;

					Vector3 point = (norm * xVal) + (cross * yVal) + centerPos;
					colors[index] = color;
					verts[index] = point;
					uvs[index] = uv;
					uvs2[index] = uv2;
					index++;

					//! Adding the triangles
					if (s < numCircles - 1)
					{
						int startIndex = resolutionU * s;
						triangles[index2] = startIndex + currentRes;
						index2++;
						triangles[index2] = startIndex + (currentRes + 1) % resolutionU;
						index2++;
						triangles[index2] = startIndex + currentRes + resolutionU;
						index2++;

						triangles[index2] = startIndex + (currentRes + 1) % resolutionU;
						index2++;
						triangles[index2] = startIndex + (currentRes + 1) % resolutionU + resolutionU;
						index2++;
						triangles[index2] = startIndex + currentRes + resolutionU;
						index2++;
					}

				}
			}

			if (mesh == null)
			{
				mesh = new Mesh
				{
					name = "Cylinder Mesh"
				};
			}
			else
			{
				mesh.Clear();
			}

			mesh.SetVertices(verts);
			mesh.SetTriangles(triangles, 0);
			mesh.SetUVs(0, uvs);
			mesh.SetUVs(1, uvs2);
			mesh.SetColors(colors);
			mesh.RecalculateNormals();
		}

		private static void CalcNormals(Span<Vector3> localPoints, ref Span<PathVertex> verts)
		{
			Vector3 lastRotationAxis = Vector3.up;

			// Loop through the data and assign to arrays.
			for (int i = 0; i < localPoints.Length; i++)
			{
				Vector3 tangent;
				Vector3 normal;

				if (i == 0)
				{
					tangent = (localPoints[i + 1] - localPoints[i]).normalized;
				}
				else
				{
					tangent = i == localPoints.Length - 1
							? (localPoints[i] - localPoints[i - 1]).normalized
							: ((localPoints[i + 1] - localPoints[i]).normalized + (localPoints[i] - localPoints[i - 1]).normalized).normalized;
				}

				// Calculate normals
				if (i == 0)
				{
					lastRotationAxis = (Vector3.Dot(tangent, Vector3.up) > 0.5f) ? -Vector3.forward : Vector3.up;
					normal = Vector3.Cross(lastRotationAxis, tangent).normalized;
				}
				else
				{
					// First reflection
					Vector3 offset = (localPoints[i] - localPoints[i - 1]);
					float sqrDst = offset.sqrMagnitude;
					Vector3 r = lastRotationAxis - offset * 2 / sqrDst * Vector3.Dot(offset, lastRotationAxis);
					Vector3 t = verts[i - 1].tangent - offset * 2 / sqrDst * Vector3.Dot(offset, verts[i - 1].tangent);

					// Second reflection
					Vector3 v2 = tangent - t;
					float c2 = Vector3.Dot(v2, v2);

					Vector3 finalRot = r - v2 * 2 / c2 * Vector3.Dot(v2, r);
					Vector3 n = Vector3.Cross(finalRot, tangent).normalized;
					normal = n;
					lastRotationAxis = finalRot;
				}

				verts[i].tangent = tangent;
				verts[i].normal = normal;
			}
		}
	}
}