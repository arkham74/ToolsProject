using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class DeleteLocalSaveEditor
{
	[MenuItem("Tools/Saves/Open Local Save")]
	public static void OpenLocalSave()
	{
		string path = Path.GetDirectoryName(Application.persistentDataPath);
		if (!Directory.Exists(path)) return;
		ProcessStartInfo startInfo = new ProcessStartInfo {Arguments = path, FileName = "explorer.exe"};
		Process.Start(startInfo);
	}

	[MenuItem("Tools/Saves/Delete Local Save")]
	public static void DeleteLocalSave()
	{
		string path = Application.persistentDataPath;
		if (!Directory.Exists(path)) return;
		if (!EditorUtility.DisplayDialog("Delete save", "Delete local save?", "Yes", "No")) return;
		Directory.Delete(path, true);
	}
}