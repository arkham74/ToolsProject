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
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace DependenciesHunter
{
	public class SelectedAssetsAnalysisUtilities
	{
		private Dictionary<string, List<string>> _cachedAssetsMap;

		public Dictionary<Object, List<string>> GetReferences(Object[] selectedObjects)
		{
			if (selectedObjects == null)
			{
				Debug.Log("No selected objects passed");
				return new Dictionary<Object, List<string>>();
			}

			if (_cachedAssetsMap == null)
			{
				DependenciesMapUtilities.FillReverseDependenciesMap(out _cachedAssetsMap);
			}

			EditorUtility.ClearProgressBar();

			GetDependencies(selectedObjects, _cachedAssetsMap, out var result);

			return result;
		}

		private static void GetDependencies(IEnumerable<Object> selectedObjects, IReadOnlyDictionary<string,
				List<string>> source, out Dictionary<Object, List<string>> results)
		{
			results = new Dictionary<Object, List<string>>();

			foreach (var selectedObject in selectedObjects)
			{
				var selectedObjectPath = AssetDatabase.GetAssetPath(selectedObject);

				if (source.ContainsKey(selectedObjectPath))
				{
					results.Add(selectedObject, source[selectedObjectPath]);
				}
				else
				{
					Debug.LogWarning("Dependencies Hunter doesn't contain the specified object in the assets map",
							selectedObject);
					results.Add(selectedObject, new List<string>());
				}
			}
		}
	}
}