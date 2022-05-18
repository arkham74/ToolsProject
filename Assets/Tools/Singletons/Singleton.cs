using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance;
	public static bool Valid => instance != null;

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
				}
				else
				{
					Debug.LogError($"There is no '{typeof(T)}' present in scene");
				}

				if (type.Length > 1)
				{
					Debug.LogError($"There is more than one '{typeof(T)}'");
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