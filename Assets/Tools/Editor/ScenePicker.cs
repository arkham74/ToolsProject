#if TOOLS_TOOLBAR
using System.Collections.Generic;
using UnityEngine;
using UnityToolbarExtender;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor.Experimental.GraphView;
using System;

namespace JD.Editor
{
	[InitializeOnLoad]
	public class ScenePicker : ScriptableObject, ISearchWindowProvider
	{
		private static Texture2D icon;

		static ScenePicker()
		{
			ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
		}

		public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
		{
			LoadIcon(ref icon, "d_Scene");
			List<SearchTreeEntry> list = new List<SearchTreeEntry>();
			list.Add(new SearchTreeGroupEntry(new GUIContent("Scenes", icon)));

			EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
			for (int i = 0; i < scenes.Length; i++)
			{
				string path = scenes[i].path;
				string name = Path.GetFileNameWithoutExtension(path);
				string text = $" {name} ({i}) ";
				GUIContent content = new GUIContent(text, icon);
				SearchTreeEntry item = new SearchTreeEntry(content)
				{
					userData = path,
					level = 1,
				};
				list.Add(item);
			}

			return list;
		}

		public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
		{
			EditorSceneManager.OpenScene((string)entry.userData, OpenSceneMode.Single);
			return true;
		}

		static void OnToolbarGUI()
		{
			Scene activeScene = EditorSceneManager.GetActiveScene();
			EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

			if (scenes.Length < 1)
			{
				if (GUILayout.Button(" Add active scene to build settings ", EditorStyles.toolbarButton))
				{
					EditorBuildSettingsScene editorScene = new EditorBuildSettingsScene(activeScene.path, true);
					List<EditorBuildSettingsScene> ascenes = new List<EditorBuildSettingsScene>(scenes);
					ascenes.Add(editorScene);
					EditorBuildSettings.scenes = ascenes.ToArray();
				}
			}
			else
			{
				LoadIcon(ref icon, "d_Scene");
				Vector2 mousePosition = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
				mousePosition.y += 30;
				SearchWindowContext context = new SearchWindowContext(mousePosition);
				ScenePicker provider = ScriptableObject.CreateInstance<ScenePicker>();
				if (GUILayout.Button(new GUIContent($" {activeScene.name} ({activeScene.buildIndex}) ", icon), EditorStyles.toolbarPopup))
				{
					SearchWindow.Open(context, provider);
				}
			}

			GUILayout.FlexibleSpace();
		}

		private static void LoadIcon(ref Texture2D icon, string name)
		{
			if (icon == null)
			{
				icon = EditorGUIUtility.FindTexture(name);
			}
		}
	}
}
#else
using UnityEditor;
using UnityEditor.PackageManager;

namespace JD.Editor
{
	[InitializeOnLoad]
	public class ScenePicker
	{
		static ScenePicker()
		{
			Client.Add("https://github.com/marijnz/unity-toolbar-extender.git");
		}
	}
}
#endif