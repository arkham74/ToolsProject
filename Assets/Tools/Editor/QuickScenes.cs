using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Overlays;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[Overlay(typeof(SceneView), "Quick Scenes")]
public class QuickScenes : ToolbarOverlay
{
	private const string ButtonID = "QuickScenes/LoadSceneButton";
	public QuickScenes() : base(ButtonID) { }

	[EditorToolbarElement(ButtonID, typeof(SceneView))]
	private class LoadSceneButton : EditorToolbarButton
	{
		private const float Width = 300;
		private const float Height = 500;
		private readonly ScenesSearchWindowProvider provider;
		private static Texture2D sceneIcon;

		public LoadSceneButton()
		{
			provider = ScriptableObject.CreateInstance<ScenesSearchWindowProvider>();
			text = "Load";

			if (sceneIcon == null)
				sceneIcon = EditorGUIUtility.FindTexture("d_Scene");

			icon = sceneIcon;
			clicked += ShowDropdown;
		}

		private void ShowDropdown()
		{
			SearchWindowContext context = new SearchWindowContext(new Vector2(900, 450), Width, Height);
			SearchWindow.Open(context, provider);
		}

		private class ScenesSearchWindowProvider : ScriptableObject, ISearchWindowProvider
		{
			private List<SearchTreeEntry> list = new List<SearchTreeEntry>();

			public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
			{
				if (list.Count == 0)
				{
					list.Add(new SearchTreeGroupEntry(new GUIContent("Scenes")));
					foreach (EditorBuildSettingsScene buildScene in EditorBuildSettings.scenes)
					{
						string sceneName = Path.GetFileNameWithoutExtension(buildScene.path);
						GUIContent content = new GUIContent(sceneName, sceneIcon);
						SearchTreeEntry entry = new SearchTreeEntry(content)
						{
							userData = buildScene,
							level = 1,
						};
						list.Add(entry);
					}
				}

				return list;
			}

			public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
			{
				EditorBuildSettingsScene buildScene = entry.userData as EditorBuildSettingsScene;
				EditorSceneManager.OpenScene(buildScene.path);
				return true;
			}
		}
	}
}