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
		private static readonly List<Resolution> resolutions = new List<Resolution>();

		public static List<Resolution> GetResolutions()
		{
			resolutions.Clear();
			int maxRefresh = GetMaxRefreshRate();
			resolutions.AddRange(Screen.resolutions.Where(e => e.refreshRate == maxRefresh));
			return resolutions;
		}

		private static int GetMaxRefreshRate()
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
