using System;
using UnityEngine;

namespace JD
{
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T instance;
		public static bool Valid => instance != null;

		// [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		// public static void Init()
		// {
		// 	instance = null;
		// }

		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					T[] type = FindObjectsOfType<T>(true);
					if (type.Length > 0)
					{
						instance = type[0];
						if (type.Length > 1)
						{
							Debug.LogError($"There is more than one '{typeof(T)}' in scene");
						}
					}
					else
					{
						throw new Exception($"There is no '{typeof(T)}' present in scene");
					}
				}
				return instance;
			}
		}

		protected virtual void Awake()
		{
			if (instance == null)
			{
				instance = this as T;
			}
			else if (instance != this)
			{
				Destroy(gameObject);
			}
		}

		protected virtual void OnDestroy()
		{
			if (instance == this)
			{
				instance = null;
			}
		}
	}
}