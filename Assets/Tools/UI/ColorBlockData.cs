using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObject/ColorBlockData")]
public class ColorBlockData : ScriptableObject
{
	public ColorBlock colorBlock = ColorBlock.defaultColorBlock;
}