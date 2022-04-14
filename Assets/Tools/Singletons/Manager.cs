using System;
using UnityEngine;

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
		// Debug.Log(typeName + " initialized", Instance);
	}
}