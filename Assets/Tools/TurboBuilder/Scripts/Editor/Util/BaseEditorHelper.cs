#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Crosstales.Common.EditorUtil
{
	/// <summary>Base for various Editor helper functions.</summary>
	public abstract class BaseEditorHelper : Crosstales.Common.Util.BaseHelper
	{
		#region Static variables

		private static readonly System.Type moduleManager = System.Type.GetType("UnityEditor.Modules.ModuleManager,UnityEditor.dll");
		private static readonly System.Reflection.MethodInfo isPlatformSupportLoaded = moduleManager.GetMethod("IsPlatformSupportLoaded", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
		private static readonly System.Reflection.MethodInfo getTargetStringFromBuildTarget = moduleManager.GetMethod("GetTargetStringFromBuildTarget", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

		private static Texture2D logo_asset_bwf;
		private static Texture2D logo_asset_dj;
		private static Texture2D logo_asset_fb;
		private static Texture2D logo_asset_oc;
		private static Texture2D logo_asset_radio;
		private static Texture2D logo_asset_rtv;
		private static Texture2D logo_asset_tb;
		private static Texture2D logo_asset_tpb;
		private static Texture2D logo_asset_tps;
		private static Texture2D logo_asset_tr;

		private static Texture2D logo_ct;
		private static Texture2D logo_unity;

		private static Texture2D icon_save;
		private static Texture2D icon_reset;
		private static Texture2D icon_refresh;
		private static Texture2D icon_delete;
		private static Texture2D icon_folder;
		private static Texture2D icon_plus;
		private static Texture2D icon_minus;

		private static Texture2D icon_manual;
		private static Texture2D icon_api;
		private static Texture2D icon_forum;
		private static Texture2D icon_product;

		private static Texture2D icon_check;

		private static Texture2D social_Discord;
		private static Texture2D social_Facebook;
		private static Texture2D social_Twitter;
		private static Texture2D social_Youtube;
		private static Texture2D social_Linkedin;

		private static Texture2D video_promo;
		private static Texture2D video_tutorial;

		private static Texture2D icon_videos;

		private static Texture2D icon_3p_assets;
		private static Texture2D asset_PlayMaker;
		private static Texture2D asset_VolumetricAudio;
		private static Texture2D asset_rocktomate;

		#endregion


		#region Static properties

		public static Texture2D Logo_Asset_BWF => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Logo_Asset_DJ => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Logo_Asset_FB => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Logo_Asset_OC => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Logo_Asset_Radio => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Logo_Asset_RTV => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Logo_Asset_TB => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Logo_Asset_TPB => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Logo_Asset_TPS => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Logo_Asset_TR => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Logo_CT => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Logo_Unity => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Icon_Save => EditorGUIUtility.IconContent("SaveActive").image as Texture2D;

		public static Texture2D Icon_Reset => EditorGUIUtility.IconContent("Preset.Context").image as Texture2D;

		public static Texture2D Icon_Refresh => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Icon_Delete => EditorGUIUtility.IconContent("TreeEditor.Trash").image as Texture2D;

		public static Texture2D Icon_Folder => EditorGUIUtility.IconContent("d_FolderOpened Icon").image as Texture2D;

		public static Texture2D Icon_Plus => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Icon_Minus => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Icon_Manual => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Icon_API => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Icon_Forum => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Icon_Product => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Icon_Check => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Social_Discord => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Social_Facebook => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Social_Twitter => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Social_Youtube => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Social_Linkedin => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Video_Promo => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Video_Tutorial => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Icon_Videos => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Icon_3p_Assets => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Asset_PlayMaker => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Asset_VolumetricAudio => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		public static Texture2D Asset_RockTomate => EditorGUIUtility.IconContent("d_UnityLogo").image as Texture2D;

		#endregion


		#region Public methods

		/// <summary>Restart Unity.</summary>
		/// <param name="executeMethod">Executed method after the restart (optional)</param>
		public static void RestartUnity(string executeMethod = "")
		{
			if (UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
			{
				bool success = false;

				using (System.Diagnostics.Process process = new System.Diagnostics.Process())
				{
					try
					{
						process.StartInfo.CreateNoWindow = true;
						process.StartInfo.UseShellExecute = false;

						string scriptfile;

						switch (Application.platform)
						{
							case RuntimePlatform.WindowsEditor:
								scriptfile = $"{System.IO.Path.GetTempPath()}RestartUnity-{System.Guid.NewGuid()}.cmd";

								System.IO.File.WriteAllText(scriptfile, generateWindowsRestartScript(executeMethod));

								process.StartInfo.FileName = "cmd.exe";
								process.StartInfo.Arguments = $"/c start  \"\" \"{scriptfile}\"";
								break;
							case RuntimePlatform.OSXEditor:
								scriptfile = $"{System.IO.Path.GetTempPath()}RestartUnity-{System.Guid.NewGuid()}.sh";

								System.IO.File.WriteAllText(scriptfile, generateMacRestartScript(executeMethod));

								process.StartInfo.FileName = "/bin/sh";
								process.StartInfo.Arguments = $"\"{scriptfile}\" &";
								break;
							case RuntimePlatform.LinuxEditor:
								scriptfile = $"{System.IO.Path.GetTempPath()}RestartUnity-{System.Guid.NewGuid()}.sh";

								System.IO.File.WriteAllText(scriptfile, generateLinuxRestartScript(executeMethod));

								process.StartInfo.FileName = "/bin/sh";
								process.StartInfo.Arguments = $"\"{scriptfile}\" &";
								break;
							default:
								Debug.LogError($"Unsupported Unity Editor: {Application.platform}");
								return;
						}

						process.Start();

						if (isWindowsEditor)
							process.WaitForExit(Crosstales.Common.Util.BaseConstants.PROCESS_KILL_TIME);

						success = true;
					}
					catch (System.Exception ex)
					{
						string errorMessage = $"Could restart Unity: {ex}";
						Debug.LogError(errorMessage);
					}
				}

				if (success)
					EditorApplication.Exit(0);
			}
			else
			{
				Debug.LogWarning("User canceled the restart.");
			}
		}

		/// <summary>Shows a separator-UI.</summary>
		/// <param name="space">Space in pixels between the component and the separator line (default: 12, optional).</param>
		public static void SeparatorUI(int space = 12)
		{
			GUILayout.Space(space);
			GUILayout.Box(string.Empty, GUILayout.ExpandWidth(true), GUILayout.Height(1));
		}

		/// <summary>Generates a read-only text field with a label.</summary>
		public static void ReadOnlyTextField(string label, string text)
		{
			EditorGUILayout.BeginHorizontal();
			{
				EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth - 4));
				EditorGUILayout.SelectableLabel(text, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
			}
			EditorGUILayout.EndHorizontal();
		}

		/// <summary>Refreshes the asset database.</summary>
		/// <param name="options">Asset import options (default: ImportAssetOptions.Default, optional).</param>
		public static void RefreshAssetDatabase(ImportAssetOptions options = ImportAssetOptions.Default)
		{
			if (!Application.isPlaying)
				AssetDatabase.Refresh(options);
		}

		/// <summary>Returns the true if the BuildTarget is installed in Unity.</summary>
		/// <param name="target">BuildTarget to test</param>
		/// <returns>True if the BuildTarget is installed in Unity.</returns>
		public static bool isValidBuildTarget(BuildTarget target)
		{
			return (bool)isPlatformSupportLoaded.Invoke(null, new object[] { (string)getTargetStringFromBuildTarget.Invoke(null, new object[] { target }) });
		}

		/*
		public static IEnumerable<BuildTarget> GetAvailableBuildTargets()
		{
				foreach (BuildTarget target in (BuildTarget[])Enum.GetValues(typeof(BuildTarget)))
				{
						BuildTargetGroup group = BuildPipeline.GetBuildTargetGroup(target);
						if (BuildPipeline.IsBuildTargetSupported(group, target))
						{
								yield return target;
						}
				}
		}
		*/

		/// <summary>Returns the BuildTarget for a build name, like 'win64'.</summary>
		/// <param name="build">Build name, like 'win64'</param>
		/// <returns>The BuildTarget for a build name.</returns>
		public static BuildTarget GetBuildTargetForBuildName(string build)
		{
			if ("win32".CTEquals(build) || "win".CTEquals(build))
				return BuildTarget.StandaloneWindows;

			if ("win64".CTEquals(build))
				return BuildTarget.StandaloneWindows64;

			if (!string.IsNullOrEmpty(build) && build.CTContains("osx"))
				return BuildTarget.StandaloneOSX;

			if (!string.IsNullOrEmpty(build) && build.CTContains("linux"))
				return BuildTarget.StandaloneLinux64;

			if ("android".CTEquals(build))
				return BuildTarget.Android;

			if ("ios".CTEquals(build))
				return BuildTarget.iOS;

			if ("wsaplayer".CTEquals(build) || "WindowsStoreApps".CTEquals(build))
				return BuildTarget.WSAPlayer;

			if ("webgl".CTEquals(build))
				return BuildTarget.WebGL;

			if ("tvOS".CTEquals(build))
				return BuildTarget.tvOS;

			if ("ps4".CTEquals(build))
				return BuildTarget.PS4;

			if ("xboxone".CTEquals(build))
				return BuildTarget.XboxOne;

			if ("switch".CTEquals(build))
				return BuildTarget.Switch;

			Debug.LogWarning($"Build target '{build}' not found! Returning 'StandaloneWindows64'.");
			return BuildTarget.StandaloneWindows64;
		}

		/// <summary>Returns the build name for a BuildTarget.</summary>
		/// <param name="build">BuildTarget for a build name</param>
		/// <returns>The build name for a BuildTarget.</returns>
		public static string GetBuildNameFromBuildTarget(BuildTarget build)
		{
			switch (build)
			{
				case BuildTarget.StandaloneWindows:
					return "Win";
				case BuildTarget.StandaloneWindows64:
					return "Win64";
				case BuildTarget.StandaloneOSX:
					return "OSXUniversal";
				case BuildTarget.StandaloneLinux64:
					return "Linux64";
				case BuildTarget.Android:
					return "Android";
				case BuildTarget.iOS:
					return "iOS";
				case BuildTarget.WSAPlayer:
					return "WindowsStoreApps";
				case BuildTarget.WebGL:
					return "WebGL";
				case BuildTarget.tvOS:
					return "tvOS";
				case BuildTarget.PS4:
					return "PS4";
				case BuildTarget.XboxOne:
					return "XboxOne";
				case BuildTarget.Switch:
					return "Switch";
				default:
					Debug.LogWarning($"Build target '{build}' not found! Returning Windows standalone.");
					return "Win64";
			}
		}

		/// <summary>Returns assets for a certain type.</summary>
		/// <returns>List of assets for a certain type.</returns>
		public static System.Collections.Generic.List<T> FindAssetsByType<T>() where T : Object
		{
			string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
			return guids.Select(AssetDatabase.GUIDToAssetPath).Select(AssetDatabase.LoadAssetAtPath<T>).Where(asset => asset != null).ToList();
		}

		/// <summary>
		/// Create and return a new asset in a smart location based on the current selection and then select it.
		/// </summary>
		/// <param name="name">Name of the new asset. Do not include the .asset extension.</param>
		/// <param name="showSaveFileBrowser">Shows the save file browser to select a destination for the asset (default: true, optional).</param>
		/// <returns>The new asset.</returns>
		public static T CreateAsset<T>(string name, bool showSaveFileBrowser = true) where T : ScriptableObject
		{
			string path;

			if (showSaveFileBrowser)
			{
				//string classname = nameof(T);
				path = EditorUtility.SaveFilePanelInProject($"Save asset {name}", name, "asset", "");
			}
			else
			{
				path = AssetDatabase.GetAssetPath(Selection.activeObject);
				if (path.Length == 0)
				{
					// no asset selected, place in asset root
					path = "Assets/" + name + ".asset";
				}
				else if (System.IO.Directory.Exists(path))
				{
					// place in currently selected directory
					path += "/" + name + ".asset";
				}
				else
				{
					// place in current selection's containing directory
					path = System.IO.Path.GetDirectoryName(path) + "/" + name + ".asset";
				}
			}

			if (string.IsNullOrEmpty(path))
				return default;

			T asset = ScriptableObject.CreateInstance<T>();
			AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(path));
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = asset;

			return asset;
		}

		/// <summary>Instantiates a prefab.</summary>
		/// <param name="prefabName">Name of the prefab.</param>
		/// <param name="path">Path to the prefab.</param>
		public static void InstantiatePrefab(string prefabName, string path)
		{
			PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath($"Assets{path}{prefabName}.prefab", typeof(GameObject)));

			if (isEditorMode)
				UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
		}

		#endregion


		#region Private methods

		private static string generateWindowsRestartScript(string executeMethod)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			// setup
			sb.AppendLine("@echo off");
			sb.AppendLine("cls");

			// title
			sb.Append("title Restart of ");
			sb.Append(Application.productName);
			sb.AppendLine(" - DO NOT CLOSE THIS WINDOW!");

			// header
			sb.AppendLine("echo ##############################################################################");
			sb.AppendLine("echo #                                                                            #");
			sb.AppendLine("echo #  Common 2023.1.0 - Windows                                                 #");
			sb.AppendLine("echo #  Copyright 2018-2023 by www.crosstales.com                                 #");
			sb.AppendLine("echo #                                                                            #");
			sb.AppendLine("echo #  This script restarts Unity.                                               #");
			sb.AppendLine("echo #  This will take some time, so please be patient and DON'T CLOSE THIS       #");
			sb.AppendLine("echo #  WINDOW before the process is finished!                                    #");
			sb.AppendLine("echo #                                                                            #");
			sb.AppendLine("echo ##############################################################################");
			sb.Append("echo ");
			sb.AppendLine(Application.productName);
			sb.AppendLine("echo.");
			sb.AppendLine("echo.");

			// check if Unity is closed
			sb.AppendLine(":waitloop");
			sb.Append("if not exist \"");
			sb.Append(Crosstales.Common.Util.BaseConstants.APPLICATION_PATH);
			sb.Append("Temp\\UnityLockfile\" goto waitloopend");
			sb.AppendLine();
			sb.AppendLine("echo.");
			sb.AppendLine("echo Waiting for Unity to close...");
			sb.AppendLine("timeout /t 3");

			sb.AppendLine("goto waitloop");
			sb.AppendLine(":waitloopend");

			// Restart Unity
			sb.AppendLine("echo.");
			sb.AppendLine("echo ##############################################################################");
			sb.AppendLine("echo #  Restarting Unity                                                          #");
			sb.AppendLine("echo ##############################################################################");
			sb.Append("start \"\" \"");
			sb.Append(Crosstales.Common.Util.FileHelper.ValidatePath(EditorApplication.applicationPath, false));
			sb.Append("\" -projectPath \"");
			sb.Append(Crosstales.Common.Util.BaseConstants.APPLICATION_PATH.Substring(0, Crosstales.Common.Util.BaseConstants.APPLICATION_PATH.Length - 1));
			sb.Append("\"");

			if (!string.IsNullOrEmpty(executeMethod))
			{
				sb.Append(" -executeMethod ");
				sb.Append(executeMethod);
			}

			sb.AppendLine();
			sb.AppendLine("echo.");

			// check if Unity is started
			sb.AppendLine(":waitloop2");
			sb.Append("if exist \"");
			sb.Append(Crosstales.Common.Util.BaseConstants.APPLICATION_PATH);
			sb.Append("Temp\\UnityLockfile\" goto waitloopend2");
			sb.AppendLine();
			sb.AppendLine("echo Waiting for Unity to start...");
			sb.AppendLine("timeout /t 3");
			sb.AppendLine("goto waitloop2");
			sb.AppendLine(":waitloopend2");
			sb.AppendLine("echo.");
			sb.AppendLine("echo Bye!");
			sb.AppendLine("timeout /t 1");
			sb.AppendLine("exit");

			return sb.ToString();
		}

		private static string generateMacRestartScript(string executeMethod)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			// setup
			sb.AppendLine("#!/bin/bash");
			sb.AppendLine("set +v");
			sb.AppendLine("clear");

			// title
			sb.Append("title='Relaunch of ");
			sb.Append(Application.productName);
			sb.AppendLine(" - DO NOT CLOSE THIS WINDOW!'");
			sb.AppendLine("echo -n -e \"\\033]0;$title\\007\"");

			// header
			sb.AppendLine("echo \"+----------------------------------------------------------------------------+\"");
			sb.AppendLine("echo \"¦                                                                            ¦\"");
			sb.AppendLine("echo \"¦  Common 2023.1.0 - macOS                                                   ¦\"");
			sb.AppendLine("echo \"¦  Copyright 2018-2023 by www.crosstales.com                                 ¦\"");
			sb.AppendLine("echo \"¦                                                                            ¦\"");
			sb.AppendLine("echo \"¦  This script restarts Unity.                                               ¦\"");
			sb.AppendLine("echo \"¦  This will take some time, so please be patient and DON'T CLOSE THIS       ¦\"");
			sb.AppendLine("echo \"¦  WINDOW before the process is finished!                                    ¦\"");
			sb.AppendLine("echo \"¦                                                                            ¦\"");
			sb.AppendLine("echo \"+----------------------------------------------------------------------------+\"");
			sb.Append("echo \"");
			sb.Append(Application.productName);
			sb.AppendLine("\"");
			sb.AppendLine("echo");
			sb.AppendLine("echo");

			// check if Unity is closed
			sb.Append("while [ -f \"");
			sb.Append(Crosstales.Common.Util.BaseConstants.APPLICATION_PATH);
			sb.Append("Temp/UnityLockfile\" ]");
			sb.AppendLine();
			sb.AppendLine("do");
			sb.AppendLine("  echo \"Waiting for Unity to close...\"");
			sb.AppendLine("  sleep 3");

			sb.AppendLine("done");

			// Restart Unity
			sb.AppendLine("echo");
			sb.AppendLine("echo \"+----------------------------------------------------------------------------+\"");
			sb.AppendLine("echo \"¦  Restarting Unity                                                          ¦\"");
			sb.AppendLine("echo \"+----------------------------------------------------------------------------+\"");
			sb.Append("open -a \"");
			sb.Append(EditorApplication.applicationPath);
			sb.Append("\" --args -projectPath \"");
			sb.Append(Crosstales.Common.Util.BaseConstants.APPLICATION_PATH);
			sb.Append("\"");

			if (!string.IsNullOrEmpty(executeMethod))
			{
				sb.Append(" -executeMethod ");
				sb.Append(executeMethod);
			}

			sb.AppendLine();

			//check if Unity is started
			sb.AppendLine("echo");
			sb.Append("while [ ! -f \"");
			sb.Append(Crosstales.Common.Util.BaseConstants.APPLICATION_PATH);
			sb.Append("Temp/UnityLockfile\" ]");
			sb.AppendLine();
			sb.AppendLine("do");
			sb.AppendLine("  echo \"Waiting for Unity to start...\"");
			sb.AppendLine("  sleep 3");
			sb.AppendLine("done");
			sb.AppendLine("echo");
			sb.AppendLine("echo \"Bye!\"");
			sb.AppendLine("sleep 1");
			sb.AppendLine("exit");

			return sb.ToString();
		}

		private static string generateLinuxRestartScript(string executeMethod)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			// setup
			sb.AppendLine("#!/bin/bash");
			sb.AppendLine("set +v");
			sb.AppendLine("clear");

			// title
			sb.Append("title='Relaunch of ");
			sb.Append(Application.productName);
			sb.AppendLine(" - DO NOT CLOSE THIS WINDOW!'");
			sb.AppendLine("echo -n -e \"\\033]0;$title\\007\"");

			// header
			sb.AppendLine("echo \"+----------------------------------------------------------------------------+\"");
			sb.AppendLine("echo \"¦                                                                            ¦\"");
			sb.AppendLine("echo \"¦  Common 2023.1.0 - Linux                                                   ¦\"");
			sb.AppendLine("echo \"¦  Copyright 2018-2023 by www.crosstales.com                                 ¦\"");
			sb.AppendLine("echo \"¦                                                                            ¦\"");
			sb.AppendLine("echo \"¦  This script restarts Unity.                                               ¦\"");
			sb.AppendLine("echo \"¦  This will take some time, so please be patient and DON'T CLOSE THIS       ¦\"");
			sb.AppendLine("echo \"¦  WINDOW before the process is finished!                                    ¦\"");
			sb.AppendLine("echo \"¦                                                                            ¦\"");
			sb.AppendLine("echo \"+----------------------------------------------------------------------------+\"");
			sb.Append("echo \"");
			sb.Append(Application.productName);
			sb.AppendLine("\"");
			sb.AppendLine("echo");
			sb.AppendLine("echo");

			// check if Unity is closed
			sb.Append("while [ -f \"");
			sb.Append(Crosstales.Common.Util.BaseConstants.APPLICATION_PATH);
			sb.Append("Temp/UnityLockfile\" ]");
			sb.AppendLine();
			sb.AppendLine("do");
			sb.AppendLine("  echo \"Waiting for Unity to close...\"");
			sb.AppendLine("  sleep 3");

			sb.AppendLine("done");

			// Restart Unity
			sb.AppendLine("echo");
			sb.AppendLine("echo \"+----------------------------------------------------------------------------+\"");
			sb.AppendLine("echo \"¦  Restarting Unity                                                          ¦\"");
			sb.AppendLine("echo \"+----------------------------------------------------------------------------+\"");
			sb.Append('"');
			sb.Append(EditorApplication.applicationPath);
			sb.Append("\" --args -projectPath \"");
			sb.Append(Crosstales.Common.Util.BaseConstants.APPLICATION_PATH);
			sb.Append("\"");

			if (!string.IsNullOrEmpty(executeMethod))
			{
				sb.Append(" -executeMethod ");
				sb.Append(executeMethod);
			}

			sb.Append(" &");
			sb.AppendLine();

			// check if Unity is started
			sb.AppendLine("echo");
			sb.Append("while [ ! -f \"");
			sb.Append(Crosstales.Common.Util.BaseConstants.APPLICATION_PATH);
			sb.Append("Temp/UnityLockfile\" ]");
			sb.AppendLine();
			sb.AppendLine("do");
			sb.AppendLine("  echo \"Waiting for Unity to start...\"");
			sb.AppendLine("  sleep 3");
			sb.AppendLine("done");
			sb.AppendLine("echo");
			sb.AppendLine("echo \"Bye!\"");
			sb.AppendLine("sleep 1");
			sb.AppendLine("exit");

			return sb.ToString();
		}


		#endregion
	}
}
#endif
// © 2018-2023 crosstales LLC (https://www.crosstales.com)