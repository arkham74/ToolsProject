using System;
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
		public static Action<bool> OnFocus = delegate { };
		public const uint APP_ID_SPACEWAR = 480;

		public static int PlayTime
		{
			get => SteamUserStats.GetStatInt("play_time");
			set => SteamUserStats.SetStat("play_time", value);
		}

		public static void Init(uint appID, bool inEditor = false)
		{
			if (Application.isEditor && !inEditor) return;

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
				GameObject instance = Instantiate(Resources.Load<GameObject>("Managers/SteamManager"));
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

		private static void OnOverlayActivated(bool overlayStatus)
		{
			OnFocus(overlayStatus);
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			OnFocus(pauseStatus);
		}

		private void OnApplicationFocus(bool focusStatus)
		{
			OnFocus(!focusStatus);
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