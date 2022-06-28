using System;
using System.Collections;
using System.IO;
using Steamworks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 162

namespace Steamworks
{
	public class SteamManager : MonoBehaviour
	{
		public static Action<bool> OnPaused = delegate { };

		public const uint APP_ID_SPACEWAR = 480;

		public static int PlayTime
		{
			get => SteamUserStats.GetStatInt("play_time");
			set => SteamUserStats.SetStat("play_time", value);
		}

		public static void Init(uint appID) => InitInternal(appID, false);
		public static void InitEditor(uint appID) => InitInternal(appID, true);

		private static void InitInternal(uint appID, bool inEditor)
		{
			if (!Application.isEditor || inEditor)
			{
				try
				{
					if (!Debug.isDebugBuild && appID != APP_ID_SPACEWAR)
					{
						if (SteamClient.RestartAppIfNecessary(appID))
						{
							Debug.LogWarning("Game is not launched through steam or steam is not running");
							Application.Quit();
							return;
						}
					}

					SteamClient.Init(appID);
					Debug.Log("Steam Manager Initialized");
					SteamManager instance = Instantiate(Resources.Load<SteamManager>("Managers/SteamManager"));
					instance.name = "Steam Manager";
					DontDestroyOnLoad(instance);
					SteamUtils.OnSteamShutdown += OnSteamShutdown;
					SteamFriends.OnGameOverlayActivated += OnOverlayActivated;
				}
				catch (Exception e)
				{
					Debug.LogError(e);
					QuitGameOrPlayMode();
				}
			}
		}

		private void OnDestroy()
		{
			SteamUtils.OnSteamShutdown -= OnSteamShutdown;
			SteamFriends.OnGameOverlayActivated -= OnOverlayActivated;
		}

		private static void QuitGameOrPlayMode()
		{
#if UNITY_EDITOR
			EditorApplication.ExitPlaymode();
#else
			Application.Quit();
#endif
		}

		private static void OnOverlayActivated(bool isPaused)
		{
			OnPaused(isPaused);
		}

		private void OnApplicationPause(bool isPaused)
		{
			OnPaused(isPaused);
		}

		private void OnApplicationFocus(bool isNotPaused)
		{
			OnPaused(!isNotPaused);
		}

		private static void OnSteamShutdown()
		{
			QuitGameOrPlayMode();
		}

		private void OnApplicationQuit()
		{
			PlayTime += Mathf.RoundToInt(Time.realtimeSinceStartup);
			SteamUserStats.StoreStats();
			GC.Collect();
			SteamClient.Shutdown();
		}
	}
}