using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JD.Editor
{
	public static class SwapNamesEditor
	{
		[MenuItem("Assets/Swap names", true)]
		public static bool SwapNamesValidate(MenuCommand menuCommand)
		{
			return Selection.objects.Length == 2;
		}

		[MenuItem("Assets/Swap names")]
		public static void SwapNames(MenuCommand menuCommand)
		{
			Object file0 = Selection.objects[0];
			Object file1 = Selection.objects[1];

			string name0 = file0.name;
			string name1 = file1.name;

			Rename(file0, "TEMP");
			Rename(file1, name0);
			Rename(file0, name1);
		}

		private static void Rename(Object obj, string newName)
		{
			string path = AssetDatabase.GetAssetPath(obj);
			AssetDatabase.RenameAsset(path, newName);
		}
	}
}
