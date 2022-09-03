using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace DependenciesHunter
{
	/// <summary>
	/// Lists all unreferenced assets in a project.
	/// </summary>
	public class AllProjectAssetsReferencesWindow : EditorWindow
	{
		private ProjectAssetsAnalysisHelper _service;

		private readonly List<string> _unusedAssets = new List<string>();

		// ReSharper disable once InconsistentNaming
		private const string PATTERNS_PREFS_KEY = "DependencyHunterIgnorePatterns";

		private int? _pageToShow;
		private const int PageSize = 50;

		private Vector2 _pagesScroll = Vector2.zero;
		private Vector2 _assetsScroll = Vector2.zero;

		private bool _launchedAtLeastOnce;

		// ReSharper disable once StringLiteralTypo
		private readonly List<string> _defaultIgnorePatterns = new List<string>
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

		private List<string> _ignoreInOutputPatterns;
		private bool _ignorePatternsFoldout;

		[MenuItem("Tools/Dependencies Hunter")]
		public static void LaunchUnreferencedAssetsWindow()
		{
			GetWindow<AllProjectAssetsReferencesWindow>();
		}

		private void ListAllUnusedAssetsInProject()
		{
			_pageToShow = null;
			_launchedAtLeastOnce = true;

			if (_service == null)
			{
				_service = new ProjectAssetsAnalysisHelper();
			}

			Clear();

			Show();

			DependenciesMapUtilities.FillReverseDependenciesMap(out Dictionary<string, List<string>> map);

			EditorUtility.ClearProgressBar();

			StringBuilder filteredOutput = new StringBuilder();
			filteredOutput.AppendLine("Assets ignored by pattern:");

			int count = 0;
			foreach (KeyValuePair<string, List<string>> mapElement in map)
			{
				EditorUtility.DisplayProgressBar("Unreferenced Assets", "Searching for unreferenced assets", (float) count / map.Count);
				count++;

				if (mapElement.Value.Count == 0)
				{
					bool validAssetType = _service.IsValidAssetType(mapElement.Key);
					bool validForOutput = false;

					if (validAssetType)
					{
						validForOutput = _service.IsValidForOutput(mapElement.Key, _ignoreInOutputPatterns);

						if (!validForOutput)
						{
							filteredOutput.AppendLine(mapElement.Key);
						}
					}

					if (validForOutput)
					{
						_unusedAssets.Add(mapElement.Key);
					}
				}
			}

			EditorUtility.ClearProgressBar();

			Debug.Log(filteredOutput.ToString());
			filteredOutput.Clear();
		}

		private void Clear()
		{
			_unusedAssets.Clear();
			EditorUtility.UnloadUnusedAssetsImmediate();
		}

		private void OnGUI()
		{
			EditorGUILayout.Separator();

			OnPatternsGUI();

			EditorGUILayout.Separator();

			if (GUILayout.Button("Run Analysis", GUILayout.Width(300f)))
			{
				ListAllUnusedAssetsInProject();
			}

			EditorGUILayout.Separator();

			if (_launchedAtLeastOnce)
			{
				EditorGUILayout.LabelField($"Unreferenced Assets: {_unusedAssets.Count}");

				if (_unusedAssets.Count > 0)
				{
					_pagesScroll = EditorGUILayout.BeginScrollView(_pagesScroll);

					EditorGUILayout.BeginHorizontal();

					Color prevColor = GUI.color;
					GUI.color = !_pageToShow.HasValue ? Color.yellow : Color.white;

					if (GUILayout.Button("All", GUILayout.Width(30f)))
					{
						_pageToShow = null;
					}

					GUI.color = prevColor;

					int totalCount = _unusedAssets.Count;
					int pagesCount = totalCount / PageSize + ( totalCount % PageSize > 0 ? 1 : 0 );

					for (int i = 0; i < pagesCount; i++)
					{
						prevColor = GUI.color;
						GUI.color = _pageToShow == i ? Color.yellow : Color.white;

						if (GUILayout.Button(( i + 1 ).ToString(), GUILayout.Width(30f)))
						{
							_pageToShow = i;
						}

						GUI.color = prevColor;
					}

					EditorGUILayout.EndHorizontal();

					EditorGUILayout.EndScrollView();
				}
			}

			EditorGUILayout.Separator();

			_assetsScroll = GUILayout.BeginScrollView(_assetsScroll);

			EditorGUILayout.BeginVertical();

			for (int i = 0; i < _unusedAssets.Count; i++)
			{
				if (_pageToShow.HasValue)
				{
					int page = _pageToShow.Value;
					if (i < page * PageSize || i >= ( page + 1 ) * PageSize)
					{
						continue;
					}
				}

				string unusedAssetPath = _unusedAssets[i];
				EditorGUILayout.BeginHorizontal();

				Type type = AssetDatabase.GetMainAssetTypeAtPath(unusedAssetPath);
				string typeName = type.ToString();
				typeName = typeName.Replace("UnityEngine.", string.Empty);
				typeName = typeName.Replace("UnityEditor.", string.Empty);

				EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(40f));
				EditorGUILayout.LabelField(typeName, GUILayout.Width(150f));

				GUIContent guiContent = EditorGUIUtility.ObjectContent(null, type);
				guiContent.text = Path.GetFileName(unusedAssetPath);

				TextAnchor alignment = GUI.skin.button.alignment;
				GUI.skin.button.alignment = TextAnchor.MiddleLeft;

				if (GUILayout.Button(guiContent, GUILayout.Width(300f), GUILayout.Height(18f)))
				{
					Selection.objects = new[] {AssetDatabase.LoadMainAssetAtPath(unusedAssetPath)};
				}

				GUI.skin.button.alignment = alignment;

				EditorGUILayout.LabelField(unusedAssetPath);

				EditorGUILayout.EndHorizontal();
			}

			GUILayout.FlexibleSpace();

			EditorGUILayout.EndVertical();
			GUILayout.EndScrollView();
		}

		private void OnPatternsGUI()
		{
			EnsurePatternsLoaded();

			_ignorePatternsFoldout = EditorGUILayout.Foldout(_ignorePatternsFoldout, "Ignored in Output Assets Patterns");

			if (!_ignorePatternsFoldout)
			{
				return;
			}

			bool isDirty = false;

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Format: RegExp patterns");
			if (GUILayout.Button("Set Default", GUILayout.Width(300f)))
			{
				_ignoreInOutputPatterns = _defaultIgnorePatterns.ToList();
				isDirty = true;
			}

			if (GUILayout.Button("Save to Clipboard"))
			{
				string contents = _ignoreInOutputPatterns.Aggregate("Patterns:", (current, t) => current + ( "\n" + t ));

				EditorGUIUtility.systemCopyBuffer = contents;
			}

			EditorGUILayout.EndHorizontal();

			int newCount = Mathf.Max(0, EditorGUILayout.IntField("Count:", _ignoreInOutputPatterns.Count));

			if (newCount != _ignoreInOutputPatterns.Count)
			{
				isDirty = true;
			}

			while (newCount < _ignoreInOutputPatterns.Count)
			{
				_ignoreInOutputPatterns.RemoveAt(_ignoreInOutputPatterns.Count - 1);
			}

			if (newCount > _ignoreInOutputPatterns.Count)
			{
				for (int i = _ignoreInOutputPatterns.Count; i < newCount; i++)
				{
					_ignoreInOutputPatterns.Add(EditorPrefs.GetString($"{PATTERNS_PREFS_KEY}_{i}"));
				}
			}

			for (int i = 0; i < _ignoreInOutputPatterns.Count; i++)
			{
				string newValue = EditorGUILayout.TextField(_ignoreInOutputPatterns[i]);
				if (_ignoreInOutputPatterns[i] != newValue)
				{
					isDirty = true;
					_ignoreInOutputPatterns[i] = newValue;
				}
			}

			if (isDirty)
			{
				SavePatterns();
			}
		}

		private void EnsurePatternsLoaded()
		{
			if (_ignoreInOutputPatterns != null)
			{
				return;
			}

			int count = EditorPrefs.GetInt(PATTERNS_PREFS_KEY, -1);

			if (count == -1)
			{
				_ignoreInOutputPatterns = _defaultIgnorePatterns.ToList();
			}
			else
			{
				_ignoreInOutputPatterns = new List<string>();

				for (int i = 0; i < count; i++)
				{
					_ignoreInOutputPatterns.Add(EditorPrefs.GetString($"{PATTERNS_PREFS_KEY}_{i}"));
				}
			}
		}

		private void SavePatterns()
		{
			EditorPrefs.SetInt(PATTERNS_PREFS_KEY, _ignoreInOutputPatterns.Count);

			for (int i = 0; i < _ignoreInOutputPatterns.Count; i++)
			{
				EditorPrefs.SetString($"{PATTERNS_PREFS_KEY}_{i}", _ignoreInOutputPatterns[i]);
			}
		}

		private void OnDestroy()
		{
			Clear();
		}
	}

	/// <summary>
	/// Lists all references of the selected assets.
	/// </summary>
	public class SelectedAssetsReferencesWindow : EditorWindow
	{
		private SelectedAssetsAnalysisHelper _service;

		private const float TabLength = 60f;
		private const TextAnchor ResultButtonAlignment = TextAnchor.MiddleLeft;

		private Dictionary<Object, List<string>> _lastResults;

		private Object[] _selectedObjects;

		private bool[] _selectedObjectsFoldouts;

		private float _workTime;

		private Vector2 _scrollPos = Vector2.zero;
		private Vector2[] _foldoutsScrolls;

		[MenuItem("Assets/Find References in Project", false, 20)]
		public static void FindReferences()
		{
			SelectedAssetsReferencesWindow window = GetWindow<SelectedAssetsReferencesWindow>();
			window.Start();
		}

		private void Start()
		{
			if (_service == null)
			{
				_service = new SelectedAssetsAnalysisHelper();
			}

			Show();

			float startTime = Time.realtimeSinceStartup;

			_selectedObjects = Selection.objects;

			_lastResults = _service.GetReferences(_selectedObjects);

			EditorUtility.DisplayProgressBar("DependenciesHunter", "Preparing Assets", 1f);
			EditorUtility.UnloadUnusedAssetsImmediate();
			EditorUtility.ClearProgressBar();

			_workTime = Time.realtimeSinceStartup - startTime;
			_selectedObjectsFoldouts = new bool[_selectedObjects.Length];
			if (_selectedObjectsFoldouts.Length == 1)
			{
				_selectedObjectsFoldouts[0] = true;
			}

			_foldoutsScrolls = new Vector2[_selectedObjectsFoldouts.Length];
		}

		private void Clear()
		{
			_selectedObjects = null;
			_service = null;

			EditorUtility.UnloadUnusedAssetsImmediate();
		}

		private void OnGUI()
		{
			if (_lastResults == null)
			{
				return;
			}

			if (_selectedObjects == null || _selectedObjects.Any(selectedObject => selectedObject == null))
			{
				Clear();
				return;
			}

			GUILayout.BeginVertical();

			GUILayout.Label($"Analysis done in: {_workTime} s");

			Dictionary<Object, List<string>> results = _lastResults;

			_scrollPos = GUILayout.BeginScrollView(_scrollPos);

			for (int i = 0; i < _selectedObjectsFoldouts.Length; i++)
			{
				EditorGUILayout.Separator();

				GUILayout.BeginHorizontal();

				List<string> dependencies = results[_selectedObjects[i]];

				_selectedObjectsFoldouts[i] = EditorGUILayout.Foldout(_selectedObjectsFoldouts[i], string.Empty);
				EditorGUILayout.ObjectField(_selectedObjects[i], typeof(Object), true);

				string content = dependencies.Count > 0 ? $"Dependencies: {dependencies.Count}" : "No dependencies found";
				EditorGUILayout.LabelField(content);

				GUILayout.EndHorizontal();

				if (_selectedObjectsFoldouts[i])
				{
					_foldoutsScrolls[i] = GUILayout.BeginScrollView(_foldoutsScrolls[i]);

					foreach (string resultPath in dependencies)
					{
						EditorGUILayout.BeginHorizontal();

						GUILayout.Space(TabLength);

						Type type = AssetDatabase.GetMainAssetTypeAtPath(resultPath);
						GUIContent guiContent = EditorGUIUtility.ObjectContent(null, type);
						guiContent.text = Path.GetFileName(resultPath);

						TextAnchor alignment = GUI.skin.button.alignment;
						GUI.skin.button.alignment = ResultButtonAlignment;

						if (GUILayout.Button(guiContent, GUILayout.MinWidth(300f), GUILayout.Height(18f)))
						{
							Selection.objects = new[] {AssetDatabase.LoadMainAssetAtPath(resultPath)};
						}

						GUI.skin.button.alignment = alignment;

						EditorGUILayout.EndHorizontal();
					}

					GUILayout.EndScrollView();
				}
			}

			GUILayout.EndScrollView();
		}

		private void OnProjectChange()
		{
			Clear();
		}

		private void OnDestroy()
		{
			Clear();
		}
	}

	public class ProjectAssetsAnalysisHelper
	{
		private List<string> _iconPaths;

		public bool IsValidAssetType(string path)
		{
			Type type = AssetDatabase.GetMainAssetTypeAtPath(path);

			if (type == typeof(MonoScript) || type == typeof(DefaultAsset))
			{
				return false;
			}

			if (type == typeof(SceneAsset))
			{
				EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

				if (scenes.Any(scene => scene.path == path))
				{
					return false;
				}
			}

			if (type == typeof(Texture2D) && UsedAsProjectIcon(path))
			{
				return false;
			}

			return true;
		}

		public bool IsValidForOutput(string path, List<string> ignoreInOutputPatterns)
		{
			foreach (string pattern in ignoreInOutputPatterns)
			{
				if (!string.IsNullOrEmpty(pattern) && Regex.Match(path, pattern).Success)
				{
					return false;
				}
			}

			return true;
		}

		private bool UsedAsProjectIcon(string texturePath)
		{
			if (_iconPaths == null)
			{
				FindAllIcons();
			}

			return _iconPaths.Contains(texturePath);
		}

		private void FindAllIcons()
		{
			_iconPaths = new List<string>();

			List<Texture2D> icons = new List<Texture2D>();
			Array targetGroups = Enum.GetValues(typeof(BuildTargetGroup));

			foreach (object targetGroup in targetGroups)
			{
				icons.AddRange(PlayerSettings.GetIconsForTargetGroup((BuildTargetGroup) targetGroup));
			}

			foreach (Texture2D icon in icons)
			{
				_iconPaths.Add(AssetDatabase.GetAssetPath(icon));
			}
		}
	}

	public class SelectedAssetsAnalysisHelper
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

			GetDependencies(selectedObjects, _cachedAssetsMap, out Dictionary<Object, List<string>> result);

			return result;
		}

		private static void GetDependencies(IEnumerable<Object> selectedObjects, IReadOnlyDictionary<string, List<string>> source, out Dictionary<Object, List<string>> results)
		{
			results = new Dictionary<Object, List<string>>();

			foreach (Object selectedObject in selectedObjects)
			{
				string selectedObjectPath = AssetDatabase.GetAssetPath(selectedObject);

				if (source.ContainsKey(selectedObjectPath))
				{
					results.Add(selectedObject, source[selectedObjectPath]);
				}
				else
				{
					Debug.LogWarning("Dependencies Hunter doesn't contain the specified object in the assets map", selectedObject);
					results.Add(selectedObject, new List<string>());
				}
			}
		}
	}

	public class DependenciesMapUtilities
	{
		public static void FillReverseDependenciesMap(out Dictionary<string, List<string>> reverseDependencies)
		{
			List<string> assetPaths = AssetDatabase.GetAllAssetPaths().ToList();

			reverseDependencies = assetPaths.ToDictionary(assetPath => assetPath, assetPath => new List<string>());

			Debug.Log($"Total Assets Count: {assetPaths.Count}");

			for (int i = 0; i < assetPaths.Count; i++)
			{
				EditorUtility.DisplayProgressBar("Dependencies Hunter", "Creating a map of dependencies", (float) i / assetPaths.Count);

				string[] assetDependencies = AssetDatabase.GetDependencies(assetPaths[i], false);

				foreach (string assetDependency in assetDependencies)
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
