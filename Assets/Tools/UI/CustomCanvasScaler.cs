using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.UI.Extensions
{
	public class CustomCanvasScaler : CanvasScaler
	{
		private Canvas rootCanvas;
		private const float K_LOG_BASE = 2;

		protected override void OnEnable()
		{
			rootCanvas = GetComponent<Canvas>();
			base.OnEnable();
		}

		protected override void HandleScaleWithScreenSize()
		{
			Camera wcam = rootCanvas.worldCamera;
			Vector2 screenSize = wcam != null
				? new Vector2(wcam.pixelWidth, wcam.pixelHeight)
				: new Vector2(Screen.width, Screen.height);

			// Multiple display support only when not the main display. For display 0 the reported
			// resolution is always the desktops resolution since its part of the display API,
			// so we use the standard none multiple display method. (case 741751)
			int displayIndex = rootCanvas.targetDisplay;
			if (displayIndex > 0 && displayIndex < Display.displays.Length)
			{
				Display display = Display.displays[displayIndex];
				screenSize = new Vector2(display.renderingWidth, display.renderingHeight);
			}

			float factor;
			switch (m_ScreenMatchMode)
			{
				case ScreenMatchMode.MatchWidthOrHeight:
					{
						// We take the log of the relative width and height before taking the average.
						// Then we transform it back in the original space.
						// the reason to transform in and out of logarithmic space is to have better behavior.
						// If one axis has twice resolution and the other has half, it should even out if widthOrHeight value is at 0.5.
						// In normal space the average would be (0.5 + 2) / 2 = 1.25
						// In logarithmic space the average is (-1 + 1) / 2 = 0
						float logWidth = Mathf.Log(screenSize.x / m_ReferenceResolution.x, K_LOG_BASE);
						float logHeight = Mathf.Log(screenSize.y / m_ReferenceResolution.y, K_LOG_BASE);
						float logWeightedAverage = Mathf.Lerp(logWidth, logHeight, m_MatchWidthOrHeight);
						factor = Mathf.Pow(K_LOG_BASE, logWeightedAverage);
						break;
					}
				case ScreenMatchMode.Expand:
					{
						factor = Mathf.Min(screenSize.x / m_ReferenceResolution.x, screenSize.y / m_ReferenceResolution.y);
						break;
					}
				case ScreenMatchMode.Shrink:
					{
						factor = Mathf.Max(screenSize.x / m_ReferenceResolution.x, screenSize.y / m_ReferenceResolution.y);
						break;
					}
				default:
					throw new ArgumentOutOfRangeException();
			}

			SetScaleFactor(factor);
			SetReferencePixelsPerUnit(m_ReferencePixelsPerUnit);
		}
	}
}