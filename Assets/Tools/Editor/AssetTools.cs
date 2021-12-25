using UnityEditor;
using UnityEngine;

public class AssetTools
{
	public static T[] FindAssetsByType<T>() where T : Object
	{
		string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
		T[] assets = new T[guids.Length];
		for (int i = 0; i < guids.Length; i++)
		{
			string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
			assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
		}

		return assets;
	}
}