using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

[ExecuteAlways]
public class ColorBlockSync : MonoBehaviour
{
	public Selectable selectable;
	[Expandable] public ColorBlockData data;

#if UNITY_EDITOR
	private void Reset()
	{
		selectable = GetComponent<Selectable>();
		data = AssetTools.FindAssetByType<ColorBlockData>();
	}

	private void Update()
	{
		if (data)
			selectable.colors = data.colorBlock;
	}
#endif
}