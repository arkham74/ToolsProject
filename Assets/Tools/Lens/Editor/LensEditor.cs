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
using JD;
using Random = UnityEngine.Random;
using UnityEditor;
using System.IO;

namespace JD
{
	public class LensEditor
	{
		private const string NAME = "Lens";
		private const string LAST_INDEX_KEY = "lastIndex";
		private const string FOLDER_NAME = "Screenshots";
		private const string FILE_FORMAT = "{0}_{1:000}.png";

		[MenuItem("Tools/Take screenshot _F10")]
		private static void TakeScreenshot()
		{
			string key = Path.Combine(NAME, Application.companyName, Application.productName, LAST_INDEX_KEY);
			int lastIndex = EditorPrefs.GetInt(key, 0);
			string projectPath = Directory.GetParent(Application.dataPath).FullName;
			string screenshotsPath = Path.Combine(projectPath, FOLDER_NAME);
			string filePath = Path.Combine(screenshotsPath, string.Format(FILE_FORMAT, Application.productName, lastIndex));
			Directory.CreateDirectory(screenshotsPath);
			ScreenCapture.CaptureScreenshot(filePath);
			EditorPrefs.SetInt(key, ++lastIndex);
		}
	}
}