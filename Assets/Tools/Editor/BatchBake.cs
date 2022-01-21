using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NaughtyAttributes;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

// ReSharper disable UnusedMember.Local

[CreateAssetMenu(fileName = "Batch Bake", menuName = "ScriptableObject/Batch Bake", order = 0)]
public class BatchBake : ScriptableObject
{
	[ScenePath] public string[] scenes;

	private void Reset()
	{
		scenes = EditorBuildSettings.scenes.Where(e => e.enabled).Select(e => e.path).ToArray();
	}

	[Button]
	public void Bake()
	{
		if (Lightmapping.isRunning) return;
		Lightmapping.BakeMultipleScenes(scenes);
	}

	[Button]
	public void BakeSeparately()
	{
		if (Lightmapping.isRunning) return;

		foreach (string path in scenes)
		{
			EditorSceneManager.OpenScene(path);
			Lightmapping.Clear();
			Lightmapping.ClearDiskCache();
			Lightmapping.Bake();
		}
	}

	[Button]
	public void Cancel()
	{
		Lightmapping.Cancel();
	}

	[Button]
	public void ForceStop()
	{
		Lightmapping.ForceStop();
	}
}