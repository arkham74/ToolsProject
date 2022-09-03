using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace JD
{
	public static class AssetTools
	{
		public static T[] FindAssetsByName<T>(string name) where T : Object
		{
			string[] guids = AssetDatabase.FindAssets(name);
			List<T> assets = new List<T>();
			for (int i = 0; i < guids.Length; i++)
			{
				string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
				T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
				if (asset)
				{
					assets.Add(asset);
				}
			}

			return assets.ToArray();
		}

		public static T FindAssetByName<T>(string name) where T : Object
		{
			return FindAssetsByName<T>(name)[0];
		}

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

		public static T FindAssetByType<T>() where T : Object
		{
			T[] ts = FindAssetsByType<T>();
			return ts.Length > 0 ? ts[0] : null;
		}
	}
}
#endif
