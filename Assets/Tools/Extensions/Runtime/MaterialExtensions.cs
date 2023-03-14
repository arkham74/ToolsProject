using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace JD
{
	public static class MaterialExtensions
	{
		public static void SetBool(this Material material, string name, bool value)
		{
			material.SetInt(name, Convert.ToInt32(value));
		}

		public static void SetBaseMap(this Material material, Texture texture)
		{
			material.SetTexture(Shader.PropertyToID("_BaseMap"), texture);
		}

		public static void SetKeyword(this Material mat, string keyword, bool value)
		{
			LocalKeyword localKeyword = new LocalKeyword(mat.shader, keyword);
			mat.SetKeyword(localKeyword, value);
		}
	}
}