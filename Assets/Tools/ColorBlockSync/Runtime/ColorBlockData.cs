using UnityEngine;
using UnityEngine.UI;

namespace JD
{
	[CreateAssetMenu(menuName = "ScriptableObject/ColorBlockData")]
	public class ColorBlockData : ScriptableObject
	{
		public ColorBlock colorBlock = ColorBlock.defaultColorBlock;
	}
}