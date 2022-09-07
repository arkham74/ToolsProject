using System;
using UnityEngine;

namespace SAR
{
	[Serializable]
	public struct SerializedType<T>
	{
		[SerializeField] private string fullName;

		private SerializedType(string fullName)
		{
			this.fullName = fullName;
		}

		public static implicit operator SerializedType<T>(string fullName) => new SerializedType<T>(fullName);
		public static implicit operator string(SerializedType<T> serializedType) => serializedType.fullName;
	}
}
