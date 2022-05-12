using UnityEngine;
using UnityEngine.InputSystem;

public class BindingAttribute : PropertyAttribute
{
	public readonly string inputReference;

	public BindingAttribute(string inputReference)
	{
		this.inputReference = inputReference;
	}
}