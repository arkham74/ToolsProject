#if ENABLE_INPUT_SYSTEM
using UnityEngine;

public class BindingAttribute : PropertyAttribute
{
	public readonly string inputReference;

	public BindingAttribute(string inputReference)
	{
		this.inputReference = inputReference;
	}
}
#endif