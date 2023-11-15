// using System.Runtime.CompilerServices;
// using Steamworks;
// using Steamworks.Data;

// namespace JD
// {
// 	public static class AchievementsExtensions
// 	{
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void ProgressAchievementStatInt(this string achiID, string statID, int max, int value = 1, int steps = 1)
// 		{
// 			int current = statID.GetStatInt();
// 			current += value;
// 			statID.SetStatInt(current);
// 			if (current % steps == 0 || current == 1 || current == max)
// 			{
// 				SteamUserStats.IndicateAchievementProgress(achiID, current, max);
// 			}
// 		}

// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool ProgressAndUnlock(this string achiID, int current, int max)
// 		{
// 			if (current < max)
// 			{
// 				return SteamUserStats.IndicateAchievementProgress(achiID, current, max);
// 			}
// 			else
// 			{
// 				Unlock(achiID);
// 				return true;
// 			}
// 		}

// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void Lock(this string id)
// 		{
// 			Achievement achievement = new Achievement(id);
// 			DebugTools.LogWarning("Clear", achievement.Identifier, achievement.State, achievement.Name, achievement.Description, achievement.UnlockTime);
// 			if (achievement.State)
// 			{
// 				achievement.Clear();
// 			}
// 		}

// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void Unlock(this string id)
// 		{
// 			Achievement achievement = new Achievement(id);
// 			DebugTools.LogWarning("Unlock", achievement.Identifier, achievement.State, achievement.Name, achievement.Description, achievement.UnlockTime);
// 			if (!achievement.State)
// 			{
// 				achievement.Trigger();
// 			}
// 		}

// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetStatInt(this string id, int value) => SteamUserStats.SetStat(id, value);

// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetStatFloat(this string id, float value) => SteamUserStats.SetStat(id, value);

// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static int GetStatInt(this string id) => SteamUserStats.GetStatInt(id);

// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static float GetStatFloat(this string id) => SteamUserStats.GetStatFloat(id);

// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void AddStatInt(this string id, int value = 1) => SteamUserStats.AddStat(id, value);

// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void AddStatFloat(this string id, float value = 1f) => SteamUserStats.AddStat(id, value);
// 	}
// }