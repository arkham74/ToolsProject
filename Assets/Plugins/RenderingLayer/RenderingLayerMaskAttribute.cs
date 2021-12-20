using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class RenderingLayerMaskAttribute : PropertyAttribute
{
	public RenderingLayerMaskAttribute() { }
}