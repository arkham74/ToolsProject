using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
#if TOOLS_NAUATTR
using NaughtyAttributes;
#endif

namespace JD.Editor
{
	[CreateAssetMenu(fileName = "Batch Bake", menuName = "ScriptableObject/Batch Bake", order = 0)]
	public class BatchBake : ScriptableObject
	{
		[ScenePath] public string[] scenes;

		private void Reset()
		{
			scenes = EditorBuildSettings.scenes.Where(e => e.enabled).Select(e => e.path).ToArray();
		}

#if TOOLS_NAUATTR
		[Button]
#endif
		public void Bake()
		{
			if (Lightmapping.isRunning) return;
			Lightmapping.BakeMultipleScenes(scenes);
		}

#if TOOLS_NAUATTR
		[Button]
#endif
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

#if TOOLS_NAUATTR
		[Button]
#endif
		public void Cancel()
		{
			Lightmapping.Cancel();
		}

#if TOOLS_NAUATTR
		[Button]
#endif
		public void ForceStop()
		{
			Lightmapping.ForceStop();
		}
	}
}