using UnityEditor;
using UnityEditor.SceneManagement;

namespace JD.Editor
{
	public static class ScenesEditor
	{
		[MenuItem("Tools/Prev Scene _F11")] private static void PrevScene() => OpenScene(-1);
		[MenuItem("Tools/Next Scene _F12")] private static void NextScene() => OpenScene(1);

		private static void OpenScene(int offset)
		{
			EditorSceneManager.SaveOpenScenes();
			EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
			int index = EditorSceneManager.GetActiveScene().buildIndex;
			string path = scenes.Repeat(index + offset).path;
			EditorSceneManager.OpenScene(path);
		}
	}
}