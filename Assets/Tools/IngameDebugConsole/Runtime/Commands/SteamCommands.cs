#if UNITY_EDITOR || DEVELOPMENT_BUILD
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
using Random = UnityEngine.Random;
using IngameDebugConsole;
using UnityEngine.Scripting;
// using Steamworks;

public class SteamCommands : MonoBehaviour
{
	// [ConsoleMethod("steam.reset_all_stats", "Reset all user stats and achievements"), Preserve]
	// public static void SteamUserStatsResetAll()
	// {
	// 	if (SteamUserStats.ResetAll(true))
	// 	{
	// 		Debug.Log("Steam stats and achievements cleared");
	// 	}
	// }
}
#endif