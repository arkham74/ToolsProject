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

namespace JD.SharedVar
{
	public abstract class Shared<T> : ScriptableObject
	{
		public T m_value;
		public event Action<T> OnValueChanged;

		public T Value
		{
			get => m_value;
			set
			{
				if (value.Equals(m_value))
				{
					return;
				}

				m_value = value;
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
	}
}
