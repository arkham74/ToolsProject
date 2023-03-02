#if TOOLS_TOOLBAR
using System.Collections.Generic;
using UnityEngine;
using UnityToolbarExtender;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.IO;

namespace JD.Editor
{
	[InitializeOnLoad]
	public class ScenePicker
	{
		private static string[] sceneNames;

		static ScenePicker()
		{
			ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
		}

		static void OnToolbarGUI()
		{
			EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
			int sceneCount = scenes.Length;
			Scene activeScene = EditorSceneManager.GetActiveScene();

			if (sceneCount < 1)
			{
				if (GUILayout.Button("Add active scene to build settings", EditorStyles.toolbarButton))
				{
					EditorBuildSettingsScene editorScene = new EditorBuildSettingsScene(activeScene.path, true);
					List<EditorBuildSettingsScene> ascenes = new List<EditorBuildSettingsScene>(scenes);
					ascenes.Add(editorScene);
					EditorBuildSettings.scenes = ascenes.ToArray();
				}
				GUILayout.FlexibleSpace();
				return;
			}

			if (sceneNames == null || sceneNames.Length != sceneCount)
				sceneNames = new string[sceneCount];

			for (int i = 0; i < sceneCount; i++)
				sceneNames[i] = Path.GetFileNameWithoutExtension(scenes[i].path);

			int index = EditorGUILayout.Popup(activeScene.buildIndex, sceneNames, EditorStyles.toolbarPopup);

			if (index != activeScene.buildIndex)
				EditorSceneManager.OpenScene(scenes[index].path, OpenSceneMode.Single);

			GUILayout.FlexibleSpace();
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