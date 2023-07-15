using System;
using UnityEngine;

namespace JD
{
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T instance;

		public static bool Valid
		{
			get
			{
				Load(true);
				return instance;
			}
		}

		public static T Instance
		{
			get
			{
				Load();
				return instance;
			}
		}

		private static void Load(bool silent = false)
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
				else if (!silent)
				{
					Debug.LogError($"There is no '{typeof(T)}' present in scene");
				}
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