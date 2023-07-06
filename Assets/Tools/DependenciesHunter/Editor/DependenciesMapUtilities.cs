// #define HUNT_ADDRESSABLES

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
#if UNITY_2021_2_OR_NEWER
using UnityEditor.Build;
#endif
#if HUNT_ADDRESSABLES
using UnityEditor.AddressableAssets;
#endif
using UnityEngine;
using UnityEngine.U2D;

// ReSharper disable once CheckNamespace
namespace DependenciesHunter
{
	public static class DependenciesMapUtilities
	{
		public static void FillReverseDependenciesMap(out Dictionary<string, List<string>> reverseDependencies)
		{
			var assetPaths = AssetDatabase.GetAllAssetPaths().ToList();

			reverseDependencies = assetPaths.ToDictionary(assetPath => assetPath, assetPath => new List<string>());

			Debug.Log($"Total Assets Count: {assetPaths.Count}");

			for (var i = 0; i < assetPaths.Count; i++)
			{
				EditorUtility.DisplayProgressBar("Dependencies Hunter", "Creating a map of dependencies",
						(float)i / assetPaths.Count);

				var assetDependencies = AssetDatabase.GetDependencies(assetPaths[i], false);

				foreach (var assetDependency in assetDependencies)
				{
					if (reverseDependencies.ContainsKey(assetDependency) && assetDependency != assetPaths[i])
					{
						reverseDependencies[assetDependency].Add(assetPaths[i]);
					}
				}
			}
		}
	}
}