using UnityEngine;


public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T level;

	public static bool Valid => level != null;

	public static T Instance
	{
		get
		{
			if (level == null)
				level = FindObjectOfType<T>(true);
			return level;
		}
	}

	protected virtual void Awake()
	{
		if (level == null)
		{
			level = this as T;
		}
		else if (level != this)
		{
			Destroy(gameObject);
		}
	}

	protected virtual void OnDestroy()
	{
		if (level == this)
		{
			level = null;
		}
	}
}