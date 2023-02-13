// using System;
// using UnityEngine;
// using UnityEngine.Rendering;
// using UnityEngine.Rendering.Universal;

// namespace JD.PlanarReflection
// {
// 	public class PlanarReflectionFeature : ScriptableRendererFeature
// 	{
// 		[SerializeField]
// 		private PlanarReflectionSettings settings = new PlanarReflectionSettings()
// 		{
// 			clipPlaneOffset = 0.07f,
// 			renderPassEvent = RenderPassEvent.BeforeRenderingOpaques,
// 			sortingCriteria = SortingCriteria.CommonOpaque,
// 		};

// 		private PlanarReflectionPass pass;

// 		public override void Create()
// 		{
// 			pass = new PlanarReflectionPass(settings, name);
// 		}

// 		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
// 		{
// 			renderer.EnqueuePass(pass);
// 		}
// 	}
// }


