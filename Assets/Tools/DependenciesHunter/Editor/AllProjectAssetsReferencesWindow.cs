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
	/// <summary>
	/// Lists all unreferenced assets in a project.
	/// </summary>
	public class AllProjectAssetsReferencesWindow : EditorWindow
	{
		private class Result
		{
			public List<AssetData> Assets { get; } = new List<AssetData>();
			public Dictionary<string, int> RefsByTypes { get; } = new Dictionary<string, int>();
			public string OutputDescription { get; set; }
		}

		private class AnalysisSettings
		{
			// ReSharper disable once StringLiteralTypo
			public readonly List<string> DefaultIgnorePatterns = new List<string>
						{
								@"/Resources/",
								@"/Editor/",
								@"/Editor Default Resources/",
								@"/ThirdParty/",
								@"ProjectSettings/",
								@"Packages/",
								@"\.asmdef$",
								@"link\.xml$",
								@"\.csv$",
								@"\.md$",
								@"\.json$",
								@"\.xml$",
								@"\.txt$"
						};

			// ReSharper disable once InconsistentNaming
			public const string PATTERNS_PREFS_KEY = "DependencyHunterIgnorePatterns";

			public List<string> IgnoredPatterns { get; set; }

			public bool FindUnreferencedOnly { get; set; } = true;
		}

		private class OutputSettings
		{
			public const int PageSize = 50;

			public int? PageToShow { get; set; }

			public string PathFilter { get; set; }
			public string TypeFilter { get; set; }
			// ReSharper disable once IdentifierTypo
			// ReSharper disable once UnusedAutoPropertyAccessor.Local
			public bool ShowAddressables { get; set; }
			public bool ShowUnreferencedOnly { get; set; }
			public bool ShowAssetsWithWarningsOnly { get; set; }

			/// <summary>
			/// Sorting types.
			/// By type: 0: A-Z, 1: Z-A
			/// By path: 2: A-Z, 3: Z-A
			/// By size: 4: A-Z, 5: Z-A
			///  
			/// </summary>
			public int SortType { get; set; }
		}

		private ProjectAssetsAnalysisUtilities _service;

		private Result _result;
		private OutputSettings _outputSettings;
		private AnalysisSettings _analysisSettings;

		private Vector2 _pagesScroll = Vector2.zero;
		private Vector2 _typesScroll = Vector2.zero;
		private Vector2 _assetsScroll = Vector2.zero;
		private bool _analysisSettingsFoldout;

		[MenuItem("Tools/Dependencies Hunter")]
		public static void LaunchUnreferencedAssetsWindow()
		{
			GetWindow<AllProjectAssetsReferencesWindow>();
		}

		private void PopulateUnusedAssetsList()
		{
			_result = new Result();
			_outputSettings = new OutputSettings();

			// ReSharper disable once ConvertIfStatementToNullCoalescingAssignment
			if (_service == null)
			{
				_service = new ProjectAssetsAnalysisUtilities();
			}

			Clear();

			if (!_analysisSettings.FindUnreferencedOnly)
			{
				_outputSettings.ShowUnreferencedOnly = false;
			}

			_outputSettings.PageToShow = 0;

			Show();

			DependenciesMapUtilities.FillReverseDependenciesMap(out var map);

			EditorUtility.ClearProgressBar();

			var filteredOutput = new StringBuilder();
			filteredOutput.AppendLine("Assets ignored by pattern:");

			var count = 0;
			foreach (var mapElement in map)
			{
				float prog = (float)count / map.Count;
				EditorUtility.DisplayProgressBar("Unreferenced Assets", "Searching for unreferenced assets", prog);
				count++;

				var warning = string.Empty;
				var referencesCount = mapElement.Value.Count;

				if (referencesCount == 1)
				{
					var type = AssetDatabase.GetMainAssetTypeAtPath(mapElement.Key);
					if (type == typeof(Texture2D))
					{
						var reference = mapElement.Value[0];
						var referenceType = AssetDatabase.GetMainAssetTypeAtPath(reference);
						if (referenceType == typeof(SpriteAtlas))
						{
							warning = $"Sprite references only its atlas {reference}";
							referencesCount = 0;
						}
					}
				}

				if (_analysisSettings.FindUnreferencedOnly && referencesCount != 0)
					continue;

				var validForOutput = ProjectAssetsAnalysisUtilities.IsValidForOutput(mapElement.Key,
						_analysisSettings.IgnoredPatterns);
				var validAssetType = _service.IsValidAssetType(mapElement.Key, validForOutput);

				if (!validAssetType)
					continue;

				if (validForOutput)
				{
					_result.Assets.Add(AssetData.Create(mapElement.Key, referencesCount, warning));
				}
				else
				{
					filteredOutput.AppendLine(mapElement.Key);
				}
			}

			var types = _result.Assets.Select(x => x.TypeName);

			foreach (var type in types)
			{
				_result.RefsByTypes[type] = _result.Assets.Count(x => x.TypeName == type);
			}

#if HUNT_ADDRESSABLES
            if (_analysisSettings.FindUnreferencedOnly)
            {
                var addressablesCount = _result.Assets.Count(x => x.IsAddressable);

                var nonAddressablesCount = _result.Assets.Count - addressablesCount;
                _result.OutputDescription = $"Result. Unreferenced Assets: Total = {_result.Assets.Count} " +
                                            $"Addressables = {addressablesCount} Common = {nonAddressablesCount}";
            }
            else
            {
                var unreferencedTotalCount = _result.Assets.Count(x => x.ReferencesCount == 0);

                var unreferencedAddressablesCount = _result.Assets.Count(x => 
                    x.IsAddressable && x.ReferencesCount == 0);

                var unreferencedCommonCount = unreferencedTotalCount - unreferencedAddressablesCount;

                _result.OutputDescription = $"Result. Assets: Total = {_result.Assets.Count} " +
                                            $"Unreferenced = {unreferencedTotalCount} " +
                                            $"Unreferenced Addressables = {unreferencedAddressablesCount} " +
                                            $"Unreferenced Common = {unreferencedCommonCount}";
            }
#else
			if (_analysisSettings.FindUnreferencedOnly)
			{
				_result.OutputDescription = $"Result. Unreferenced Assets: {_result.Assets.Count}";
			}
			else
			{
				var unreferencedTotalCount = _result.Assets.Count(x => x.ReferencesCount == 0);
				_result.OutputDescription = $"Result. Assets: Total = {_result.Assets.Count} Unreferenced = {unreferencedTotalCount}";
			}
#endif

			SortByPath();

			EditorUtility.ClearProgressBar();

			Debug.Log(filteredOutput.ToString());
			Debug.Log(_result.OutputDescription);
			filteredOutput.Clear();
		}

		private static void Clear()
		{
			EditorUtility.UnloadUnusedAssetsImmediate();
		}

		private void OnGUI()
		{
			GUIUtilities.HorizontalLine();

			EditorGUILayout.BeginHorizontal();

			GUILayout.FlexibleSpace();

			var prevColor = GUI.color;
			GUI.color = Color.green;

			if (GUILayout.Button("Run Analysis", GUILayout.Width(300f)))
			{
				PopulateUnusedAssetsList();
			}

			GUI.color = prevColor;

			GUILayout.FlexibleSpace();

			EditorGUILayout.EndHorizontal();

			GUIUtilities.HorizontalLine();

			OnAnalysisSettingsGUI();

			GUIUtilities.HorizontalLine();

			if (_result == null)
			{
				return;
			}

			if (_result.Assets.Count == 0)
			{
				EditorGUILayout.LabelField("No unreferenced found");
				return;
			}

			var filteredAssets = _result.Assets;

			if (!string.IsNullOrEmpty(_outputSettings.PathFilter))
			{
				filteredAssets = filteredAssets.Where(x => x.Path.Contains(_outputSettings.PathFilter)).ToList();
			}

			if (!_outputSettings.ShowAddressables)
			{
				filteredAssets = filteredAssets.Where(x => !x.IsAddressable).ToList();
			}

			if (!string.IsNullOrEmpty(_outputSettings.TypeFilter))
			{
				filteredAssets = filteredAssets.Where(x => x.TypeName == _outputSettings.TypeFilter).ToList();
			}

			if (_outputSettings.ShowAssetsWithWarningsOnly)
			{
				filteredAssets = filteredAssets.Where(x => !string.IsNullOrEmpty(x.Warning)).ToList();
			}

			if (!_analysisSettings.FindUnreferencedOnly && _outputSettings.ShowUnreferencedOnly)
			{
				filteredAssets = filteredAssets.Where(x => x.ReferencesCount == 0).ToList();
			}

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(_result.OutputDescription);

			if (filteredAssets.Count < 1000)
			{
				if (GUILayout.Button("Save to Clipboard", GUILayout.Width(250f)))
				{
					var toClipboard = new StringBuilder();

					toClipboard.AppendLine($"Unreferenced Assets [{filteredAssets.Count}]:");

					foreach (var asset in filteredAssets)
					{
						toClipboard.AppendLine("[" + asset.TypeName + "][" + asset.ReadableSize + "] " + asset.Path);
					}

					EditorGUIUtility.systemCopyBuffer = toClipboard.ToString();
				}
			}

			EditorGUILayout.EndHorizontal();

			_pagesScroll = EditorGUILayout.BeginScrollView(_pagesScroll);

			EditorGUILayout.BeginHorizontal();

			prevColor = GUI.color;
			GUI.color = !_outputSettings.PageToShow.HasValue ? Color.yellow : Color.white;

			if (GUILayout.Button("All", GUILayout.Width(30f)))
			{
				_outputSettings.PageToShow = null;
			}

			GUI.color = prevColor;

			var totalCount = filteredAssets.Count;
			var pagesCount = totalCount / OutputSettings.PageSize + (totalCount % OutputSettings.PageSize > 0 ? 1 : 0);

			for (var i = 0; i < pagesCount; i++)
			{
				prevColor = GUI.color;
				GUI.color = _outputSettings.PageToShow == i ? Color.yellow : Color.white;

				if (GUILayout.Button((i + 1).ToString(), GUILayout.Width(30f)))
				{
					_outputSettings.PageToShow = i;
				}

				GUI.color = prevColor;
			}

			if (_outputSettings.PageToShow.HasValue && _outputSettings.PageToShow > pagesCount - 1)
			{
				_outputSettings.PageToShow = pagesCount - 1;
			}

			if (_outputSettings.PageToShow.HasValue && pagesCount == 0)
			{
				_outputSettings.PageToShow = null;
			}

			EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndScrollView();

			GUIUtilities.HorizontalLine();

			EditorGUILayout.BeginHorizontal();

			var textFieldStyle = EditorStyles.textField;
			var prevTextFieldAlignment = textFieldStyle.alignment;
			textFieldStyle.alignment = TextAnchor.MiddleCenter;

			_outputSettings.PathFilter = EditorGUILayout.TextField("Path Contains:",
					_outputSettings.PathFilter, GUILayout.Width(400f));

			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();

#if HUNT_ADDRESSABLES
            _outputSettings.ShowAddressables = EditorGUILayout.Toggle("Show Addressables:", 
                _outputSettings.ShowAddressables);
#endif

			if (!_analysisSettings.FindUnreferencedOnly)
			{
				_outputSettings.ShowUnreferencedOnly = EditorGUILayout.Toggle("Unreferenced Only:",
						_outputSettings.ShowUnreferencedOnly);
			}

			_outputSettings.ShowAssetsWithWarningsOnly = EditorGUILayout.Toggle("Implicitly Unused Only",
					_outputSettings.ShowAssetsWithWarningsOnly);

			textFieldStyle.alignment = prevTextFieldAlignment;

			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();

			prevColor = GUI.color;

			var sortType = _outputSettings.SortType;

			GUI.color = sortType == 0 || sortType == 1 ? Color.yellow : Color.white;
			var orderType = sortType == 1 ? "Z-A" : "A-Z";
			if (GUILayout.Button("Sort by type " + orderType, GUILayout.Width(150f)))
			{
				SortByType();
			}

			GUI.color = sortType == 2 || sortType == 3 ? Color.yellow : Color.white;
			orderType = sortType == 3 ? "Z-A" : "A-Z";
			if (GUILayout.Button("Sort by path " + orderType, GUILayout.Width(150f)))
			{
				SortByPath();
			}

			GUI.color = sortType == 4 || sortType == 5 ? Color.yellow : Color.white;
			orderType = sortType == 5 ? "Z-A" : "A-Z";
			if (GUILayout.Button("Sort by size " + orderType, GUILayout.Width(150f)))
			{
				SortBySize();
			}

			GUI.color = prevColor;

			EditorGUILayout.EndHorizontal();

			GUIUtilities.HorizontalLine();

			_typesScroll = EditorGUILayout.BeginScrollView(_typesScroll);

			EditorGUILayout.BeginHorizontal();

			prevColor = GUI.color;
			GUI.color = string.IsNullOrEmpty(_outputSettings.TypeFilter) ? Color.yellow : Color.white;

			if (GUILayout.Button("All Types", GUILayout.Width(100f)))
			{
				_outputSettings.TypeFilter = string.Empty;
			}

			var prevAlignment = GUI.skin.button.alignment;
			GUI.skin.button.alignment = TextAnchor.MiddleLeft;

			foreach (var typeInfo in _result.RefsByTypes)
			{
				GUI.color = _outputSettings.TypeFilter == typeInfo.Key ? Color.yellow : Color.white;

				var typeName = typeInfo.Key;
				var dotIndex = typeName.LastIndexOf(".", StringComparison.Ordinal);

				if (dotIndex != -1 && dotIndex + 1 < typeName.Length - 3)
				{
					typeName = typeName.Substring(dotIndex + 1);
				}

				if (GUILayout.Button($"[{typeInfo.Value}] {typeName}", GUILayout.Width(150f)))
				{
					_outputSettings.TypeFilter = typeInfo.Key;
				}
			}

			GUI.skin.button.alignment = prevAlignment;
			GUI.color = prevColor;

			EditorGUILayout.EndHorizontal();

			EditorGUILayout.EndScrollView();

			GUIUtilities.HorizontalLine();

			_assetsScroll = GUILayout.BeginScrollView(_assetsScroll);

			EditorGUILayout.BeginVertical();

			for (var i = 0; i < filteredAssets.Count; i++)
			{
				if (_outputSettings.PageToShow.HasValue)
				{
					var page = _outputSettings.PageToShow.Value;
					if (i < page * OutputSettings.PageSize || i >= (page + 1) * OutputSettings.PageSize)
					{
						continue;
					}
				}

				var asset = filteredAssets[i];
				EditorGUILayout.BeginHorizontal();

				prevColor = GUI.color;

				var color = Color.white;
				if (!asset.ValidType)
				{
					color = Color.red;
				}
				else if (!string.IsNullOrEmpty(asset.Warning))
				{
					color = Color.yellow;
				}

				GUI.color = color;

				if (string.IsNullOrEmpty(asset.Warning))
				{
					EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(40f));
				}
				else
				{
					asset.Foldout = EditorGUILayout.Foldout(asset.Foldout, i + " (i)");
				}

				EditorGUILayout.LabelField(asset.TypeName, GUILayout.Width(75f));
				GUI.color = prevColor;

				if (asset.ValidType)
				{
					var guiContent = EditorGUIUtility.ObjectContent(null, asset.Type);
					guiContent.text = Path.GetFileName(asset.Path);

					var alignment = GUI.skin.button.alignment;
					GUI.skin.button.alignment = TextAnchor.MiddleLeft;

					if (GUILayout.Button(guiContent, GUILayout.Width(300f), GUILayout.Height(18f)))
					{
						Selection.objects = new[] { AssetDatabase.LoadMainAssetAtPath(asset.Path) };
					}

					GUI.skin.button.alignment = alignment;
				}

				EditorGUILayout.LabelField(asset.ReadableSize, GUILayout.Width(70f));

#if HUNT_ADDRESSABLES
                if (_outputSettings.ShowAddressables)
                {
                    EditorGUILayout.LabelField(asset.IsAddressable ? "Addressable" : string.Empty,
                        GUILayout.Width(70f));
                }
#endif

				prevColor = GUI.color;

				GUI.color = asset.ReferencesCount > 0 ? Color.white : Color.yellow;

				EditorGUILayout.LabelField($"Refs:{asset.ReferencesCount}",
						GUILayout.Width(70f));

				GUI.color = prevColor;

				EditorGUILayout.LabelField(asset.ShortPath);

				EditorGUILayout.EndHorizontal();

				if (asset.Foldout)
				{
					EditorGUILayout.LabelField($"[{asset.Warning}]");
				}
			}

			GUILayout.FlexibleSpace();

			EditorGUILayout.EndVertical();
			GUILayout.EndScrollView();
		}

		private void OnAnalysisSettingsGUI()
		{
			EnsurePatternsLoaded();

			_analysisSettingsFoldout = EditorGUILayout.Foldout(_analysisSettingsFoldout,
					$"Analysis Settings. Patterns Ignored in Output: {_analysisSettings.IgnoredPatterns.Count}. "
					+ (_analysisSettings.FindUnreferencedOnly ? "Listing unreferenced assets only" : "Listing all assets"));

			if (!_analysisSettingsFoldout)
				return;

			EditorGUILayout.LabelField("Any changes here will be applied to the next run", GUILayout.Width(350f));

			GUIUtilities.HorizontalLine();

			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.LabelField("Show Unreferenced Assets Only");
			_analysisSettings.FindUnreferencedOnly = EditorGUILayout.Toggle(string.Empty,
					_analysisSettings.FindUnreferencedOnly);
			GUILayout.FlexibleSpace();

			EditorGUILayout.EndHorizontal();

			EditorGUILayout.LabelField("* Uncheck to list all assets with their references count", GUILayout.Width(350f));

			GUIUtilities.HorizontalLine();

			var isPatternsListDirty = false;

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Format: RegExp patterns");
			if (GUILayout.Button("Set Default", GUILayout.Width(300f)))
			{
				_analysisSettings.IgnoredPatterns = _analysisSettings.DefaultIgnorePatterns.ToList();
				isPatternsListDirty = true;
			}

			if (GUILayout.Button("Save to Clipboard"))
			{
				var contents = _analysisSettings.IgnoredPatterns.Aggregate("Patterns:",
						(current, t) => current + "\n" + t);

				EditorGUIUtility.systemCopyBuffer = contents;
			}

			EditorGUILayout.EndHorizontal();

			var newCount = Mathf.Max(0, EditorGUILayout.IntField("Count:", _analysisSettings.IgnoredPatterns.Count));

			if (newCount != _analysisSettings.IgnoredPatterns.Count)
			{
				isPatternsListDirty = true;
			}

			while (newCount < _analysisSettings.IgnoredPatterns.Count)
			{
				_analysisSettings.IgnoredPatterns.RemoveAt(_analysisSettings.IgnoredPatterns.Count - 1);
			}

			if (newCount > _analysisSettings.IgnoredPatterns.Count)
			{
				for (var i = _analysisSettings.IgnoredPatterns.Count; i < newCount; i++)
				{
					_analysisSettings.IgnoredPatterns.Add(EditorPrefs.GetString($"{AnalysisSettings.PATTERNS_PREFS_KEY}_{i}"));
				}
			}

			for (var i = 0; i < _analysisSettings.IgnoredPatterns.Count; i++)
			{
				var newValue = EditorGUILayout.TextField(_analysisSettings.IgnoredPatterns[i]);
				if (_analysisSettings.IgnoredPatterns[i] != newValue)
				{
					isPatternsListDirty = true;
					_analysisSettings.IgnoredPatterns[i] = newValue;
				}
			}

			if (isPatternsListDirty)
			{
				SavePatterns();
			}
		}

		private void EnsurePatternsLoaded()
		{
			// ReSharper disable once ConvertIfStatementToNullCoalescingAssignment
			if (_analysisSettings == null)
			{
				_analysisSettings = new AnalysisSettings();
			}

			if (_analysisSettings.IgnoredPatterns != null)
			{
				return;
			}

			var count = EditorPrefs.GetInt(AnalysisSettings.PATTERNS_PREFS_KEY, -1);

			if (count == -1)
			{
				_analysisSettings.IgnoredPatterns = _analysisSettings.DefaultIgnorePatterns.ToList();
			}
			else
			{
				_analysisSettings.IgnoredPatterns = new List<string>();

				for (var i = 0; i < count; i++)
				{
					_analysisSettings.IgnoredPatterns.Add(EditorPrefs.GetString($"{AnalysisSettings.PATTERNS_PREFS_KEY}_{i}"));
				}
			}
		}

		private void SavePatterns()
		{
			EditorPrefs.SetInt(AnalysisSettings.PATTERNS_PREFS_KEY, _analysisSettings.IgnoredPatterns.Count);

			for (var i = 0; i < _analysisSettings.IgnoredPatterns.Count; i++)
			{
				EditorPrefs.SetString($"{AnalysisSettings.PATTERNS_PREFS_KEY}_{i}", _analysisSettings.IgnoredPatterns[i]);
			}
		}

		private void SortByType()
		{
			if (_outputSettings.SortType == 0)
			{
				_outputSettings.SortType = 1;
				_result.Assets?.Sort((a, b) =>
						string.Compare(b.TypeName, a.TypeName, StringComparison.Ordinal));
			}
			else
			{
				_outputSettings.SortType = 0;
				_result.Assets?.Sort((a, b) =>
						string.Compare(a.TypeName, b.TypeName, StringComparison.Ordinal));
			}
		}

		private void SortByPath()
		{
			if (_outputSettings.SortType == 2)
			{
				_outputSettings.SortType = 3;
				_result.Assets?.Sort((a, b) =>
						string.Compare(b.Path, a.Path, StringComparison.Ordinal));
			}
			else
			{
				_outputSettings.SortType = 2;
				_result.Assets?.Sort((a, b) =>
						string.Compare(a.Path, b.Path, StringComparison.Ordinal));
			}
		}

		private void SortBySize()
		{
			if (_outputSettings.SortType == 4)
			{
				_outputSettings.SortType = 5;
				_result.Assets?.Sort((b, a) => a.BytesSize.CompareTo(b.BytesSize));
			}
			else
			{
				_outputSettings.SortType = 4;
				_result.Assets?.Sort((a, b) => a.BytesSize.CompareTo(b.BytesSize));
			}
		}

		private void OnDestroy()
		{
			Clear();
		}
	}
}