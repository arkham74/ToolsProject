using System;
using UnityEngine;

public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObjectSingleton<T>
{
	private static T instance;
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				string singletonName = typeof(T).Name;
				instance = Resources.Load<T>("Managers/" + singletonName);
				if (instance == null)
				{
					throw new NullReferenceException($"Can't find {singletonName} singleton");
				}
			}

			return instance;
		}
	}

	// private void Awake()
	// {
	// 	Init();
	// 	Debug.Log($"{typeof(T).Name} initialized", Instance);
	// }

	// protected abstract void Init();
}