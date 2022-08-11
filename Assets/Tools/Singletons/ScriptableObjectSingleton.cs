using System;
using UnityEngine;

namespace JD
{
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
	}
}