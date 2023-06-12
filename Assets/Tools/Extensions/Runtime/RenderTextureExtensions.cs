using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD
{
	public static class RenderTextureExtensions
	{
		public static void CopyTo(this RenderTexture source, Texture2D dest)
		{
			RenderTexture prev = RenderTexture.active;
			RenderTexture.active = source;
			dest.ReadPixels(new Rect(0, 0, dest.width, dest.height), 0, 0, source.useMipMap);
			dest.Apply();
			RenderTexture.active = prev;
		}

		public static void CopyTo(this Texture2D source, RenderTexture dest)
		{
			source.Apply();
			Graphics.CopyTexture(source, dest);
		}

		public static Texture2D ToTexture2D(this RenderTexture source)
		{
			int mips = source.mipmapCount;
			TextureCreationFlags flags = mips > 1 ? TextureCreationFlags.MipChain : TextureCreationFlags.None;
			Texture2D dest = new Texture2D(source.width, source.height, source.graphicsFormat, mips, flags);
			source.CopyTo(dest);
			return dest;
		}

		public static RenderTexture ToRenderTexture(this Texture2D source)
		{
			RenderTexture dest = new RenderTexture(source.width, source.height, 0);
			dest.enableRandomWrite = true;
			source.CopyTo(dest);
			return dest;
		}

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
