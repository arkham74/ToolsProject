using System;
using UnityEngine;

namespace JD
{
	[Serializable]
	public struct SerializedType<T>
	{
		[SerializeField] private string fullName;

		private SerializedType(string fullName)
		{
			this.fullName = fullName;
		}

		public override string ToString()
		{
			return fullName;
		}

		public T CreateInstance()
		{
			Type type = Type.GetType(fullName);
			object v = Activator.CreateInstance(type);
			T v1 = (T)v;
			Debug.LogWarning(fullName);
			Debug.LogWarning(type);
			Debug.LogWarning(v);
			Debug.LogWarning(v1);
			return v1;
		}

		// public static implicit operator SerializedType<T>(string fullName) => new SerializedType<T>(fullName);
		// public static implicit operator string(SerializedType<T> serializedType) => serializedType.fullName;

		// public static implicit operator Type(SerializedType<T> serializedType) => Type.GetType(serializedType);
		// public static implicit operator SerializedType<T>(Type type) => new SerializedType<T>(type.FullName);
	}
}
