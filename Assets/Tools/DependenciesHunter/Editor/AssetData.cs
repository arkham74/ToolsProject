// #define HUNT_ADDRESSABLES

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
#if UNITY_2021_2_OR_NEWER
using UnityEditor.Build;
#endif
#if HUNT_ADDRESSABLES
using UnityEditor.AddressableAssets;
#endif
using UnityEngine;
using UnityEngine.U2D;

// ReSharper disable once CheckNamespace
namespace DependenciesHunter
{
	public class AssetData
	{
		public static AssetData Create(string path, int referencesCount, string warning)
		{
			var type = AssetDatabase.GetMainAssetTypeAtPath(path);
			string typeName;

			if (type != null)
			{
				typeName = type.ToString();
				typeName = typeName.Replace("UnityEngine.", string.Empty);
				typeName = typeName.Replace("UnityEditor.", string.Empty);
			}
			else
			{
				typeName = "Unknown Type";
			}

			var isAddressable = CommonUtilities.IsAssetAddressable(path);

			var fileInfo = new FileInfo(path);
			var bytesSize = fileInfo.Length;
			return new AssetData(path, type, typeName, bytesSize,
					CommonUtilities.GetReadableSize(bytesSize), isAddressable, referencesCount, warning);
		}

		private AssetData(string path, Type type, string typeName, long bytesSize,
				string readableSize, bool addressable, int referencesCount, string warning)
		{
			Path = path;
			ShortPath = Path.Replace("Assets/", string.Empty);
			Type = type;
			TypeName = typeName;
			BytesSize = bytesSize;
			ReadableSize = readableSize;
			IsAddressable = addressable;
			ReferencesCount = referencesCount;
			Warning = warning;
		}

		public string Path { get; }
		public string ShortPath { get; }
		public Type Type { get; }
		public string TypeName { get; }
		public long BytesSize { get; }
		public string ReadableSize { get; }
		public bool IsAddressable { get; }
		public int ReferencesCount { get; }
		public string Warning { get; }
		public bool ValidType => Type != null;
		public bool Foldout { get; set; }
	}
}