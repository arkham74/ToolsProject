using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace JD
{
	[AttributeUsage(AttributeTargets.Field)]
	public class TypeAttribute : PropertyAttribute
	{
		public readonly Type Type;

		public TypeAttribute(Type type)
		{
			Type = type;
		}
	}
}
