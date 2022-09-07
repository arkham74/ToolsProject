using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace JD
{
	[AttributeUsage(AttributeTargets.Field)]
	public class TypeAttribute : PropertyAttribute
	{
		public readonly string[] Types;

		public TypeAttribute(Type type)
		{
			Types = TypeCache.GetTypesDerivedFrom(type).Where(e => !e.IsAbstract).Select(e => e.FullName).ToArray();
		}
	}
}
