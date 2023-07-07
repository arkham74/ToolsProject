using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JD
{
	public static class GameTools
	{
		public static bool IsSceneLoaded(string sceneName)
		{
			return SceneManager.GetSceneByName(sceneName).name == sceneName;
		}

		public static void LoadIfNotLoaded(string sceneName, LoadSceneMode loadSceneMode)
		{
			if (!IsSceneLoaded(sceneName))
			{
				SceneManager.LoadScene(sceneName, loadSceneMode);
			}
		}

		public static T Instantiate<T>(T prefab, Transform transform) where T : Object
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				return PrefabUtility.InstantiatePrefab(prefab, transform) as T;
			}
			else
#endif
			{
				return Object.Instantiate(prefab, transform);
			}
		}

		public static void Quit()
		{
#if UNITY_EDITOR
			EditorApplication.ExitPlaymode();
#else
			Application.Quit();
#endif
		}
	}
}
