#if TOOLS_TOOLBAR
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Serialization;
using TMPro;
using JD;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tools = JD.Tools;
using UnityToolbarExtender;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.IO;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace CordBot
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
				if (GUILayout.Button("Add active scene to build settings"))
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

			int index = EditorGUILayout.Popup(activeScene.buildIndex, sceneNames);

			if (index != activeScene.buildIndex)
				EditorSceneManager.OpenScene(scenes[index].path, OpenSceneMode.Single);

			GUILayout.FlexibleSpace();
		}
	}
}
#endif