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
		private const string playTimeKey = "play_time";

		public static int PlayTime
		{
			get => SteamUserStats.GetStatInt(playTimeKey);
			set => SteamUserStats.SetStat(playTimeKey, value);
		}

		public static void Init(uint appID, bool inEditor = true)
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
					SteamUserStats.RequestCurrentStats();
//					Debug.Log("Steam Manager Initialized");
					SteamManager instance = Instantiate(Resources.Load<SteamManager>("Managers/SteamManager"));
					instance.name = "Steam Manager";
					DontDestroyOnLoad(instance);
					SteamFriends.OnGameOverlayActivated += OnOverlayActivated;
					SteamUtils.OnSteamShutdown += Quit;
				}
				catch (Exception e)
				{
					Debug.LogError(e);
					Quit();
				}
			}
		}

		public static void Quit()
		{
			SteamUtils.OnSteamShutdown -= Quit;
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

		private void OnDestroy()
		{
			SteamFriends.OnGameOverlayActivated -= OnOverlayActivated;
			PlayTime += Mathf.RoundToInt(Time.realtimeSinceStartup);
			SteamUserStats.StoreStats();
			GC.Collect();
			SteamClient.Shutdown();
		}
	}
}
