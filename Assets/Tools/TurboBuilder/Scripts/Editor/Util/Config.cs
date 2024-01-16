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

			if (CTPrefs.HasKey(Constants.KEY_CUSTOM_PATH_BUILD))
				CUSTOM_PATH_BUILD = CTPrefs.GetBool(Constants.KEY_CUSTOM_PATH_BUILD);

			if (CTPrefs.HasKey(Constants.KEY_PATH_BUILD))
				PATH_BUILD = CTPrefs.GetString(Constants.KEY_PATH_BUILD);

			if (CTPrefs.HasKey(Constants.KEY_VCS))
				VCS = CTPrefs.GetInt(Constants.KEY_VCS);

			if (CTPrefs.HasKey(Constants.KEY_ADD_NAME_TO_PATH))
				ADD_NAME_TO_PATH = CTPrefs.GetBool(Constants.KEY_ADD_NAME_TO_PATH);

			if (CTPrefs.HasKey(Constants.KEY_ADD_VERSION_TO_PATH))
				ADD_VERSION_TO_PATH = CTPrefs.GetBool(Constants.KEY_ADD_VERSION_TO_PATH);

			if (CTPrefs.HasKey(Constants.KEY_ADD_DATE_TO_PATH))
				ADD_DATE_TO_PATH = CTPrefs.GetBool(Constants.KEY_ADD_DATE_TO_PATH);

			if (CTPrefs.HasKey(Constants.KEY_DATE_FORMAT))
				DATE_FORMAT = CTPrefs.GetString(Constants.KEY_DATE_FORMAT);

			/*
			if (CTEditorPrefs.HasKey(Constants.KEY_BATCHMODE))
					BATCHMODE = CTEditorPrefs.GetBool(Constants.KEY_BATCHMODE);

			if (CTEditorPrefs.HasKey(Constants.KEY_QUIT))
					QUIT = CTEditorPrefs.GetBool(Constants.KEY_QUIT);

			if (CTEditorPrefs.HasKey(Constants.KEY_NO_GRAPHICS))
					NO_GRAPHICS = CTEditorPrefs.GetBool(Constants.KEY_NO_GRAPHICS);
			*/

			if (CTPrefs.HasKey(Constants.KEY_EXECUTE_METHOD_PRE_BUILDING))
				EXECUTE_METHOD_PRE_BUILDING = CTPrefs.GetString(Constants.KEY_EXECUTE_METHOD_PRE_BUILDING);

			if (CTPrefs.HasKey(Constants.KEY_EXECUTE_METHOD_POST_BUILDING))
				EXECUTE_METHOD_POST_BUILDING = CTPrefs.GetString(Constants.KEY_EXECUTE_METHOD_POST_BUILDING);

			if (CTPrefs.HasKey(Constants.KEY_EXECUTE_METHOD_PRE_BUILD))
				EXECUTE_METHOD_PRE_BUILD = CTPrefs.GetString(Constants.KEY_EXECUTE_METHOD_PRE_BUILD);

			if (CTPrefs.HasKey(Constants.KEY_EXECUTE_METHOD_POST_BUILD))
				EXECUTE_METHOD_POST_BUILD = CTPrefs.GetString(Constants.KEY_EXECUTE_METHOD_POST_BUILD);

			if (CTPrefs.HasKey(Constants.KEY_EXECUTE_METHOD_PRE_BUILD_ALL))
				EXECUTE_METHOD_PRE_BUILD_ALL = CTPrefs.GetString(Constants.KEY_EXECUTE_METHOD_PRE_BUILD_ALL);

			if (CTPrefs.HasKey(Constants.KEY_EXECUTE_METHOD_POST_BUILD_ALL))
				EXECUTE_METHOD_POST_BUILD_ALL = CTPrefs.GetString(Constants.KEY_EXECUTE_METHOD_POST_BUILD_ALL);

			if (CTPrefs.HasKey(Constants.KEY_DELETE_LOCKFILE))
				DELETE_LOCKFILE = CTPrefs.GetBool(Constants.KEY_DELETE_LOCKFILE);

			if (CTPrefs.HasKey(Constants.KEY_CONFIRM_BUILD))
				CONFIRM_BUILD = CTPrefs.GetBool(Constants.KEY_CONFIRM_BUILD);

			if (!Constants.DEV_DEBUG)
			{
				if (CTPrefs.HasKey(Constants.KEY_DEBUG))
					DEBUG = CTPrefs.GetBool(Constants.KEY_DEBUG);
			}
			else
			{
				DEBUG = Constants.DEV_DEBUG;
			}

			if (CTPrefs.HasKey(Constants.KEY_UPDATE_CHECK))
				UPDATE_CHECK = CTPrefs.GetBool(Constants.KEY_UPDATE_CHECK);

			if (CTPrefs.HasKey(Constants.KEY_COMPILE_DEFINES))
				COMPILE_DEFINES = CTPrefs.GetBool(Constants.KEY_COMPILE_DEFINES);

			if (CTPrefs.HasKey(Constants.KEY_PLATFORM_WINDOWS))
				PLATFORM_WINDOWS = CTPrefs.GetBool(Constants.KEY_PLATFORM_WINDOWS);

			if (CTPrefs.HasKey(Constants.KEY_PLATFORM_MAC))
				PLATFORM_MAC = CTPrefs.GetBool(Constants.KEY_PLATFORM_MAC);

			if (CTPrefs.HasKey(Constants.KEY_PLATFORM_LINUX))
				PLATFORM_LINUX = CTPrefs.GetBool(Constants.KEY_PLATFORM_LINUX);

			if (CTPrefs.HasKey(Constants.KEY_PLATFORM_ANDROID))
				PLATFORM_ANDROID = CTPrefs.GetBool(Constants.KEY_PLATFORM_ANDROID);

			if (CTPrefs.HasKey(Constants.KEY_PLATFORM_IOS))
				PLATFORM_IOS = CTPrefs.GetBool(Constants.KEY_PLATFORM_IOS);

			if (CTPrefs.HasKey(Constants.KEY_PLATFORM_WSA))
				PLATFORM_WSA = CTPrefs.GetBool(Constants.KEY_PLATFORM_WSA);

			if (CTPrefs.HasKey(Constants.KEY_PLATFORM_WEBGL))
				PLATFORM_WEBGL = CTPrefs.GetBool(Constants.KEY_PLATFORM_WEBGL);

			if (CTPrefs.HasKey(Constants.KEY_PLATFORM_TVOS))
				PLATFORM_TVOS = CTPrefs.GetBool(Constants.KEY_PLATFORM_TVOS);

			if (CTPrefs.HasKey(Constants.KEY_PLATFORM_PS4))
				PLATFORM_PS4 = CTPrefs.GetBool(Constants.KEY_PLATFORM_PS4);

			if (CTPrefs.HasKey(Constants.KEY_PLATFORM_XBOXONE))
				PLATFORM_XBOXONE = CTPrefs.GetBool(Constants.KEY_PLATFORM_XBOXONE);

			if (CTPrefs.HasKey(Constants.KEY_PLATFORM_SWITCH))
				PLATFORM_SWITCH = CTPrefs.GetBool(Constants.KEY_PLATFORM_SWITCH);

			if (CTPrefs.HasKey(Constants.KEY_ARCH_WINDOWS))
				ARCH_WINDOWS = CTPrefs.GetInt(Constants.KEY_ARCH_WINDOWS);

			/*
									if (CTPrefs.HasKey(Constants.KEY_ARCH_MAC))
									{
											ARCH_MAC = CTPrefs.GetInt(Constants.KEY_ARCH_MAC);
									}
			*/
			if (CTPrefs.HasKey(Constants.KEY_ARCH_LINUX))
				ARCH_LINUX = CTPrefs.GetInt(Constants.KEY_ARCH_LINUX);

			if (CTPrefs.HasKey(Constants.KEY_TEX_ANDROID))
				TEX_ANDROID = CTPrefs.GetInt(Constants.KEY_TEX_ANDROID);

			if (CTPrefs.HasKey(Constants.KEY_BO_SHOW_BUILT_PLAYER))
				BO_SHOW_BUILT_PLAYER = CTPrefs.GetBool(Constants.KEY_BO_SHOW_BUILT_PLAYER);

			if (CTPrefs.HasKey(Constants.KEY_BO_DEVELOPMENT))
				BO_DEVELOPMENT = CTPrefs.GetBool(Constants.KEY_BO_DEVELOPMENT);

			if (CTPrefs.HasKey(Constants.KEY_BO_PROFILER))
				BO_PROFILER = CTPrefs.GetBool(Constants.KEY_BO_PROFILER);

			if (CTPrefs.HasKey(Constants.KEY_BO_SCRIPTDEBUG))
				BO_SCRIPTDEBUG = CTPrefs.GetBool(Constants.KEY_BO_SCRIPTDEBUG);

			if (CTPrefs.HasKey(Constants.KEY_BO_COMPRESS))
				BO_COMPRESS = CTPrefs.GetBool(Constants.KEY_BO_COMPRESS);

			if (CTPrefs.HasKey(Constants.KEY_SHOW_COLUMN_PLATFORM))
				SHOW_COLUMN_PLATFORM = CTPrefs.GetBool(Constants.KEY_SHOW_COLUMN_PLATFORM);

			if (CTPrefs.HasKey(Constants.KEY_SHOW_COLUMN_ARCHITECTURE))
				SHOW_COLUMN_ARCHITECTURE = CTPrefs.GetBool(Constants.KEY_SHOW_COLUMN_ARCHITECTURE);

			/*
			if (CTPrefs.HasKey(Constants.KEY_SHOW_COLUMN_TEXTURE))
					SHOW_COLUMN_TEXTURE = CTPrefs.GetBool(Constants.KEY_SHOW_COLUMN_TEXTURE);
			*/

			if (CTPrefs.HasKey(Constants.KEY_AUTO_SAVE))
				AUTO_SAVE = CTPrefs.GetBool(Constants.KEY_AUTO_SAVE);

			isLoaded = true;
		}

		/// <summary>Saves the all changeable variables.</summary>
		public static void Save()
		{
			CTPrefs.SetBool(Constants.KEY_CUSTOM_PATH_BUILD, CUSTOM_PATH_BUILD);
			CTPrefs.SetString(Constants.KEY_PATH_BUILD, PATH_BUILD);
			CTPrefs.SetInt(Constants.KEY_VCS, VCS);
			CTPrefs.SetBool(Constants.KEY_ADD_NAME_TO_PATH, ADD_NAME_TO_PATH);
			CTPrefs.SetBool(Constants.KEY_ADD_VERSION_TO_PATH, ADD_VERSION_TO_PATH);
			CTPrefs.SetBool(Constants.KEY_ADD_DATE_TO_PATH, ADD_DATE_TO_PATH);
			CTPrefs.SetString(Constants.KEY_DATE_FORMAT, DATE_FORMAT);

			/*
			CTPrefs.SetBool(Constants.KEY_BATCHMODE, BATCHMODE);
			CTPrefs.SetBool(Constants.KEY_QUIT, QUIT);
			CTPrefs.SetBool(Constants.KEY_NO_GRAPHICS, NO_GRAPHICS);
			*/

			CTPrefs.SetString(Constants.KEY_EXECUTE_METHOD_PRE_BUILDING, EXECUTE_METHOD_PRE_BUILDING);
			CTPrefs.SetString(Constants.KEY_EXECUTE_METHOD_POST_BUILDING, EXECUTE_METHOD_POST_BUILDING);
			CTPrefs.SetString(Constants.KEY_EXECUTE_METHOD_PRE_BUILD, EXECUTE_METHOD_PRE_BUILD);
			CTPrefs.SetString(Constants.KEY_EXECUTE_METHOD_POST_BUILD, EXECUTE_METHOD_POST_BUILD);
			CTPrefs.SetString(Constants.KEY_EXECUTE_METHOD_PRE_BUILD_ALL, EXECUTE_METHOD_PRE_BUILD_ALL);
			CTPrefs.SetString(Constants.KEY_EXECUTE_METHOD_POST_BUILD_ALL, EXECUTE_METHOD_POST_BUILD_ALL);
			CTPrefs.SetBool(Constants.KEY_DELETE_LOCKFILE, DELETE_LOCKFILE);
			CTPrefs.SetBool(Constants.KEY_CONFIRM_BUILD, CONFIRM_BUILD);

			if (!Constants.DEV_DEBUG)
				CTPrefs.SetBool(Constants.KEY_DEBUG, DEBUG);

			CTPrefs.SetBool(Constants.KEY_UPDATE_CHECK, UPDATE_CHECK);
			CTPrefs.SetBool(Constants.KEY_COMPILE_DEFINES, COMPILE_DEFINES);

			CTPrefs.SetBool(Constants.KEY_PLATFORM_WINDOWS, PLATFORM_WINDOWS);
			CTPrefs.SetBool(Constants.KEY_PLATFORM_MAC, PLATFORM_MAC);
			CTPrefs.SetBool(Constants.KEY_PLATFORM_LINUX, PLATFORM_LINUX);
			CTPrefs.SetBool(Constants.KEY_PLATFORM_ANDROID, PLATFORM_ANDROID);
			CTPrefs.SetBool(Constants.KEY_PLATFORM_IOS, PLATFORM_IOS);
			CTPrefs.SetBool(Constants.KEY_PLATFORM_WSA, PLATFORM_WSA);
			CTPrefs.SetBool(Constants.KEY_PLATFORM_WEBGL, PLATFORM_WEBGL);
			CTPrefs.SetBool(Constants.KEY_PLATFORM_TVOS, PLATFORM_TVOS);
			CTPrefs.SetBool(Constants.KEY_PLATFORM_PS4, PLATFORM_PS4);
			CTPrefs.SetBool(Constants.KEY_PLATFORM_XBOXONE, PLATFORM_XBOXONE);
			CTPrefs.SetBool(Constants.KEY_PLATFORM_SWITCH, PLATFORM_SWITCH);

			CTPrefs.SetInt(Constants.KEY_ARCH_WINDOWS, ARCH_WINDOWS);
			//CTPrefs.SetInt(Constants.KEY_ARCH_MAC, ARCH_MAC);
			CTPrefs.SetInt(Constants.KEY_ARCH_LINUX, ARCH_LINUX);
			CTPrefs.SetInt(Constants.KEY_TEX_ANDROID, TEX_ANDROID);

			CTPrefs.SetBool(Constants.KEY_BO_SHOW_BUILT_PLAYER, BO_SHOW_BUILT_PLAYER);
			CTPrefs.SetBool(Constants.KEY_BO_DEVELOPMENT, BO_DEVELOPMENT);
			CTPrefs.SetBool(Constants.KEY_BO_PROFILER, BO_PROFILER);
			CTPrefs.SetBool(Constants.KEY_BO_SCRIPTDEBUG, BO_SCRIPTDEBUG);
			CTPrefs.SetBool(Constants.KEY_BO_COMPRESS, BO_COMPRESS);

			CTPrefs.SetBool(Constants.KEY_SHOW_COLUMN_PLATFORM, SHOW_COLUMN_PLATFORM);
			CTPrefs.SetBool(Constants.KEY_SHOW_COLUMN_ARCHITECTURE, SHOW_COLUMN_ARCHITECTURE);
			//CTPrefs.SetBool(Constants.KEY_SHOW_COLUMN_TEXTURE, SHOW_COLUMN_TEXTURE);

			CTPrefs.SetBool(Constants.KEY_AUTO_SAVE, AUTO_SAVE);

			CTPrefs.Save();
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