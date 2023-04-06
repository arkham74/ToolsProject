using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JD.PathTrace
{
	[VolumeComponentMenuForRenderPipeline("Path Trace", typeof(UniversalRenderPipeline))]
	public class PathTraceVolumeComponent : VolumeComponent
	{
		public BoolParameter scene = new BoolParameter(true);
		public MinIntParameter samples = new MinIntParameter(6, 0);
		public MinIntParameter bounces = new MinIntParameter(3, 0);
	}
}