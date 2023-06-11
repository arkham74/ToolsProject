using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD
{
	public static class RenderTextureExtensions
	{
		public static void Clear(this RenderTexture texture, bool clearDepth, bool clearColor, Color backgroundColor, float depth = 1.0f)
		{
			RenderTexture prev = RenderTexture.active;
			RenderTexture.active = texture;
			GL.Clear(clearDepth, clearColor, backgroundColor, depth);
			RenderTexture.active = prev;
		}

		public static void ClearWithSkybox(this RenderTexture texture, bool clearDepth)
		{
			texture.ClearWithSkybox(clearDepth, Camera.main);
		}

		public static void ClearWithSkybox(this RenderTexture texture, bool clearDepth, Camera camera)
		{
			RenderTexture prev = RenderTexture.active;
			RenderTexture.active = texture;
			GL.ClearWithSkybox(clearDepth, camera);
			RenderTexture.active = prev;
		}
	}
}
