// This class is a modification on the class shared publicly by Glenn Powell (glennpow) that can be found here
// http://forum.unity3d.com/threads/free-script-particle-systems-in-ui-screen-space-overlay.406862/

using UnityEngine;
using UnityEngine.UI;

namespace Michsky.UI.Shift
{
	[ExecuteAlways]
	[RequireComponent(typeof(CanvasRenderer))]
	[RequireComponent(typeof(ParticleSystem))]
	public class UIParticleRenderer : MaskableGraphic
	{
		enum UpdateMode
		{
			Update,
			LateUpdate,
			FixedUpdate,
		}

		[SerializeField] private UpdateMode updateMode = UpdateMode.Update;
		private Transform _transform;
		private ParticleSystem pSystem;
		private ParticleSystem.Particle[] particles;
		private readonly UIVertex[] _quad = new UIVertex[4];
		private Vector4 imageUV = Vector4.zero;
		private ParticleSystem.TextureSheetAnimationModule textureSheetAnimation;
		private int textureSheetAnimationFrames;
		private Vector2 textureSheetAnimationFrameSize;
		private ParticleSystemRenderer pRenderer;

		private Material currentMaterial;
		private Texture currentTexture;
		private ParticleSystem.MainModule mainModule;

		public override Texture mainTexture
		{
			get
			{
				return currentTexture;
			}
		}

		protected bool Initialize()
		{
			// initialize members
			if (_transform == null)
			{
				_transform = transform;
			}
			if (pSystem == null)
			{
				if (!TryGetComponent(out pSystem))
					return false;

				mainModule = pSystem.main;

				if (pSystem.main.maxParticles > 14000)
				{
					mainModule.maxParticles = 14000;
				}

				if (pSystem.TryGetComponent(out pRenderer))
					pRenderer.enabled = false;

				Shader foundShader = Shader.Find("UI/Particles/Additive");
				Material pMaterial = new Material(foundShader);

				if (material == null)
					material = pMaterial;

				currentMaterial = material;

				if (currentMaterial && currentMaterial.HasProperty("_MainTex"))
				{
					currentTexture = currentMaterial.mainTexture;
					if (currentTexture == null)
						currentTexture = Texture2D.whiteTexture;
				}

				material = currentMaterial;
				// automatically set scaling
				mainModule.scalingMode = ParticleSystemScalingMode.Hierarchy;
				particles = null;
			}

			if (particles == null)
				particles = new ParticleSystem.Particle[pSystem.main.maxParticles];

			imageUV = new Vector4(0, 0, 1, 1);

			// prepare texture sheet animation
			textureSheetAnimation = pSystem.textureSheetAnimation;
			textureSheetAnimationFrames = 0;
			textureSheetAnimationFrameSize = Vector2.zero;

			if (textureSheetAnimation.enabled)
			{
				textureSheetAnimationFrames = textureSheetAnimation.numTilesX * textureSheetAnimation.numTilesY;
				textureSheetAnimationFrameSize = new Vector2(1f / textureSheetAnimation.numTilesX, 1f / textureSheetAnimation.numTilesY);
			}

			return true;
		}

		protected override void Awake()
		{
			base.Awake();

			if (!Initialize())
				enabled = false;
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				if (!Initialize())
					return;
			}
#endif
			// prepare vertices
			vh.Clear();

			if (!gameObject.activeInHierarchy)
				return;

			Vector2 temp = Vector2.zero;
			Vector2 corner1 = Vector2.zero;
			Vector2 corner2 = Vector2.zero;
			// iterate through current particles
			int count = pSystem.GetParticles(particles);

			for (int i = 0; i < count; ++i)
			{
				ParticleSystem.Particle particle = particles[i];

				// get particle properties
				Vector2 position = mainModule.simulationSpace == ParticleSystemSimulationSpace.Local ? particle.position : _transform.InverseTransformPoint(particle.position);

				float rotation = -particle.rotation * Mathf.Deg2Rad;
				float rotation90 = rotation + Mathf.PI / 2;
				Color32 color = particle.GetCurrentColor(pSystem);
				float size = particle.GetCurrentSize(pSystem) * 0.5f;

				// apply scale
				if (mainModule.scalingMode == ParticleSystemScalingMode.Shape)
					position /= canvas.scaleFactor;

				// apply texture sheet animation
				Vector4 particleUV = imageUV;
				if (textureSheetAnimation.enabled)
				{
					float frameProgress = textureSheetAnimation.frameOverTime.curveMin.Evaluate(1 - particle.remainingLifetime / particle.startLifetime);

					frameProgress = Mathf.Repeat(frameProgress * textureSheetAnimation.cycleCount, 1);
					int frame = 0;

					switch (textureSheetAnimation.animation)
					{

						case ParticleSystemAnimationType.WholeSheet:
							frame = Mathf.FloorToInt(frameProgress * textureSheetAnimationFrames);
							break;

						case ParticleSystemAnimationType.SingleRow:
							frame = Mathf.FloorToInt(frameProgress * textureSheetAnimation.numTilesX);

							int row = textureSheetAnimation.rowIndex;
							//                    if (textureSheetAnimation.useRandomRow) { // FIXME - is this handled internally by rowIndex?
							//                        row = Random.Range(0, textureSheetAnimation.numTilesY, using: particle.randomSeed);
							//                    }
							frame += row * textureSheetAnimation.numTilesX;
							break;

					}

					frame %= textureSheetAnimationFrames;
					particleUV.x = frame % textureSheetAnimation.numTilesX * textureSheetAnimationFrameSize.x;
					particleUV.y = Mathf.FloorToInt(frame / textureSheetAnimation.numTilesX) * textureSheetAnimationFrameSize.y;
					particleUV.z = particleUV.x + textureSheetAnimationFrameSize.x;
					particleUV.w = particleUV.y + textureSheetAnimationFrameSize.y;
				}

				temp.x = particleUV.x;
				temp.y = particleUV.y;

				_quad[0] = UIVertex.simpleVert;
				_quad[0].color = color;
				_quad[0].uv0 = temp;

				temp.x = particleUV.x;
				temp.y = particleUV.w;
				_quad[1] = UIVertex.simpleVert;
				_quad[1].color = color;
				_quad[1].uv0 = temp;

				temp.x = particleUV.z;
				temp.y = particleUV.w;
				_quad[2] = UIVertex.simpleVert;
				_quad[2].color = color;
				_quad[2].uv0 = temp;

				temp.x = particleUV.z;
				temp.y = particleUV.y;
				_quad[3] = UIVertex.simpleVert;
				_quad[3].color = color;
				_quad[3].uv0 = temp;

				if (rotation == 0)
				{
					// no rotation
					corner1.x = position.x - size;
					corner1.y = position.y - size;
					corner2.x = position.x + size;
					corner2.y = position.y + size;

					temp.x = corner1.x;
					temp.y = corner1.y;
					_quad[0].position = temp;
					temp.x = corner1.x;
					temp.y = corner2.y;
					_quad[1].position = temp;
					temp.x = corner2.x;
					temp.y = corner2.y;
					_quad[2].position = temp;
					temp.x = corner2.x;
					temp.y = corner1.y;
					_quad[3].position = temp;
				}

				else
				{
					// apply rotation
					Vector2 right = new Vector2(Mathf.Cos(rotation), Mathf.Sin(rotation)) * size;
					Vector2 up = new Vector2(Mathf.Cos(rotation90), Mathf.Sin(rotation90)) * size;

					_quad[0].position = position - right - up;
					_quad[1].position = position - right + up;
					_quad[2].position = position + right + up;
					_quad[3].position = position + right - up;
				}

				vh.AddUIVertexQuad(_quad);
			}
		}

		void FixedUpdate()
		{
			if (updateMode == UpdateMode.FixedUpdate && Application.isPlaying)
			{
				pSystem.Simulate(Time.fixedUnscaledDeltaTime, false, false, true);
				SetAllDirty();

				if (currentMaterial != null && currentTexture != currentMaterial.mainTexture ||
					 material != null && currentMaterial != null && material.shader != currentMaterial.shader)
				{
					pSystem = null;
					Initialize();
				}
			}
		}

		void Update()
		{
			if (updateMode == UpdateMode.Update && Application.isPlaying)
			{
				pSystem.Simulate(Time.unscaledDeltaTime, false, false, true);
				SetAllDirty();

				if (currentMaterial != null && currentTexture != currentMaterial.mainTexture ||
					 material != null && currentMaterial != null && material.shader != currentMaterial.shader)
				{
					pSystem = null;
					Initialize();
				}
			}
		}

		void LateUpdate()
		{
			if (!Application.isPlaying)
			{
				SetAllDirty();
			}
			else
			{
				if (updateMode == UpdateMode.LateUpdate)
				{
					pSystem.Simulate(Time.unscaledDeltaTime, false, false, true);
					SetAllDirty();

					if (currentMaterial != null && currentTexture != currentMaterial.mainTexture ||
						 material != null && currentMaterial != null && material.shader != currentMaterial.shader)
					{
						pSystem = null;
						Initialize();
					}
				}
			}

			if (material == currentMaterial)
				return;

			pSystem = null;
			Initialize();
		}
	}
}