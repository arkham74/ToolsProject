#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Crosstales.Common.Util;

namespace Crosstales.TPB.Util
{
	/// <summary>Configuration for the asset.</summary>
	[InitializeOnLoad]
	public static class Config
	{
		#region Variables

		/// <summary>Enable or disable custom location for the cache.</summary>
		public static bool CUSTOM_PATH_BUILD = Constants.DEFAULT_CUSTOM_PATH_BUILD;

		/// <summary>TPS-cache path.</summary>
		private static string pathCache = Constants.DEFAULT_PATH_CACHE;

		public static string PATH_BUILD
		{
			get => CUSTOM_PATH_BUILD && !string.IsNullOrEmpty(pathCache) ? FileHelper.ValidatePath(pathCache) : Constants.DEFAULT_PATH_CACHE;
			set => pathCache = value;
		}

		private static int vcs = Constants.DEFAULT_VCS;

		/// <summary>Selected VCS-system (default: 0, 0 = none, 1 = git, 2 = SVN, 3 Mercurial, 4 = Collab, 5 = PlasticSCM).</summary>
		public static int VCS
		{
			get => vcs;
			set => vcs = Mathf.Clamp(value, 0, 5);
		}

		/// <summary>Enable or disable adding the product name to the build path.</summary>
		public static bool ADD_NAME_TO_PATH = Constants.DEFAULT_ADD_NAME_TO_PATH;

		/// <summary>Enable or disable adding the product version to the build path.</summary>
		public static bool ADD_VERSION_TO_PATH = Constants.DEFAULT_ADD_VERSION_TO_PATH;

		/// <summary>Enable or disable adding the current date and time to the build path.</summary>
		public static bool ADD_DATE_TO_PATH = Constants.DEFAULT_ADD_DATE_TO_PATH;

		/// <summary>The date format for the builds.</summary>
		public static string DATE_FORMAT = Constants.DEFAULT_DATE_FORMAT;

		/*
		/// <summary>Enable or disable batch mode for CLI operations.</summary>
		public static bool BATCHMODE = Constants.DEFAULT_BATCHMODE;

		/// <summary>Enable or disable quit Unity Editor for CLI operations.</summary>
		public static bool QUIT = Constants.DEFAULT_QUIT;

		/// <summary>Enable or disable graphics device in Unity Editor for CLI operations.</summary>
		public static bool NO_GRAPHICS = Constants.DEFAULT_NO_GRAPHICS;
		*/

		/// <summary>Execute static method 'ClassName.MethodName' in Unity before building.</summary>
		public static string EXECUTE_METHOD_PRE_BUILDING = string.Empty;

		/// <summary>Execute static method 'ClassName.MethodName' in Unity after building.</summary>
		public static string EXECUTE_METHOD_POST_BUILDING = string.Empty;

		/// <summary>Execute static method 'ClassName.MethodName' in Unity before a build.</summary>
		public static string EXECUTE_METHOD_PRE_BUILD = string.Empty;

		/// <summary>Execute static method 'ClassName.MethodName>' in Unity after a build.</summary>
		public static string EXECUTE_METHOD_POST_BUILD = string.Empty;

		/// <summary>Execute static method 'ClassName.MethodName' in Unity before all builds.</summary>
		public static string EXECUTE_METHOD_PRE_BUILD_ALL = string.Empty;

		/// <summary>Execute static method 'ClassName.MethodName>' in Unity after all builds.</summary>
		public static string EXECUTE_METHOD_POST_BUILD_ALL = string.Empty;

		/// <summary>Enable or disable deleting the 'UnityLockfile'.</summary>
		public static bool DELETE_LOCKFILE = Constants.DEFAULT_DELETE_LOCKFILE;

		/// <summary>Enable or disable the build confirmation dialog.</summary>
		public static bool CONFIRM_BUILD = Constants.DEFAULT_CONFIRM_BUILD;

		/// <summary>Enable or disable debug logging for the asset.</summary>
		public static bool DEBUG = Constants.DEFAULT_DEBUG;

		/// <summary>Enable or disable update-checks for the asset.</summary>
		public static bool UPDATE_CHECK = Constants.DEFAULT_UPDATE_CHECK;

		/// <summary>Enable or disable adding compile define "CT_TPB" for the asset.</summary>
		public static bool COMPILE_DEFINES = Constants.DEFAULT_COMPILE_DEFINES;

		/// <summary>Enable or disable the Windows platform.</summary>
		public static bool PLATFORM_WINDOWS;

		/// <summary>Enable or disable the macOS platform.</summary>
		public static bool PLATFORM_MAC;

		/// <summary>Enable or disable the Linux platform.</summary>
		public static bool PLATFORM_LINUX;

		/// <summary>Enable or disable the Android platform.</summary>
		public static bool PLATFORM_ANDROID;

		/// <summary>Enable or disable the iOS platform.</summary>
		public static bool PLATFORM_IOS;

		/// <summary>Enable or disable the WSA platform.</summary>
		public static bool PLATFORM_WSA;

		/// <summary>Enable or disable the WebGL platform.</summary>
		public static bool PLATFORM_WEBGL;

		/// <summary>Enable or disable the tvOS platform.</summary>
		public static bool PLATFORM_TVOS;

		/// <summary>Enable or disable the PS4 platform.</summary>
		public static bool PLATFORM_PS4;

		/// <summary>Enable or disable the XBoxOne platform.</summary>
		public static bool PLATFORM_XBOXONE;

		/// <summary>Enable or disable the Nintendo Switch platform.</summary>
		public static bool PLATFORM_SWITCH;

		/// <summary>Architecture of the Windows platform.</summary>
		public static int ARCH_WINDOWS = Constants.DEFAULT_ARCH_WINDOWS;

		/*
						/// <summary>Architecture of the macOS platform.</summary>
						public static int ARCH_MAC = Constants.DEFAULT_ARCH_MAC;
		*/
		/// <summary>Architecture of the Linux platform.</summary>
		public static int ARCH_LINUX = Constants.DEFAULT_ARCH_LINUX;

		/// <summary>Texture format of the Android platform.</summary>
		public static int TEX_ANDROID = Constants.DEFAULT_TEX_ANDROID;

		/// <summary>Enable or disable 'BuildOptions.ShowBuiltPlayer'.</summary>
		public static bool BO_SHOW_BUILT_PLAYER = Constants.DEFAULT_BO_SHOW_BUILT_PLAYER;

		/// <summary>Enable or disable 'BuildOptions.Development'.</summary>
		public static bool BO_DEVELOPMENT = Constants.DEFAULT_BO_DEVELOPMENT;

		/// <summary>Enable or disable 'BuildOptions.ConnectWithProfiler'.</summary>
		public static bool BO_PROFILER = Constants.DEFAULT_BO_PROFILER;

		/// <summary>Enable or disable 'BuildOptions.AllowDebugging'.</summary>
		public static bool BO_SCRIPTDEBUG = Constants.DEFAULT_BO_SCRIPTDEBUG;

		/// <summary>Enable or disable compressing the build result as ZIP.</summary>
		public static bool BO_COMPRESS = Constants.DEFAULT_BO_COMPRESS;

		/// <summary>Shows or hides the column for the platform.</summary>
		public static bool SHOW_COLUMN_PLATFORM = Constants.DEFAULT_SHOW_COLUMN_PLATFORM;

		/// <summary>Shows or hides the column for the platform.</summary>
		public static bool SHOW_COLUMN_PLATFORM_LOGO = Constants.DEFAULT_SHOW_COLUMN_PLATFORM_LOGO;

		/// <summary>Shows or hides the column for the architecture.</summary>
		public static bool SHOW_COLUMN_ARCHITECTURE = Constants.DEFAULT_SHOW_COLUMN_ARCHITECTURE;

		/*
		/// <summary>Shows or hides the column for the texture format.</summary>
		public static bool SHOW_COLUMN_TEXTURE = Constants.DEFAULT_SHOW_COLUMN_TEXTURE;
		*/

		/// <summary>Enable or disable automatic saving of all scenes.</summary>
		public static bool AUTO_SAVE = Constants.DEFAULT_AUTO_SAVE;

		/// <summary>Is the configuration loaded?</summary>
		public static bool isLoaded;

		private static string assetPath;
		private const string idPath = "Documentation/id/";
		private static readonly string idName = Constants.ASSET_UID + ".txt";

		#endregion


		#region Constructor

		static Config()
		{
			if (!isLoaded)
			{
				Load();

				if (DEBUG)
					Debug.Log("Config data loaded");
			}
		}

		#endregion


		#region Properties

		/// <summary>Returns the path to the asset inside the Unity project.</summary>
		/// <returns>The path to the asset inside the Unity project.</returns>
		public static string ASSET_PATH
		{
			get
			{
				if (assetPath == null)
				{
					try
					{
						if (System.IO.File.Exists(Constants.DEFAULT_ASSET_PATH + idPath + idName))
						{
							assetPath = Constants.DEFAULT_ASSET_PATH;
						}
						else
						{
							string[] files = System.IO.Directory.GetFiles(Application.dataPath, idName, System.IO.SearchOption.AllDirectories);

							if (files.Length > 0)
							{
								string name = files[0].Substring(Application.dataPath.Length);
								assetPath = name.Substring(0, name.Length - idPath.Length - idName.Length).Replace("\\", "/");
							}
							else
							{
								Debug.LogWarning("Could not locate the asset! File not found: " + idName);
								assetPath = Constants.DEFAULT_ASSET_PATH;
							}
						}
					}
					catch (System.Exception ex)
					{
						Debug.LogWarning("Could not locate asset: " + ex);
					}
				}

				return assetPath;
			}
		}

		#endregion


		#region Public static methods

		/// <summary>Resets all changeable variables to their default value.</summary>
		public static void Reset()
		{
			assetPath = null;

			setupPlatforms();

			CUSTOM_PATH_BUILD = Constants.DEFAULT_CUSTOM_PATH_BUILD;
			pathCache = Constants.DEFAULT_PATH_CACHE;
			VCS = Constants.DEFAULT_VCS;
			ADD_NAME_TO_PATH = Constants.DEFAULT_ADD_NAME_TO_PATH;
			ADD_VERSION_TO_PATH = Constants.DEFAULT_ADD_VERSION_TO_PATH;
			ADD_DATE_TO_PATH = Constants.DEFAULT_ADD_DATE_TO_PATH;
			DATE_FORMAT = Constants.DEFAULT_DATE_FORMAT;
			/*
			BATCHMODE = Constants.DEFAULT_BATCHMODE;
			QUIT = Constants.DEFAULT_QUIT;
			NO_GRAPHICS = Constants.DEFAULT_NO_GRAPHICS;
			*/
			EXECUTE_METHOD_PRE_BUILDING = string.Empty;
			EXECUTE_METHOD_POST_BUILDING = string.Empty;
			EXECUTE_METHOD_PRE_BUILD = string.Empty;
			EXECUTE_METHOD_POST_BUILD = string.Empty;
			EXECUTE_METHOD_PRE_BUILD_ALL = string.Empty;
			EXECUTE_METHOD_POST_BUILD_ALL = string.Empty;
			DELETE_LOCKFILE = Constants.DEFAULT_DELETE_LOCKFILE;
			CONFIRM_BUILD = Constants.DEFAULT_CONFIRM_BUILD;

			if (!Constants.DEV_DEBUG)
				DEBUG = Constants.DEFAULT_DEBUG;

			UPDATE_CHECK = Constants.DEFAULT_UPDATE_CHECK;
			COMPILE_DEFINES = Constants.DEFAULT_COMPILE_DEFINES;

			ARCH_WINDOWS = Constants.DEFAULT_ARCH_WINDOWS;
			//ARCH_MAC = Constants.DEFAULT_ARCH_MAC;
			ARCH_LINUX = Constants.DEFAULT_ARCH_LINUX;
			TEX_ANDROID = Constants.DEFAULT_TEX_ANDROID;

			BO_SHOW_BUILT_PLAYER = Constants.DEFAULT_BO_SHOW_BUILT_PLAYER;
			BO_DEVELOPMENT = Constants.DEFAULT_BO_DEVELOPMENT;
			BO_PROFILER = Constants.DEFAULT_BO_PROFILER;
			BO_SCRIPTDEBUG = Constants.DEFAULT_BO_SCRIPTDEBUG;
			BO_COMPRESS = Constants.DEFAULT_BO_COMPRESS;

			SHOW_COLUMN_PLATFORM = Constants.DEFAULT_SHOW_COLUMN_PLATFORM;
			SHOW_COLUMN_PLATFORM_LOGO = Constants.DEFAULT_SHOW_COLUMN_PLATFORM_LOGO;
			SHOW_COLUMN_ARCHITECTURE = Constants.DEFAULT_SHOW_COLUMN_ARCHITECTURE;
			//SHOW_COLUMN_TEXTURE = Constants.DEFAULT_SHOW_COLUMN_TEXTURE;

			AUTO_SAVE = Constants.DEFAULT_AUTO_SAVE;
		}

		/// <summary>Loads the all changeable variables.</summary>
		public static void Load()
		{
			assetPath = null;

			setupPlatforms();

			if (CTEditorPrefs.HasKey(Constants.KEY_CUSTOM_PATH_BUILD))
				CUSTOM_PATH_BUILD = CTEditorPrefs.GetBool(Constants.KEY_CUSTOM_PATH_BUILD);

			if (CTEditorPrefs.HasKey(Constants.KEY_PATH_BUILD))
				PATH_BUILD = CTEditorPrefs.GetString(Constants.KEY_PATH_BUILD);

			if (CTEditorPrefs.HasKey(Constants.KEY_VCS))
				VCS = CTEditorPrefs.GetInt(Constants.KEY_VCS);

			if (CTEditorPrefs.HasKey(Constants.KEY_ADD_NAME_TO_PATH))
				ADD_NAME_TO_PATH = CTEditorPrefs.GetBool(Constants.KEY_ADD_NAME_TO_PATH);

			if (CTEditorPrefs.HasKey(Constants.KEY_ADD_VERSION_TO_PATH))
				ADD_VERSION_TO_PATH = CTEditorPrefs.GetBool(Constants.KEY_ADD_VERSION_TO_PATH);

			if (CTEditorPrefs.HasKey(Constants.KEY_ADD_DATE_TO_PATH))
				ADD_DATE_TO_PATH = CTEditorPrefs.GetBool(Constants.KEY_ADD_DATE_TO_PATH);

			if (CTEditorPrefs.HasKey(Constants.KEY_DATE_FORMAT))
				DATE_FORMAT = CTEditorPrefs.GetString(Constants.KEY_DATE_FORMAT);

			/*
			if (CTEditorPrefs.HasKey(Constants.KEY_BATCHMODE))
					BATCHMODE = CTEditorPrefs.GetBool(Constants.KEY_BATCHMODE);

			if (CTEditorPrefs.HasKey(Constants.KEY_QUIT))
					QUIT = CTEditorPrefs.GetBool(Constants.KEY_QUIT);

			if (CTEditorPrefs.HasKey(Constants.KEY_NO_GRAPHICS))
					NO_GRAPHICS = CTEditorPrefs.GetBool(Constants.KEY_NO_GRAPHICS);
			*/

			if (CTEditorPrefs.HasKey(Constants.KEY_EXECUTE_METHOD_PRE_BUILDING))
				EXECUTE_METHOD_PRE_BUILDING = CTEditorPrefs.GetString(Constants.KEY_EXECUTE_METHOD_PRE_BUILDING);

			if (CTEditorPrefs.HasKey(Constants.KEY_EXECUTE_METHOD_POST_BUILDING))
				EXECUTE_METHOD_POST_BUILDING = CTEditorPrefs.GetString(Constants.KEY_EXECUTE_METHOD_POST_BUILDING);

			if (CTEditorPrefs.HasKey(Constants.KEY_EXECUTE_METHOD_PRE_BUILD))
				EXECUTE_METHOD_PRE_BUILD = CTEditorPrefs.GetString(Constants.KEY_EXECUTE_METHOD_PRE_BUILD);

			if (CTEditorPrefs.HasKey(Constants.KEY_EXECUTE_METHOD_POST_BUILD))
				EXECUTE_METHOD_POST_BUILD = CTEditorPrefs.GetString(Constants.KEY_EXECUTE_METHOD_POST_BUILD);

			if (CTEditorPrefs.HasKey(Constants.KEY_EXECUTE_METHOD_PRE_BUILD_ALL))
				EXECUTE_METHOD_PRE_BUILD_ALL = CTEditorPrefs.GetString(Constants.KEY_EXECUTE_METHOD_PRE_BUILD_ALL);

			if (CTEditorPrefs.HasKey(Constants.KEY_EXECUTE_METHOD_POST_BUILD_ALL))
				EXECUTE_METHOD_POST_BUILD_ALL = CTEditorPrefs.GetString(Constants.KEY_EXECUTE_METHOD_POST_BUILD_ALL);

			if (CTEditorPrefs.HasKey(Constants.KEY_DELETE_LOCKFILE))
				DELETE_LOCKFILE = CTEditorPrefs.GetBool(Constants.KEY_DELETE_LOCKFILE);

			if (CTEditorPrefs.HasKey(Constants.KEY_CONFIRM_BUILD))
				CONFIRM_BUILD = CTEditorPrefs.GetBool(Constants.KEY_CONFIRM_BUILD);

			if (!Constants.DEV_DEBUG)
			{
				if (CTEditorPrefs.HasKey(Constants.KEY_DEBUG))
					DEBUG = CTEditorPrefs.GetBool(Constants.KEY_DEBUG);
			}
			else
			{
				DEBUG = Constants.DEV_DEBUG;
			}

			if (CTEditorPrefs.HasKey(Constants.KEY_UPDATE_CHECK))
				UPDATE_CHECK = CTEditorPrefs.GetBool(Constants.KEY_UPDATE_CHECK);

			if (CTEditorPrefs.HasKey(Constants.KEY_COMPILE_DEFINES))
				COMPILE_DEFINES = CTEditorPrefs.GetBool(Constants.KEY_COMPILE_DEFINES);

			if (CTEditorPrefs.HasKey(Constants.KEY_PLATFORM_WINDOWS))
				PLATFORM_WINDOWS = CTEditorPrefs.GetBool(Constants.KEY_PLATFORM_WINDOWS);

			if (CTEditorPrefs.HasKey(Constants.KEY_PLATFORM_MAC))
				PLATFORM_MAC = CTEditorPrefs.GetBool(Constants.KEY_PLATFORM_MAC);

			if (CTEditorPrefs.HasKey(Constants.KEY_PLATFORM_LINUX))
				PLATFORM_LINUX = CTEditorPrefs.GetBool(Constants.KEY_PLATFORM_LINUX);

			if (CTEditorPrefs.HasKey(Constants.KEY_PLATFORM_ANDROID))
				PLATFORM_ANDROID = CTEditorPrefs.GetBool(Constants.KEY_PLATFORM_ANDROID);

			if (CTEditorPrefs.HasKey(Constants.KEY_PLATFORM_IOS))
				PLATFORM_IOS = CTEditorPrefs.GetBool(Constants.KEY_PLATFORM_IOS);

			if (CTEditorPrefs.HasKey(Constants.KEY_PLATFORM_WSA))
				PLATFORM_WSA = CTEditorPrefs.GetBool(Constants.KEY_PLATFORM_WSA);

			if (CTEditorPrefs.HasKey(Constants.KEY_PLATFORM_WEBGL))
				PLATFORM_WEBGL = CTEditorPrefs.GetBool(Constants.KEY_PLATFORM_WEBGL);

			if (CTEditorPrefs.HasKey(Constants.KEY_PLATFORM_TVOS))
				PLATFORM_TVOS = CTEditorPrefs.GetBool(Constants.KEY_PLATFORM_TVOS);

			if (CTEditorPrefs.HasKey(Constants.KEY_PLATFORM_PS4))
				PLATFORM_PS4 = CTEditorPrefs.GetBool(Constants.KEY_PLATFORM_PS4);

			if (CTEditorPrefs.HasKey(Constants.KEY_PLATFORM_XBOXONE))
				PLATFORM_XBOXONE = CTEditorPrefs.GetBool(Constants.KEY_PLATFORM_XBOXONE);

			if (CTEditorPrefs.HasKey(Constants.KEY_PLATFORM_SWITCH))
				PLATFORM_SWITCH = CTEditorPrefs.GetBool(Constants.KEY_PLATFORM_SWITCH);

			if (CTEditorPrefs.HasKey(Constants.KEY_ARCH_WINDOWS))
				ARCH_WINDOWS = CTEditorPrefs.GetInt(Constants.KEY_ARCH_WINDOWS);

			/*
									if (CTEditorPrefs.HasKey(Constants.KEY_ARCH_MAC))
									{
											ARCH_MAC = CTEditorPrefs.GetInt(Constants.KEY_ARCH_MAC);
									}
			*/
			if (CTEditorPrefs.HasKey(Constants.KEY_ARCH_LINUX))
				ARCH_LINUX = CTEditorPrefs.GetInt(Constants.KEY_ARCH_LINUX);

			if (CTEditorPrefs.HasKey(Constants.KEY_TEX_ANDROID))
				TEX_ANDROID = CTEditorPrefs.GetInt(Constants.KEY_TEX_ANDROID);

			if (CTEditorPrefs.HasKey(Constants.KEY_BO_SHOW_BUILT_PLAYER))
				BO_SHOW_BUILT_PLAYER = CTEditorPrefs.GetBool(Constants.KEY_BO_SHOW_BUILT_PLAYER);

			if (CTEditorPrefs.HasKey(Constants.KEY_BO_DEVELOPMENT))
				BO_DEVELOPMENT = CTEditorPrefs.GetBool(Constants.KEY_BO_DEVELOPMENT);

			if (CTEditorPrefs.HasKey(Constants.KEY_BO_PROFILER))
				BO_PROFILER = CTEditorPrefs.GetBool(Constants.KEY_BO_PROFILER);

			if (CTEditorPrefs.HasKey(Constants.KEY_BO_SCRIPTDEBUG))
				BO_SCRIPTDEBUG = CTEditorPrefs.GetBool(Constants.KEY_BO_SCRIPTDEBUG);

			if (CTEditorPrefs.HasKey(Constants.KEY_BO_COMPRESS))
				BO_COMPRESS = CTEditorPrefs.GetBool(Constants.KEY_BO_COMPRESS);

			if (CTEditorPrefs.HasKey(Constants.KEY_SHOW_COLUMN_PLATFORM))
				SHOW_COLUMN_PLATFORM = CTEditorPrefs.GetBool(Constants.KEY_SHOW_COLUMN_PLATFORM);

			if (CTEditorPrefs.HasKey(Constants.KEY_SHOW_COLUMN_ARCHITECTURE))
				SHOW_COLUMN_ARCHITECTURE = CTEditorPrefs.GetBool(Constants.KEY_SHOW_COLUMN_ARCHITECTURE);

			/*
			if (CTEditorPrefs.HasKey(Constants.KEY_SHOW_COLUMN_TEXTURE))
					SHOW_COLUMN_TEXTURE = CTEditorPrefs.GetBool(Constants.KEY_SHOW_COLUMN_TEXTURE);
			*/

			if (CTEditorPrefs.HasKey(Constants.KEY_AUTO_SAVE))
				AUTO_SAVE = CTEditorPrefs.GetBool(Constants.KEY_AUTO_SAVE);

			isLoaded = true;
		}

		/// <summary>Saves the all changeable variables.</summary>
		public static void Save()
		{
			CTEditorPrefs.SetBool(Constants.KEY_CUSTOM_PATH_BUILD, CUSTOM_PATH_BUILD);
			CTEditorPrefs.SetString(Constants.KEY_PATH_BUILD, PATH_BUILD);
			CTEditorPrefs.SetInt(Constants.KEY_VCS, VCS);
			CTEditorPrefs.SetBool(Constants.KEY_ADD_NAME_TO_PATH, ADD_NAME_TO_PATH);
			CTEditorPrefs.SetBool(Constants.KEY_ADD_VERSION_TO_PATH, ADD_VERSION_TO_PATH);
			CTEditorPrefs.SetBool(Constants.KEY_ADD_DATE_TO_PATH, ADD_DATE_TO_PATH);
			CTEditorPrefs.SetString(Constants.KEY_DATE_FORMAT, DATE_FORMAT);

			/*
			CTEditorPrefs.SetBool(Constants.KEY_BATCHMODE, BATCHMODE);
			CTEditorPrefs.SetBool(Constants.KEY_QUIT, QUIT);
			CTEditorPrefs.SetBool(Constants.KEY_NO_GRAPHICS, NO_GRAPHICS);
			*/

			CTEditorPrefs.SetString(Constants.KEY_EXECUTE_METHOD_PRE_BUILDING, EXECUTE_METHOD_PRE_BUILDING);
			CTEditorPrefs.SetString(Constants.KEY_EXECUTE_METHOD_POST_BUILDING, EXECUTE_METHOD_POST_BUILDING);
			CTEditorPrefs.SetString(Constants.KEY_EXECUTE_METHOD_PRE_BUILD, EXECUTE_METHOD_PRE_BUILD);
			CTEditorPrefs.SetString(Constants.KEY_EXECUTE_METHOD_POST_BUILD, EXECUTE_METHOD_POST_BUILD);
			CTEditorPrefs.SetString(Constants.KEY_EXECUTE_METHOD_PRE_BUILD_ALL, EXECUTE_METHOD_PRE_BUILD_ALL);
			CTEditorPrefs.SetString(Constants.KEY_EXECUTE_METHOD_POST_BUILD_ALL, EXECUTE_METHOD_POST_BUILD_ALL);
			CTEditorPrefs.SetBool(Constants.KEY_DELETE_LOCKFILE, DELETE_LOCKFILE);
			CTEditorPrefs.SetBool(Constants.KEY_CONFIRM_BUILD, CONFIRM_BUILD);

			if (!Constants.DEV_DEBUG)
				CTEditorPrefs.SetBool(Constants.KEY_DEBUG, DEBUG);

			CTEditorPrefs.SetBool(Constants.KEY_UPDATE_CHECK, UPDATE_CHECK);
			CTEditorPrefs.SetBool(Constants.KEY_COMPILE_DEFINES, COMPILE_DEFINES);

			CTEditorPrefs.SetBool(Constants.KEY_PLATFORM_WINDOWS, PLATFORM_WINDOWS);
			CTEditorPrefs.SetBool(Constants.KEY_PLATFORM_MAC, PLATFORM_MAC);
			CTEditorPrefs.SetBool(Constants.KEY_PLATFORM_LINUX, PLATFORM_LINUX);
			CTEditorPrefs.SetBool(Constants.KEY_PLATFORM_ANDROID, PLATFORM_ANDROID);
			CTEditorPrefs.SetBool(Constants.KEY_PLATFORM_IOS, PLATFORM_IOS);
			CTEditorPrefs.SetBool(Constants.KEY_PLATFORM_WSA, PLATFORM_WSA);
			CTEditorPrefs.SetBool(Constants.KEY_PLATFORM_WEBGL, PLATFORM_WEBGL);
			CTEditorPrefs.SetBool(Constants.KEY_PLATFORM_TVOS, PLATFORM_TVOS);
			CTEditorPrefs.SetBool(Constants.KEY_PLATFORM_PS4, PLATFORM_PS4);
			CTEditorPrefs.SetBool(Constants.KEY_PLATFORM_XBOXONE, PLATFORM_XBOXONE);
			CTEditorPrefs.SetBool(Constants.KEY_PLATFORM_SWITCH, PLATFORM_SWITCH);

			CTEditorPrefs.SetInt(Constants.KEY_ARCH_WINDOWS, ARCH_WINDOWS);
			//CTEditorPrefs.SetInt(Constants.KEY_ARCH_MAC, ARCH_MAC);
			CTEditorPrefs.SetInt(Constants.KEY_ARCH_LINUX, ARCH_LINUX);
			CTEditorPrefs.SetInt(Constants.KEY_TEX_ANDROID, TEX_ANDROID);

			CTEditorPrefs.SetBool(Constants.KEY_BO_SHOW_BUILT_PLAYER, BO_SHOW_BUILT_PLAYER);
			CTEditorPrefs.SetBool(Constants.KEY_BO_DEVELOPMENT, BO_DEVELOPMENT);
			CTEditorPrefs.SetBool(Constants.KEY_BO_PROFILER, BO_PROFILER);
			CTEditorPrefs.SetBool(Constants.KEY_BO_SCRIPTDEBUG, BO_SCRIPTDEBUG);
			CTEditorPrefs.SetBool(Constants.KEY_BO_COMPRESS, BO_COMPRESS);

			CTEditorPrefs.SetBool(Constants.KEY_SHOW_COLUMN_PLATFORM, SHOW_COLUMN_PLATFORM);
			CTEditorPrefs.SetBool(Constants.KEY_SHOW_COLUMN_ARCHITECTURE, SHOW_COLUMN_ARCHITECTURE);
			//CTEditorPrefs.SetBool(Constants.KEY_SHOW_COLUMN_TEXTURE, SHOW_COLUMN_TEXTURE);

			CTEditorPrefs.SetBool(Constants.KEY_AUTO_SAVE, AUTO_SAVE);

			CTEditorPrefs.Save();
		}

		#endregion

		private static void setupPlatforms()
		{
			PLATFORM_WINDOWS = Helper.isValidBuildTarget(BuildTarget.StandaloneWindows) || Helper.isValidBuildTarget(BuildTarget.StandaloneWindows64);
			PLATFORM_MAC = Helper.isValidBuildTarget(BuildTarget.StandaloneOSX);
			PLATFORM_LINUX = Helper.isValidBuildTarget(BuildTarget.StandaloneLinux64);
			PLATFORM_ANDROID = Helper.isValidBuildTarget(BuildTarget.Android);
			PLATFORM_IOS = Helper.isValidBuildTarget(BuildTarget.iOS);
			PLATFORM_WSA = Helper.isValidBuildTarget(BuildTarget.WSAPlayer);
			PLATFORM_WEBGL = Helper.isValidBuildTarget(BuildTarget.WebGL);
			PLATFORM_TVOS = Helper.isValidBuildTarget(BuildTarget.tvOS);
			PLATFORM_PS4 = Helper.isValidBuildTarget(BuildTarget.PS4);
			PLATFORM_XBOXONE = Helper.isValidBuildTarget(BuildTarget.XboxOne);
			PLATFORM_SWITCH = Helper.isValidBuildTarget(BuildTarget.Switch);
		}
	}
}
#endif
// © 2018-2023 crosstales LLC (https://www.crosstales.com)