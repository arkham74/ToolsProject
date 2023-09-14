using UnityEngine;

namespace JD
{
	public static class RectTransformExtensions
	{
		/// <summary>
		/// Counts the bounding box corners of the given RectTransform that are visible from the given Camera in screen space.
		/// </summary>
		/// <returns>The amount of bounding box corners that are visible from the Camera.</returns>
		/// <param name="rectTransform">Rect transform.</param>
		/// <param name="camera">Camera.</param>
		private static int CountCornersVisibleFrom(this RectTransform rectTransform, Camera camera)
		{
			Rect screenBounds =
				new Rect(0f, 0f, Screen.width,
					Screen.height); // Screen space bounds (assumes camera renders across the entire screen)
			Vector3[] objectCorners = new Vector3[4];
			rectTransform.GetWorldCorners(objectCorners);

			int visibleCorners = 0;
			Vector3 tempScreenSpaceCorner;                 // Cached
			for (int i = 0; i < objectCorners.Length; i++) // For each corner in rectTransform
			{
				tempScreenSpaceCorner =
					camera.WorldToScreenPoint(objectCorners[i]);    // Transform world space position of corner to screen space
				if (screenBounds.Contains(tempScreenSpaceCorner)) // If the corner is inside the screen
				{
					visibleCorners++;
				}
			}

			return visibleCorners;
		}

		/// <summary>
		/// Determines if this RectTransform is fully visible from the specified camera.
		/// Works by checking if each bounding box corner of this RectTransform is inside the cameras screen space view frustrum.
		/// </summary>
		/// <returns><c>true</c> if is fully visible from the specified camera; otherwise, <c>false</c>.</returns>
		/// <param name="rectTransform">Rect transform.</param>
		/// <param name="camera">Camera.</param>
		public static bool IsFullyVisibleFrom(this RectTransform rectTransform, Camera camera)
		{
			return CountCornersVisibleFrom(rectTransform, camera) == 4; // True if all 4 corners are visible
		}

		/// <summary>
		/// Determines if this RectTransform is at least partially visible from the specified camera.
		/// Works by checking if any bounding box corner of this RectTransform is inside the cameras screen space view frustrum.
		/// </summary>
		/// <returns><c>true</c> if is at least partially visible from the specified camera; otherwise, <c>false</c>.</returns>
		/// <param name="rectTransform">Rect transform.</param>
		/// <param name="camera">Camera.</param>
		public static bool IsVisibleFrom(this RectTransform rectTransform, Camera camera)
		{
			return CountCornersVisibleFrom(rectTransform, camera) > 0; // True if any corners are visible
		}

		public static Vector2 GetCenter(this RectTransform rectTransform)
		{
			return rectTransform.TransformPoint(rectTransform.rect.center);
		}

		public static Vector3 ScreenPointToWorldPointInRectangle(this RectTransform rect, Vector2 screenPoint)
		{
			RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPoint, Camera.main, out Vector3 worldPoint);
			return worldPoint;
		}

		public static Vector3 ScreenPointToLocalPointInRectangle(this RectTransform rect, Vector2 screenPoint)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPoint, Camera.main, out Vector2 localPoint);
			return localPoint;
		}

		public static Vector3 ScreenPointToWorldPointInRectangle(this RectTransform rect, Vector2 screenPoint, Camera cam)
		{
			RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPoint, cam, out Vector3 worldPoint);
			return worldPoint;
		}

		public static Vector3 ScreenPointToLocalPointInRectangle(this RectTransform rect, Vector2 screenPoint, Camera cam)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPoint, cam, out Vector2 localPoint);
			return localPoint;
		}
	}
}