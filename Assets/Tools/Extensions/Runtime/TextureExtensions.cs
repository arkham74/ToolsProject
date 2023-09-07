using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD
{
	public static class TextureExtensions
	{
		public static Texture2D ResizePointNonAlloc(this Texture2D source, int width, int height)
		{
			RenderTextureDescriptor descriptor = new RenderTextureDescriptor(width, height, RenderTextureFormat.ARGB32, 0);
			RenderTexture renderTexture = RenderTexture.GetTemporary(descriptor);
			Graphics.Blit(source, renderTexture);
			source.Reinitialize(width, height);
			renderTexture.CopyTo(source);
			RenderTexture.ReleaseTemporary(renderTexture);
			return source;
		}

		public static Texture2D ResizeBilinearNonAlloc(this Texture2D source, int width, int height)
		{
			RenderTextureDescriptor smallDescriptor = new RenderTextureDescriptor(width, height, RenderTextureFormat.ARGB32, 0);
			RenderTextureDescriptor bigDescriptor = new RenderTextureDescriptor(source.width, source.height, RenderTextureFormat.ARGB32, 0)
			{
				useMipMap = true,
				autoGenerateMips = true
			};
			RenderTexture smallRenderTexture = RenderTexture.GetTemporary(smallDescriptor);
			RenderTexture bigRenderTexture = RenderTexture.GetTemporary(bigDescriptor);
			Graphics.Blit(source, bigRenderTexture);
			Graphics.Blit(bigRenderTexture, smallRenderTexture);
			source.Reinitialize(width, height);
			smallRenderTexture.CopyTo(source);
			RenderTexture.ReleaseTemporary(smallRenderTexture);
			RenderTexture.ReleaseTemporary(bigRenderTexture);
			return source;
		}

		public static Texture2D ResizePoint(this Texture2D source, int width, int height)
		{
			RenderTextureDescriptor descriptor = new RenderTextureDescriptor(width, height, RenderTextureFormat.ARGB32, 0);
			RenderTexture renderTexture = RenderTexture.GetTemporary(descriptor);
			Graphics.Blit(source, renderTexture);
			Texture2D destination = renderTexture.ToTexture2D();
			RenderTexture.ReleaseTemporary(renderTexture);
			return destination;
		}

		public static Texture2D ResizeBilinear(this Texture2D source, int width, int height)
		{
			RenderTextureDescriptor smallDescriptor = new RenderTextureDescriptor(width, height, RenderTextureFormat.ARGB32, 0);
			RenderTextureDescriptor bigDescriptor = new RenderTextureDescriptor(source.width, source.height, RenderTextureFormat.ARGB32, 0)
			{
				useMipMap = true,
				autoGenerateMips = true
			};
			RenderTexture smallRenderTexture = RenderTexture.GetTemporary(smallDescriptor);
			RenderTexture bigRenderTexture = RenderTexture.GetTemporary(bigDescriptor);
			Graphics.Blit(source, bigRenderTexture);
			Graphics.Blit(bigRenderTexture, smallRenderTexture);
			Texture2D destination = smallRenderTexture.ToTexture2D();
			RenderTexture.ReleaseTemporary(smallRenderTexture);
			RenderTexture.ReleaseTemporary(bigRenderTexture);
			return destination;
		}

		public static Texture2D ToTexture2D(this RenderTexture source)
		{
			int mips = source.mipmapCount;
			TextureCreationFlags flags = mips > 1 ? TextureCreationFlags.MipChain : TextureCreationFlags.None;
			Texture2D dest = new Texture2D(source.width, source.height, source.graphicsFormat, mips, flags);
			source.CopyTo(dest);
			return dest;
		}

		public static RenderTexture ToRenderTexture(this Texture2D source, DefaultFormat format = DefaultFormat.HDR)
		{
			RenderTexture dest = new RenderTexture(source.width, source.height, 0, format)
			{
				enableRandomWrite = true
			};
			source.BlitTo(dest);
			return dest;
		}

		public static void BlitTo(this Texture2D source, RenderTexture dest)
		{
			Graphics.Blit(source, dest);
		}

		public static void CopyTo(this RenderTexture source, Texture2D dest)
		{
			RenderTexture prev = RenderTexture.active;
			RenderTexture.active = source;
			dest.ReadPixels(new Rect(0, 0, dest.width, dest.height), 0, 0, source.useMipMap);
			dest.Apply();
			RenderTexture.active = prev;
		}

		public static Texture2D Clone(this Texture2D source)
		{
			Texture2D clone = new Texture2D(source.width, source.height, source.format, source.mipmapCount, false);
			source.CopyTo(clone);
			return clone;
		}

		public static Texture2D Clone(this Texture2D source, TextureFormat format)
		{
			return Clone(source, format, false);
		}

		public static Texture2D CloneNonReadable(this Texture2D source, TextureFormat format)
		{
			return Clone(source, format, true);
		}

		private static Texture2D Clone(Texture2D source, TextureFormat format, bool nonReadable)
		{
			if (source.format == format)
			{
				return source.Clone();
			}

			if (source.isReadable)
			{
				Texture2D clone = new Texture2D(source.width, source.height, format, source.mipmapCount, false);
				clone.SetPixels32(source.GetPixels32());
				return clone;
			}

			if (!nonReadable)
			{
				Debug.LogWarning($"Enable Read/Write in \"{source}\" import settings for better performance or use \"CloneNonReadable()\" to skip this warning", source);
			}

			return source.ToRenderTexture(DefaultFormat.LDR).ToTexture2D();
		}

		public static void CopyTo(this Texture2D source, Texture2D dest)
		{
			Graphics.CopyTexture(source, dest);
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

		public static void Clear(this Texture2D texture2D)
		{
			Clear(texture2D, Color.clear);
		}

		public static void Clear(this Texture2D texture2D, Color color)
		{
			NativeArray<Color32> colors = texture2D.GetPixelData<Color32>(0);
			ClearJob clearJob = new ClearJob(colors, color);
			clearJob.Schedule(colors.Length, 64).Complete();
			texture2D.Apply();
		}

		[BurstCompile]
		private struct ClearJob : IJobParallelFor
		{
			[WriteOnly] private NativeArray<Color32> colors;
			private readonly Color color;

			public ClearJob(NativeArray<Color32> colors, Color color)
			{
				this.colors = colors;
				this.color = color;
			}

			public void Execute(int index)
			{
				colors[index] = color;
			}
		}
	}
}