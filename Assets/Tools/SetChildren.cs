using UnityEditor;
using UnityEngine;
#if TOOLS_NAUATTR
using NaughtyAttributes;
#endif

public class SetChildren : MonoBehaviour
{
#if UNITY_EDITOR
	public GameObject prefab;

#if TOOLS_NAUATTR
	[Button]
#endif
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

#if TOOLS_NAUATTR
	[Button]
#endif
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