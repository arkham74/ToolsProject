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

		[MenuItem("Tools/Saves/Delete Persistent Data")]
		public static void DeletePersistentData()
		{
			string path = Application.persistentDataPath;
			if (Directory.Exists(path))
			{
				if (EditorUtility.DisplayDialog("Delete Persistent Data", "Delete Persistent Data?", "Yes", "No"))
				{
					Directory.Delete(path, true);
				}
			}
		}

		[MenuItem("Tools/Saves/Delete Player Prefs")]
		public static void DeletePlayerPrefs()
		{
			if (EditorUtility.DisplayDialog("Delete Player Prefs", "Delete Player Prefs?", "Yes", "No"))
			{
				PlayerPrefs.DeleteAll();
			}
		}

		[MenuItem("Tools/Saves/Delete Editor Prefs")]
		public static void DeleteEditorPrefs()
		{
			if (EditorUtility.DisplayDialog("Delete Editor Prefs", "Delete Editor Prefs?", "Yes", "No"))
			{
				EditorPrefs.DeleteAll();
			}
		}
	}
}
