﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Coffee.UISoftMask
{
	/// <summary>
	/// Soft mask.
	/// Use instead of Mask for smooth masking.
	/// </summary>
	public class SoftMask : Mask, IMeshModifier
	{
		/// <summary>
		/// Down sampling rate.
		/// </summary>
		public enum DownSampling
		{
			None = 0,
			x1 = 1,
			x2 = 2,
			x4 = 4,
			x8 = 8,
		}

		private static readonly List<SoftMask>[] s_TmpSoftMasks = new List<SoftMask>[]
		{
				new List<SoftMask>(),
				new List<SoftMask>(),
				new List<SoftMask>(),
				new List<SoftMask>(),
		};

		private static readonly Color[] s_ClearColors = new Color[]
		{
				new Color(0, 0, 0, 0),
				new Color(1, 0, 0, 0),
				new Color(1, 1, 0, 0),
				new Color(1, 1, 1, 0),
		};

		private static bool s_UVStartsAtTop;
		private static bool s_IsMetal;
		private static Shader s_SoftMaskShader;
		private static Texture2D s_ReadTexture;
		private static readonly List<SoftMask> s_ActiveSoftMasks = new List<SoftMask>();
		private static readonly List<SoftMask> s_TempRelatables = new List<SoftMask>();
		private static readonly Dictionary<int, Matrix4x4> s_PreviousViewProjectionMatrices = new Dictionary<int, Matrix4x4>();
		private static readonly Dictionary<int, Matrix4x4> s_NowViewProjectionMatrices = new Dictionary<int, Matrix4x4>();
		private static int s_StencilCompId;
		private static int s_ColorMaskId;
		private static int s_MainTexId;
		private static int s_SoftnessId;
		private static int s_Alpha;
		private static int s_PreviousWidth;
		private static int s_PreviousHeight;
		private MaterialPropertyBlock _mpb;
		private CommandBuffer _cb;
		private Material _material;
		private RenderTexture _softMaskBuffer;
		private int _stencilDepth;
		private Mesh _mesh;
		private SoftMask _parent;
		internal readonly List<SoftMask> _children = new List<SoftMask>();
		private bool _hasChanged = false;
		private bool _hasStencilStateChanged = false;


		[FormerlySerializedAs("m_DesamplingRate")]
		[SerializeField, Tooltip("The down sampling rate for soft mask buffer.")]
		private DownSampling m_DownSamplingRate = DownSampling.x1;

		[SerializeField, Range(0, 1), Tooltip("The value used by the soft mask to select the area of influence defined over the soft mask's graphic.")]
		private float m_Softness = 1;

		[SerializeField, Range(0f, 1f), Tooltip("The transparency of the whole masked graphic.")]
		private float m_Alpha = 1;

		[Header("Advanced Options")]
		[SerializeField, Tooltip("Should the soft mask ignore parent soft masks?")]
		private bool m_IgnoreParent = false;

		[SerializeField, Tooltip("Is the soft mask a part of parent soft mask?")]
		private bool m_PartOfParent = false;

		[SerializeField, Tooltip("Self graphic will not be drawn to soft mask buffer.")]
		private bool m_IgnoreSelfGraphic;


		[SerializeField, Tooltip("Self graphic will not be written to stencil buffer.")]
		private bool m_IgnoreSelfStencil;

		/// <summary>
		/// The down sampling rate for soft mask buffer.
		/// </summary>
		public DownSampling DownSamplingRate
		{
			get { return m_DownSamplingRate; }
			set
			{
				if (m_DownSamplingRate == value)
				{
					return;
				}

				m_DownSamplingRate = value;
				HasChanged = true;
			}
		}

		/// <summary>
		/// The value used by the soft mask to select the area of influence defined over the soft mask's graphic.
		/// </summary>
		public float Softness
		{
			get { return m_Softness; }
			set
			{
				value = Mathf.Clamp01(value);
				if (Mathf.Approximately(m_Softness, value))
				{
					return;
				}

				m_Softness = value;
				HasChanged = true;
			}
		}

		/// <summary>
		/// The transparency of the whole masked graphic.
		/// </summary>
		public float Alpha
		{
			get { return m_Alpha; }
			set
			{
				value = Mathf.Clamp01(value);
				if (Mathf.Approximately(m_Alpha, value))
				{
					return;
				}

				m_Alpha = value;
				HasChanged = true;
			}
		}

		/// <summary>
		/// Should the soft mask ignore parent soft masks?
		/// </summary>
		/// <value>If set to true the soft mask will ignore any parent soft mask settings.</value>
		public bool IgnoreParent
		{
			get { return m_IgnoreParent; }
			set
			{
				if (m_IgnoreParent == value)
				{
					return;
				}

				m_IgnoreParent = value;
				HasChanged = true;
				OnTransformParentChanged();
			}
		}

		/// <summary>
		/// Is the soft mask a part of parent soft mask?
		/// </summary>
		public bool PartOfParent
		{
			get { return m_PartOfParent; }
			set
			{
				if (m_PartOfParent == value)
				{
					return;
				}

				m_PartOfParent = value;
				HasChanged = true;
				OnTransformParentChanged();
			}
		}

		/// <summary>
		/// The soft mask buffer.
		/// </summary>
		public RenderTexture SoftMaskBuffer
		{
			get
			{
				if (_parent)
				{
					ReleaseRt(ref _softMaskBuffer);
					return _parent.SoftMaskBuffer;
				}

				// Check the size of soft mask buffer.
				GetDownSamplingSize(m_DownSamplingRate, out int w, out int h);
				if (_softMaskBuffer && (_softMaskBuffer.width != w || _softMaskBuffer.height != h))
				{
					ReleaseRt(ref _softMaskBuffer);
				}

				if (!_softMaskBuffer)
				{
					_softMaskBuffer = RenderTexture.GetTemporary(w, h, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, 1, RenderTextureMemoryless.Depth);
					HasChanged = true;
					_hasStencilStateChanged = true;
				}

				return _softMaskBuffer;
			}
		}

		public bool HasChanged
		{
			get { return _parent ? _parent.HasChanged : _hasChanged; }
			private set
			{
				if (_parent)
				{
					_parent.HasChanged = value;
				}

				_hasChanged = value;
			}
		}

		public SoftMask Parent => _parent;

		public bool IgnoreSelfGraphic
		{
			get { return m_IgnoreSelfGraphic; }
			set
			{
				if (m_IgnoreSelfGraphic == value)
				{
					return;
				}

				m_IgnoreSelfGraphic = value;
				HasChanged = true;
				graphic.SetVerticesDirtyEx();
			}
		}

		public bool IgnoreSelfStencil
		{
			get { return m_IgnoreSelfStencil; }
			set
			{
				if (m_IgnoreSelfStencil == value)
				{
					return;
				}

				m_IgnoreSelfStencil = value;
				HasChanged = true;
				graphic.SetVerticesDirtyEx();
				graphic.SetMaterialDirtyEx();
			}
		}


		Material Material => _material
					 ? _material
					 : _material =
						  new Material(s_SoftMaskShader
								? s_SoftMaskShader
								: s_SoftMaskShader = Resources.Load<Shader>("SoftMask"))
						  { hideFlags = HideFlags.HideAndDontSave };

		Mesh Mesh => _mesh ? _mesh : _mesh = new Mesh() { hideFlags = HideFlags.HideAndDontSave };


		/// <summary>
		/// Perform material modification in this function.
		/// </summary>
		/// <returns>Modified material.</returns>
		/// <param name="baseMaterial">Configured Material.</param>
		public override Material GetModifiedMaterial(Material baseMaterial)
		{
			HasChanged = true;
			if (IgnoreSelfStencil)
			{
				return baseMaterial;
			}

			Material result = base.GetModifiedMaterial(baseMaterial);
			if (m_IgnoreParent && result != baseMaterial)
			{
				result.SetInt(s_StencilCompId, (int)CompareFunction.Always);
			}

			return result;
		}


		/// <summary>
		/// Call used to modify mesh.
		/// </summary>
		void IMeshModifier.ModifyMesh(Mesh mesh)
		{
			HasChanged = true;
			_mesh = mesh;
		}

		/// <summary>
		/// Call used to modify mesh.
		/// </summary>
		void IMeshModifier.ModifyMesh(VertexHelper verts)
		{
			if (isActiveAndEnabled)
			{
				if (IgnoreSelfGraphic)
				{
					verts.Clear();
					verts.FillMesh(Mesh);
				}
				else if (IgnoreSelfStencil)
				{
					verts.FillMesh(Mesh);
					verts.Clear();
				}
				else
				{
					verts.FillMesh(Mesh);
				}
			}

			HasChanged = true;
		}

		/// <summary>
		/// Given a point and a camera is the raycast valid.
		/// </summary>
		/// <returns>Valid.</returns>
		/// <param name="sp">Screen position.</param>
		/// <param name="eventCamera">Raycast camera.</param>
		/// <param name="g">Target graphic.</param>
		public bool IsRaycastLocationValid(Vector2 sp, Graphic g, int[] interactions)
		{
			if (!isActiveAndEnabled || (g == graphic && !g.raycastTarget))
			{
				return true;
			}

			int x = (int)((SoftMaskBuffer.width - 1) * Mathf.Clamp01(sp.x / Screen.width));
			int y = s_UVStartsAtTop && !s_IsMetal
				 ? (int)((SoftMaskBuffer.height - 1) * (1 - Mathf.Clamp01(sp.y / Screen.height)))
				 : (int)((SoftMaskBuffer.height - 1) * Mathf.Clamp01(sp.y / Screen.height));
			return 0.5f < GetPixelValue(x, y, interactions);
		}

		public override bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return true;
		}

		/// <summary>
		/// This function is called when the object becomes enabled and active.
		/// </summary>
		protected override void OnEnable()
		{
			HasChanged = true;

			// Register.
			if (s_ActiveSoftMasks.Count == 0)
			{
				Canvas.willRenderCanvases += UpdateMaskTextures;

				if (s_StencilCompId == 0)
				{
					s_UVStartsAtTop = SystemInfo.graphicsUVStartsAtTop;
					s_IsMetal = SystemInfo.graphicsDeviceType == GraphicsDeviceType.Metal;
					s_StencilCompId = Shader.PropertyToID("_StencilComp");
					s_ColorMaskId = Shader.PropertyToID("_ColorMask");
					s_MainTexId = Shader.PropertyToID("_MainTex");
					s_SoftnessId = Shader.PropertyToID("_Softness");
					s_Alpha = Shader.PropertyToID("_Alpha");
				}
			}

			s_ActiveSoftMasks.Add(this);

			// Reset the parent-child relation.
			GetComponentsInChildren(false, s_TempRelatables);
			for (int i = s_TempRelatables.Count - 1; 0 <= i; i--)
			{
				s_TempRelatables[i].OnTransformParentChanged();
			}

			s_TempRelatables.Clear();

			// Create objects.
			_mpb = new MaterialPropertyBlock();
			_cb = new CommandBuffer();

			graphic.SetVerticesDirtyEx();

			base.OnEnable();
			_hasStencilStateChanged = false;
		}

		/// <summary>
		/// This function is called when the behaviour becomes disabled.
		/// </summary>
		protected override void OnDisable()
		{
			// Unregister.
			s_ActiveSoftMasks.Remove(this);
			if (s_ActiveSoftMasks.Count == 0)
			{
				Canvas.willRenderCanvases -= UpdateMaskTextures;
			}

			// Reset the parent-child relation.
			for (int i = _children.Count - 1; 0 <= i; i--)
			{
				_children[i].SetParent(_parent);
			}

			_children.Clear();
			SetParent(null);

			// Destroy objects.
			_mpb.Clear();
			_mpb = null;
			_cb.Release();
			_cb = null;

			ReleaseObject(_mesh);
			_mesh = null;
			ReleaseObject(_material);
			_material = null;
			ReleaseRt(ref _softMaskBuffer);

			base.OnDisable();
			_hasStencilStateChanged = false;
		}

		/// <summary>
		/// This function is called when the parent property of the transform of the GameObject has changed.
		/// </summary>
		protected override void OnTransformParentChanged()
		{
			HasChanged = true;
			SoftMask newParent = null;
			if (isActiveAndEnabled && !m_IgnoreParent)
			{
				Transform parentTransform = transform.parent;
				while (parentTransform && (!newParent || !newParent.enabled))
				{
					newParent = parentTransform.GetComponent<SoftMask>();
					parentTransform = parentTransform.parent;
				}
			}

			SetParent(newParent);
			HasChanged = true;
		}

		protected override void OnRectTransformDimensionsChange()
		{
			HasChanged = true;
		}

#if UNITY_EDITOR
		/// <summary>
		/// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
		/// </summary>
		protected override void OnValidate()
		{
			graphic.SetVerticesDirtyEx();
			graphic.SetMaterialDirtyEx();
			OnTransformParentChanged();
			base.OnValidate();
			_hasStencilStateChanged = false;
		}
#endif

		/// <summary>
		/// Update all soft mask textures.
		/// </summary>
		private static void UpdateMaskTextures()
		{
			Profiler.BeginSample("UpdateMaskTextures");
			foreach (SoftMask sm in s_ActiveSoftMasks)
			{
				if (!sm || sm._hasChanged)
				{
					continue;
				}

				Canvas canvas = sm.graphic.canvas;
				if (!canvas)
				{
					continue;
				}

				if (canvas.renderMode == RenderMode.WorldSpace)
				{
					Camera cam = canvas.worldCamera;
					if (!cam)
					{
						continue;
					}

					Profiler.BeginSample("Check view projection matrix changed (world space)");
					Matrix4x4 nowVP = cam.projectionMatrix * cam.worldToCameraMatrix;
					int id = cam.GetInstanceID();
					s_PreviousViewProjectionMatrices.TryGetValue(id, out Matrix4x4 previousVP);
					s_NowViewProjectionMatrices[id] = nowVP;

					if (previousVP != nowVP)
					{
						sm.HasChanged = true;
					}

					Profiler.EndSample();
				}

				RectTransform rt = sm.rectTransform;
				if (rt.hasChanged)
				{
					rt.hasChanged = false;
					sm.HasChanged = true;
				}
#if UNITY_EDITOR
				if (!Application.isPlaying)
				{
					sm.HasChanged = true;
				}
#endif
			}

			Profiler.BeginSample("Update changed soft masks");
			foreach (SoftMask sm in s_ActiveSoftMasks)
			{
				if (!sm || !sm._hasChanged)
				{
					continue;
				}

				sm._hasChanged = false;
				if (sm._parent)
				{
					continue;
				}

				sm.UpdateMaskTexture();

				if (!sm._hasStencilStateChanged)
				{
					continue;
				}

				sm._hasStencilStateChanged = false;

				Profiler.BeginSample("Notify stencil state changed");
				MaskUtilities.NotifyStencilStateChanged(sm);
				Profiler.EndSample();
			}

			Profiler.EndSample();

			Profiler.BeginSample("Update previous view projection matrices");
			s_PreviousViewProjectionMatrices.Clear();
			foreach (KeyValuePair<int, Matrix4x4> kv in s_NowViewProjectionMatrices)
			{
				s_PreviousViewProjectionMatrices.Add(kv.Key, kv.Value);
			}

			s_NowViewProjectionMatrices.Clear();
			Profiler.EndSample();

			Profiler.EndSample();

#if UNITY_EDITOR
			int w = s_PreviousWidth;
			int h = s_PreviousHeight;
			GetDownSamplingSize(DownSampling.None, out s_PreviousWidth, out s_PreviousHeight);
			if (w != s_PreviousWidth || h != s_PreviousHeight)
			{
				Canvas.ForceUpdateCanvases();
			}
#endif
		}

		/// <summary>
		/// Update the mask texture.
		/// </summary>
		private void UpdateMaskTexture()
		{
			if (!graphic || !graphic.canvas)
			{
				return;
			}

			Profiler.BeginSample("UpdateMaskTexture");


			_stencilDepth = MaskUtilities.GetStencilDepth(transform, MaskUtilities.FindRootSortOverrideCanvas(transform));

			// Collect children soft masks.
			Profiler.BeginSample("Collect children soft masks");
			int depth = 0;
			s_TmpSoftMasks[0].Add(this);
			while (_stencilDepth + depth < 3)
			{
				int count = s_TmpSoftMasks[depth].Count;
				for (int i = 0; i < count; i++)
				{
					List<SoftMask> children = s_TmpSoftMasks[depth][i]._children;
					int childCount = children.Count;
					for (int j = 0; j < childCount; j++)
					{
						SoftMask child = children[j];
						int childDepth = child.m_PartOfParent ? depth : depth + 1;
						s_TmpSoftMasks[childDepth].Add(child);
					}
				}

				depth++;
			}

			Profiler.EndSample();

			// CommandBuffer.
			Profiler.BeginSample("Initialize CommandBuffer");
			_cb.Clear();
			_cb.SetRenderTarget(SoftMaskBuffer);
			_cb.ClearRenderTarget(false, true, s_ClearColors[_stencilDepth]);
			Profiler.EndSample();

			// Set view and projection matrices.
			Profiler.BeginSample("Set view and projection matrices");
			Canvas c = graphic.canvas.rootCanvas;
			Camera cam = c.worldCamera != null ? c.worldCamera : Camera.main;
			if (c && c.renderMode != RenderMode.ScreenSpaceOverlay && cam)
			{
				Matrix4x4 p = GL.GetGPUProjectionMatrix(cam.projectionMatrix, false);
				_cb.SetViewProjectionMatrices(cam.worldToCameraMatrix, p);
			}
			else
			{
				Vector3 pos = c.transform.position;
				Matrix4x4 vm = Matrix4x4.TRS(new Vector3(-pos.x, -pos.y, -1000), Quaternion.identity, new Vector3(1, 1, -1f));
				Matrix4x4 pm = Matrix4x4.TRS(new Vector3(0, 0, -1), Quaternion.identity, new Vector3(1 / pos.x, 1 / pos.y, -2 / 10000f));
				_cb.SetViewProjectionMatrices(vm, pm);
			}

			Profiler.EndSample();

			// Draw soft masks.
			Profiler.BeginSample("Draw Mesh");
			for (int i = 0; i < s_TmpSoftMasks.Length; i++)
			{
				int count = s_TmpSoftMasks[i].Count;
				for (int j = 0; j < count; j++)
				{
					SoftMask sm = s_TmpSoftMasks[i][j];

					if (i != 0)
					{
						sm._stencilDepth = MaskUtilities.GetStencilDepth(sm.transform, MaskUtilities.FindRootSortOverrideCanvas(sm.transform));
					}

					// Set material property.
					sm.Material.SetInt(s_ColorMaskId, (int)1 << (3 - _stencilDepth - i));
					sm._mpb.SetTexture(s_MainTexId, sm.graphic.mainTexture);
					sm._mpb.SetFloat(s_SoftnessId, sm.m_Softness);
					sm._mpb.SetFloat(s_Alpha, sm.m_Alpha);

					// Draw mesh.
					_cb.DrawMesh(sm.Mesh, sm.transform.localToWorldMatrix, sm.Material, 0, 0, sm._mpb);
				}

				s_TmpSoftMasks[i].Clear();
			}

			Profiler.EndSample();

			Graphics.ExecuteCommandBuffer(_cb);
			Profiler.EndSample();
		}

		/// <summary>
		/// Gets the size of the down sampling.
		/// </summary>
		private static void GetDownSamplingSize(DownSampling rate, out int w, out int h)
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				string[] res = UnityEditor.UnityStats.screenRes.Split('x');
				w = Mathf.Max(64, int.Parse(res[0]));
				h = Mathf.Max(64, int.Parse(res[1]));
			}
			else
#endif
			if (Screen.fullScreenMode == FullScreenMode.Windowed)
			{
				w = Screen.width;
				h = Screen.height;
			}
			else
			{
				w = Screen.currentResolution.width;
				h = Screen.currentResolution.height;
			}

			if (rate == DownSampling.None)
			{
				return;
			}

			float aspect = (float)w / h;
			if (w < h)
			{
				h = Mathf.ClosestPowerOfTwo(h / (int)rate);
				w = Mathf.CeilToInt(h * aspect);
			}
			else
			{
				w = Mathf.ClosestPowerOfTwo(w / (int)rate);
				h = Mathf.CeilToInt(w / aspect);
			}
		}

		/// <summary>
		/// Release the specified obj.
		/// </summary>
		/// <param name="tmpRT">Object.</param>
		private static void ReleaseRt(ref RenderTexture tmpRT)
		{
			if (!tmpRT)
			{
				return;
			}

			tmpRT.Release();
			RenderTexture.ReleaseTemporary(tmpRT);
			tmpRT = null;
		}

		/// <summary>
		/// Release the specified obj.
		/// </summary>
		/// <param name="obj">Object.</param>
		private static void ReleaseObject(Object obj)
		{
			if (!obj)
			{
				return;
			}

#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				DestroyImmediate(obj);
			}
			else
#endif
			{
				Destroy(obj);
			}
		}


		/// <summary>
		/// Set the parent of the soft mask.
		/// </summary>
		/// <param name="newParent">The parent soft mask to use.</param>
		private void SetParent(SoftMask newParent)
		{
			if (_parent != newParent && this != newParent)
			{
				if (_parent && _parent._children.Contains(this))
				{
					_parent._children.Remove(this);
					_parent._children.RemoveAll(x => x == null);
				}

				_parent = newParent;
			}

			if (_parent && !_parent._children.Contains(this))
			{
				_parent._children.Add(this);
			}
		}

		/// <summary>
		/// Gets the pixel value.
		/// </summary>
		private float GetPixelValue(int x, int y, int[] interactions)
		{
			if (!s_ReadTexture)
			{
				s_ReadTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
			}

			RenderTexture currentRt = RenderTexture.active;

			RenderTexture.active = SoftMaskBuffer;
			s_ReadTexture.ReadPixels(new Rect(x, y, 1, 1), 0, 0);
			s_ReadTexture.Apply(false, false);
			RenderTexture.active = currentRt;

			byte[] colors = s_ReadTexture.GetRawTextureData();

			for (int i = 0; i < 4; i++)
			{
				switch (interactions[(i + 3) % 4])
				{
					case 0:
						colors[i] = 255;
						break;
					case 2:
						colors[i] = (byte)(255 - colors[i]);
						break;
				}
			}

			return _stencilDepth switch
			{
				0 => colors[1] / 255f,
				1 => colors[1] / 255f * (colors[2] / 255f),
				2 => colors[1] / 255f * (colors[2] / 255f) * (colors[3] / 255f),
				3 => colors[1] / 255f * (colors[2] / 255f) * (colors[3] / 255f) * (colors[0] / 255f),
				_ => 0,
			};
		}
	}
}
