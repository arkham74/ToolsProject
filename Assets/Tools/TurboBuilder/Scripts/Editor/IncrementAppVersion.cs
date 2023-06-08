#if UNITY_EDITOR
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using UnityEditor;

namespace Crosstales.TPB
{
	[InitializeOnLoad]
	public class IncrementAppVersion : MonoBehaviour
	{
		static IncrementAppVersion()
		{
			Builder.OnBuildingComplete += IncrementVersion;
		}

		[MenuItem("Tools/IncrementVersion")]
		private static void IncrementVersion()
		{
			IncrementVersion(true);
		}

		private static void IncrementVersion(bool success)
		{
			if (success)
			{
				int[] ver = PlayerSettings.bundleVersion.Split(".").Select(int.Parse).ToArray();
				ver[2]++;
				PlayerSettings.bundleVersion = string.Join(".", ver);
			}
		}
	}
}
#endif