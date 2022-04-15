using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using IngameDebugConsole;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public static class ConsoleCommands
{
	[ConsoleMethod("show.cursor", "Show/Hide cursor"), UnityEngine.Scripting.Preserve]
	public static void ShowCursor()
	{
		ShowCursor(!Cursor.visible);
	}

	[ConsoleMethod("show.cursor", "Show/Hide cursor"), UnityEngine.Scripting.Preserve]
	public static void ShowCursor(bool value = true)
	{
		Cursor.visible = value;
		Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
	}

	[ConsoleMethod("fps", "Shows fps counter"), UnityEngine.Scripting.Preserve]
	public static void ShowFps()
	{
		Tayx.Graphy.GraphyManager.Instance.ToggleActive();
	}

	[ConsoleMethod("maxQueuedFrames", "Max Queued Frames"), UnityEngine.Scripting.Preserve]
	public static void MaxQueuedFrames(int maxQueuedFrames)
	{
		QualitySettings.maxQueuedFrames = maxQueuedFrames;
	}

	[ConsoleMethod("vSyncCount", "V-sync count"), UnityEngine.Scripting.Preserve]
	public static void VSyncCount(int vSyncCount)
	{
		QualitySettings.vSyncCount = vSyncCount;
	}

	[ConsoleMethod("targetFrameRate", "Limit framerate"), UnityEngine.Scripting.Preserve]
	public static void TargetFrameRate(int targetFrameRate)
	{
		Application.targetFrameRate = targetFrameRate;
	}
}