﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Crosstales.TPB.Util;

namespace Crosstales.TPB.EditorIntegration
{
	/// <summary>Editor window extension.</summary>
	//[InitializeOnLoad]
	public class ConfigWindow : ConfigBase
	{
		#region Variables

		private int tab;
		private int lastTab;

		#endregion


		#region EditorWindow methods

		[MenuItem("Window/" + Constants.ASSET_NAME, false, 1030)]
		public static void ShowWindow()
		{
			GetWindow(typeof(ConfigWindow));
		}

		public static void ShowWindow(int tab)
		{
			ConfigWindow window = GetWindow(typeof(ConfigWindow)) as ConfigWindow;
			if (window != null) window.tab = tab;
		}

		private void OnEnable()
		{
			titleContent = new GUIContent(Constants.ASSET_NAME_SHORT, Helper.Logo_Asset_Small);

			init();
		}

		private void OnDestroy()
		{
			save();
		}

		private void OnLostFocus()
		{
			save();
		}

		private void OnGUI()
		{
			//  tab = GUILayout.Toolbar(tab, new[] { "Build", "Config", "Help", "About" });
			tab = GUILayout.Toolbar(tab, new[] { "Build", "Config" });

			if (tab != lastTab)
			{
				lastTab = tab;
				GUI.FocusControl(null);
			}

			switch (tab)
			{
				case 0: showBuild(); break;
				case 1: showConfiguration(); break;
				case 2: showHelp(); break;
				default: showAbout(); break;
			}
		}

		private void OnInspectorUpdate()
		{
			Repaint();
		}

		#endregion
	}
}
#endif
// © 2018-2023 crosstales LLC (https://www.crosstales.com)