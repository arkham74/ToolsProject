using IngameDebugConsole;
using JD;
using UnityEngine;

public static class DebugCommands
{
	[ConsoleMethod("debug.resolutions", "List all resolutions"), UnityEngine.Scripting.Preserve]
	public static void DebugResolutions()
	{
		Debug.Log("Filtered:\n" + ResolutionInfo.GetResolutions().Join() + "\n\nRaw:\n" + Screen.resolutions.Join() + "\n");
	}

	[ConsoleMethod("debug.maxRefreshRate", "Show max refresh rate"), UnityEngine.Scripting.Preserve]
	public static void DebugMaxRefreshRate()
	{
		Debug.Log(ResolutionInfo.GetMaxRefreshRate());
	}

	[ConsoleMethod("debug.resolutionIndex", "Get current resolution index"), UnityEngine.Scripting.Preserve]
	public static void DebugResolutionIndex()
	{
		Debug.Log(ResolutionInfo.GetResolutionIndex());
	}
}