// #define HUNT_ADDRESSABLES

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using JD;
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
	/// <summary>
	/// Lists all references of the selected assets.
	/// </summary>
	public class SelectedAssetsReferencesWindow : EditorWindow
	{
		private SelectedAssetsAnalysisUtilities _service;

		private const float TabLength = 60f;

		private Dictionary<Object, List<string>> _lastResults;

		private Object[] _selectedObjects;

		private bool[] _selectedObjectsFoldouts;

		private float _workTime;

		private Vector2 _scrollPos = Vector2.zero;
		private Vector2[] _foldoutsScrolls;

		[MenuItem("Assets/Find References In Project", false, 20)]
		public static void FindReferences()
		{
			var window = GetWindow<SelectedAssetsReferencesWindow>("Find References");
			window.Start();
		}

		private void Start()
		{
			// ReSharper disable once ConvertIfStatementToNullCoalescingAssignment
			if (_service == null)
			{
				_service = new SelectedAssetsAnalysisUtilities();
			}

			Show();

			var startTime = Time.realtimeSinceStartup;

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
			if (_lastResults == null) return;
			if (_selectedObjects == null) return;

			_scrollPos = GUILayout.BeginScrollView(_scrollPos);

			EditorGUI.indentLevel++;
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Work time:", _workTime.ToString());
			EditorGUILayout.Separator();
			foreach (Object selected in _selectedObjects)
			{
				List<string> list = _lastResults[selected];

				EditorGUILayout.ObjectField(selected, selected.GetType(), false);

				EditorGUI.indentLevel++;
				EditorGUILayout.Separator();
				EditorGUILayout.LabelField("Dependencies:", list.Count.ToString());
				EditorGUILayout.Separator();
				foreach (string depenPath in list)
				{
					Object depen = AssetDatabase.LoadMainAssetAtPath(depenPath);
					EditorGUILayout.ObjectField(depen, depen.GetType(), false);
				}
				EditorGUILayout.Separator();
				EditorGUI.indentLevel--;
			}
			EditorGUILayout.Separator();
			EditorGUI.indentLevel--;
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
}