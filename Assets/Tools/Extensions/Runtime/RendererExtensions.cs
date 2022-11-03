using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using TMPro;
using JD;
using Freya;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tools = JD.Tools;
using UnityEngine.Rendering;

namespace JD
{
	public static class RendererExtensions
	{
		public static void SetBlockFloat(this Renderer renderer, string name, float value) => renderer.SetBlockFloat(Shader.PropertyToID(name), value);
		public static void SetBlockColor(this Renderer renderer, string name, Color value) => renderer.SetBlockColor(Shader.PropertyToID(name), value);

		public static void SetBlockBuffer(this Renderer renderer, string name, ComputeBuffer value) => renderer.SetBlockBuffer(Shader.PropertyToID(name), value);
		public static void SetBlockBuffer(this Renderer renderer, string name, GraphicsBuffer value) => renderer.SetBlockBuffer(Shader.PropertyToID(name), value);
		public static void SetBlockMatrix(this Renderer renderer, string name, Matrix4x4 value) => renderer.SetBlockMatrix(Shader.PropertyToID(name), value);
		public static void SetBlockVector(this Renderer renderer, string name, Vector4 value) => renderer.SetBlockVector(Shader.PropertyToID(name), value);

		public static void SetBlockInteger(this Renderer renderer, string name, int value) => renderer.SetBlockInteger(Shader.PropertyToID(name), value);
		public static void SetBlockTexture(this Renderer renderer, string name, RenderTexture value, RenderTextureSubElement element) => renderer.SetBlockTexture(Shader.PropertyToID(name), value, element);
		public static void SetBlockTexture(this Renderer renderer, string name, Texture value) => renderer.SetBlockTexture(Shader.PropertyToID(name), value);

		public static void SetBlockFloatArray(this Renderer renderer, string name, float[] values) => renderer.SetBlockFloatArray(Shader.PropertyToID(name), values);
		public static void SetBlockFloatArray(this Renderer renderer, string name, List<float> values) => renderer.SetBlockFloatArray(Shader.PropertyToID(name), values);

		public static void SetBlockMatrixArray(this Renderer renderer, string name, List<Matrix4x4> values) => renderer.SetBlockMatrixArray(Shader.PropertyToID(name), values);
		public static void SetBlockMatrixArray(this Renderer renderer, string name, Matrix4x4[] values) => renderer.SetBlockMatrixArray(Shader.PropertyToID(name), values);
		public static void SetBlockVectorArray(this Renderer renderer, string name, List<Vector4> values) => renderer.SetBlockVectorArray(Shader.PropertyToID(name), values);
		public static void SetBlockVectorArray(this Renderer renderer, string name, Vector4[] values) => renderer.SetBlockVectorArray(Shader.PropertyToID(name), values);

		public static void SetBlockConstantBuffer(this Renderer renderer, string name, ComputeBuffer value, int offset, int size) => renderer.SetBlockConstantBuffer(Shader.PropertyToID(name), value, offset, size);
		public static void SetBlockConstantBuffer(this Renderer renderer, string name, GraphicsBuffer value, int offset, int size) => renderer.SetBlockConstantBuffer(Shader.PropertyToID(name), value, offset, size);

		public static void SetBlockFloat(this Renderer renderer, int nameID, float value)
		{
			MaterialPropertyBlock mpb = Pools.GetMaterialPropertyBlock();
			renderer.GetPropertyBlock(mpb);
			mpb.SetFloat(nameID, value);
			renderer.SetPropertyBlock(mpb);
			Pools.Release(mpb);
		}

		public static void SetBlockInteger(this Renderer renderer, int nameID, int value)
		{
			MaterialPropertyBlock mpb = Pools.GetMaterialPropertyBlock();
			renderer.GetPropertyBlock(mpb);
			mpb.SetInteger(nameID, value);
			renderer.SetPropertyBlock(mpb);
			Pools.Release(mpb);
		}

		public static void SetBlockVector(this Renderer renderer, int nameID, Vector4 value)
		{
			MaterialPropertyBlock mpb = Pools.GetMaterialPropertyBlock();
			renderer.GetPropertyBlock(mpb);
			mpb.SetVector(nameID, value);
			renderer.SetPropertyBlock(mpb);
			Pools.Release(mpb);
		}

		public static void SetBlockColor(this Renderer renderer, int nameID, Color value)
		{
			MaterialPropertyBlock mpb = Pools.GetMaterialPropertyBlock();
			renderer.GetPropertyBlock(mpb);
			mpb.SetColor(nameID, value);
			renderer.SetPropertyBlock(mpb);
			Pools.Release(mpb);
		}

		public static void SetBlockMatrix(this Renderer renderer, int nameID, Matrix4x4 value)
		{
			MaterialPropertyBlock mpb = Pools.GetMaterialPropertyBlock();
			renderer.GetPropertyBlock(mpb);
			mpb.SetMatrix(nameID, value);
			renderer.SetPropertyBlock(mpb);
			Pools.Release(mpb);
		}

		public static void SetBlockBuffer(this Renderer renderer, int nameID, ComputeBuffer value)
		{
			MaterialPropertyBlock mpb = Pools.GetMaterialPropertyBlock();
			renderer.GetPropertyBlock(mpb);
			mpb.SetBuffer(nameID, value);
			renderer.SetPropertyBlock(mpb);
			Pools.Release(mpb);
		}

		public static void SetBlockBuffer(this Renderer renderer, int nameID, GraphicsBuffer value)
		{
			MaterialPropertyBlock mpb = Pools.GetMaterialPropertyBlock();
			renderer.GetPropertyBlock(mpb);
			mpb.SetBuffer(nameID, value);
			renderer.SetPropertyBlock(mpb);
			Pools.Release(mpb);
		}

		public static void SetBlockTexture(this Renderer renderer, int nameID, Texture value)
		{
			MaterialPropertyBlock mpb = Pools.GetMaterialPropertyBlock();
			renderer.GetPropertyBlock(mpb);
			mpb.SetTexture(nameID, value);
			renderer.SetPropertyBlock(mpb);
			Pools.Release(mpb);
		}

		public static void SetBlockTexture(this Renderer renderer, int nameID, RenderTexture value, RenderTextureSubElement element)
		{
			MaterialPropertyBlock mpb = Pools.GetMaterialPropertyBlock();
			renderer.GetPropertyBlock(mpb);
			mpb.SetTexture(nameID, value, element);
			renderer.SetPropertyBlock(mpb);
			Pools.Release(mpb);
		}

		public static void SetBlockConstantBuffer(this Renderer renderer, int nameID, ComputeBuffer value, int offset, int size)
		{
			MaterialPropertyBlock mpb = Pools.GetMaterialPropertyBlock();
			renderer.GetPropertyBlock(mpb);
			mpb.SetConstantBuffer(nameID, value, offset, size);
			renderer.SetPropertyBlock(mpb);
			Pools.Release(mpb);
		}

		public static void SetBlockConstantBuffer(this Renderer renderer, int nameID, GraphicsBuffer value, int offset, int size)
		{
			MaterialPropertyBlock mpb = Pools.GetMaterialPropertyBlock();
			renderer.GetPropertyBlock(mpb);
			mpb.SetConstantBuffer(nameID, value, offset, size);
			renderer.SetPropertyBlock(mpb);
			Pools.Release(mpb);
		}

		public static void SetBlockFloatArray(this Renderer renderer, int nameID, List<float> values)
		{
			MaterialPropertyBlock mpb = Pools.GetMaterialPropertyBlock();
			renderer.GetPropertyBlock(mpb);
			mpb.SetFloatArray(nameID, values);
			renderer.SetPropertyBlock(mpb);
			Pools.Release(mpb);
		}

		public static void SetBlockFloatArray(this Renderer renderer, int nameID, float[] values)
		{
			MaterialPropertyBlock mpb = Pools.GetMaterialPropertyBlock();
			renderer.GetPropertyBlock(mpb);
			mpb.SetFloatArray(nameID, values);
			renderer.SetPropertyBlock(mpb);
			Pools.Release(mpb);
		}

		public static void SetBlockVectorArray(this Renderer renderer, int nameID, List<Vector4> values)
		{
			MaterialPropertyBlock mpb = Pools.GetMaterialPropertyBlock();
			renderer.GetPropertyBlock(mpb);
			mpb.SetVectorArray(nameID, values);
			renderer.SetPropertyBlock(mpb);
			Pools.Release(mpb);
		}

		public static void SetBlockVectorArray(this Renderer renderer, int nameID, Vector4[] values)
		{
			MaterialPropertyBlock mpb = Pools.GetMaterialPropertyBlock();
			renderer.GetPropertyBlock(mpb);
			mpb.SetVectorArray(nameID, values);
			renderer.SetPropertyBlock(mpb);
			Pools.Release(mpb);
		}

		public static void SetBlockMatrixArray(this Renderer renderer, int nameID, List<Matrix4x4> values)
		{
			MaterialPropertyBlock mpb = Pools.GetMaterialPropertyBlock();
			renderer.GetPropertyBlock(mpb);
			mpb.SetMatrixArray(nameID, values);
			renderer.SetPropertyBlock(mpb);
			Pools.Release(mpb);
		}

		public static void SetBlockMatrixArray(this Renderer renderer, int nameID, Matrix4x4[] values)
		{
			MaterialPropertyBlock mpb = Pools.GetMaterialPropertyBlock();
			renderer.GetPropertyBlock(mpb);
			mpb.SetMatrixArray(nameID, values);
			renderer.SetPropertyBlock(mpb);
			Pools.Release(mpb);
		}
	}
}