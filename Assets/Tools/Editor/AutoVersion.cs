using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEditor.Callbacks;

[InitializeOnLoad]
public static class AutoVersion
{
	[PostProcessBuild(1)]
	private static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
	{
		IncrementVersion();
	}

	public static void IncrementVersion()
	{
		string current = PlayerSettings.bundleVersion;
		int[] newVerInt = current.Split('.').Select(int.Parse).ToArray();
		newVerInt[2] += 1;
		string newVersion = string.Join(".", newVerInt);
		Debug.LogWarning($"{PlayerSettings.productName} Version: {newVersion}");
		PlayerSettings.bundleVersion = newVersion;
	}
}