using System;
using UnityEngine;

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
	}
}