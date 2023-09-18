using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

#if TOOLS_NAUATTR
using NaughtyAttributes;
#endif

namespace JD.Shared
{
	public abstract class SharedAction<T> : ScriptableObject
	{
		public event Action<T> OnValueChanged = delegate { };

		public void Invoke(T value)
		{
			OnValueChanged.Invoke(value);
		}
	}
}