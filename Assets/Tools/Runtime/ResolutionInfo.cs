using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using Object = UnityEngine.Object;

#if TOOLS_LOCALIZATION
using UnityEngine.Localization.Settings;
#endif

namespace JD
{
	public static class ResolutionInfo
	{
		public static List<Resolution> GetResolutions()
		{
			HashSet<Resolution> set = new HashSet<Resolution>();
			int max = GetMaxRefreshRate();

			foreach (Resolution res in Screen.resolutions)
			{
				Resolution resolution = res;
				resolution.refreshRate = max;
				set.Add(resolution);
			}

			return set.ToList();
		}

		public static int GetMaxRefreshRate()
		{
			return Screen.resolutions.Max(e => e.refreshRate);
		}

		public static int GetResolutionIndex()
		{
			List<Resolution> list = GetResolutions();
			return list.FindIndex(e => e.height == Screen.height && e.width == Screen.width);
		}
	}
}
