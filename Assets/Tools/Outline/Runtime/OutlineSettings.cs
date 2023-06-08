#if OUTLINE_URP
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.Outline
{
	public class OutlineSettings : ScriptableObject
	{
		public RenderPassEvent passEvent = RenderPassEvent.AfterRenderingTransparents;
		[ColorUsage(false, true)] public Color color = Color.red * 16;
		[Range(0, 512)] public int width = 1;
		[RenderingLayer] public uint layer = 2;
		public CompareFunction compare = CompareFunction.LessEqual;
	}
}
#endif