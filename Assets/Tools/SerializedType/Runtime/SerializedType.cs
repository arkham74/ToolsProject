using System;
using UnityEngine;

namespace JD
{
	[Serializable]
	public struct SerializedType<T> where T : class
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
			Type type = Type.GetType(fullName, true);
			object objectInstance = Activator.CreateInstance(type);
			T typeInstance = objectInstance as T;
			return typeInstance;
		}

		// public static implicit operator SerializedType<T>(string fullName) => new SerializedType<T>(fullName);
		// public static implicit operator string(SerializedType<T> serializedType) => serializedType.fullName;

		// public static implicit operator Type(SerializedType<T> serializedType) => Type.GetType(serializedType);
		// public static implicit operator SerializedType<T>(Type type) => new SerializedType<T>(type.FullName);
	}
}
