using System;
using IngameDebugConsole;
using Tayx.Graphy;
using UnityEngine;

public static class InitializeManagers
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	public static void Init()
	{
		FrameGraph.Init();
		DebugLogManager.Init();
	}
}

public abstract class Manager<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T Instance { get; private set; }
	public static bool IsInitialized => Instance != null;

	public static void Init()
	{
		string typeName = typeof(T).Name;
		T prefab = Resources.Load<T>($"Managers/{typeName}");
		Instance = Instantiate(prefab);
		Instance.name = typeName;
		DontDestroyOnLoad(Instance.gameObject);
	}
}