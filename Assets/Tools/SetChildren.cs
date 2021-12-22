using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

public class SetChildren : MonoBehaviour
{
#if UNITY_EDITOR
	public GameObject prefab;

	[Button]
	public void Set()
	{
		if (!prefab) return;
		int i = 0;
		foreach (Transform child in transform)
		{
			Object go = PrefabUtility.InstantiatePrefab(prefab, child);
			go.name = $"{prefab.name} ({i})";
			i++;
		}
	}

	[Button]
	public void Clear()
	{
		foreach (Transform child in transform)
		{
			while (child.childCount > 0)
			{
				Transform grandChild = child.GetChild(0);
				grandChild.SetParent(null);
				DestroyImmediate(grandChild.gameObject);
			}
		}
	}
#endif
}