using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace JD
{
	[Serializable]
	public struct SerializedType<T> : IEquatable<Type> where T : class
	{
		[SerializeField][FormerlySerializedAs("fullName")] private string assemblyQualifiedName;

		public SerializedType(string assemblyQualifiedName)
		{
			this.assemblyQualifiedName = assemblyQualifiedName;
		}

		public SerializedType(Type type) : this(type.AssemblyQualifiedName)
		{

		}

		public override string ToString()
		{
			return assemblyQualifiedName;
		}

		public T CreateInstance()
		{
			Type type = Type.GetType(assemblyQualifiedName, true);
			object objectInstance = Activator.CreateInstance(type);
			T typeInstance = objectInstance as T;
			return typeInstance;
		}

		public bool Equals(Type other)
		{
			return assemblyQualifiedName.Equals(other.AssemblyQualifiedName);
		}

		public static implicit operator SerializedType<T>(Type type) => new SerializedType<T>(type);
	}
}
