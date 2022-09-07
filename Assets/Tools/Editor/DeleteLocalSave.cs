using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace JD.Editor
{
	public static class DeleteLocalSave
	{
		[MenuItem("Tools/Saves/Open Local Save")]
		public static void LocalSaveOpen()
		{
			string path = Path.GetDirectoryName(Application.persistentDataPath);
			path = Path.Combine(path, Application.productName);
			Directory.CreateDirectory(path);
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				Arguments = path,
				FileName = "explorer.exe"
			};
			Process.Start(startInfo);
		}

		[MenuItem("Tools/Saves/Delete Local Save")]
		public static void LocalSaveDelete()
		{
			string path = Application.persistentDataPath;
			if (Directory.Exists(path))
			{
				if (EditorUtility.DisplayDialog("Delete save", "Delete local save?", "Yes", "No"))
				{
					Directory.Delete(path, true);
					PlayerPrefs.DeleteAll();
					// EditorPrefs.DeleteAll();
				}
			}
		}
	}
}
