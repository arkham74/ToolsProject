using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

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

	public void Sync()
	{
		selectable.colors = data.colorBlock;
		selectable.MarkDirty();
	}
#endif
}