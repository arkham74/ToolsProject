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
	public abstract class Shared<T> : ScriptableObject
	{
		#if TOOLS_NAUATTR
		[ShowNonSerializedField]
		#endif
		private T value;

		public event Action<T> OnValueChanged;

		public T Value
		{
			get => value;
			set
			{
				if (value.Equals(this.value))
				{
					return;
				}

				this.value = value;
				if (OnValueChanged != null)
				{
					OnValueChanged(Value);
				}
			}
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		public static implicit operator T(Shared<T> shared) => shared.value;
	}
}