using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using IngameDebugConsole;
using JD;
using UnityEngine.Scripting;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public static class ConsoleCommands
{
	[ConsoleMethod("screenParams", "Show current screen params")]
	[Preserve]
	public static void Screenparams()
	{
		StringBuilder sb = Pools.GetStringBuilder();
		sb.AppendFormat("currentResolution = {0}", Screen.currentResolution);
		sb.AppendLine();
		sb.AppendFormat("targetFrameRate = {0}", Application.targetFrameRate);
		sb.AppendLine();
		sb.AppendFormat("vSyncCount = {0}", QualitySettings.vSyncCount);
		sb.AppendLine();
		sb.AppendFormat("maxQueuedFrames = {0}", QualitySettings.maxQueuedFrames);
		sb.AppendLine();
		sb.AppendFormat("width = {0}, height = {1}", Screen.width, Screen.height);
		sb.AppendLine();
		DisplayInfo info = Screen.mainWindowDisplayInfo;
		RefreshRate refreshRate = info.refreshRate;
		sb.AppendFormat("mainWindowDisplayInfo = {0}", $"{info.name}, {info.width}, {info.height}, {refreshRate.value}({refreshRate.numerator}/{refreshRate.denominator}), {info.workArea}");
		sb.AppendLine();
		foreach (Display display in Display.displays)
		{
			sb.AppendFormat("active = {0}", display.active);
			sb.AppendLine();
			sb.AppendFormat("renderingWidth = {0}, renderingHeight = {1}", display.renderingWidth, display.renderingHeight);
			sb.AppendLine();
			sb.AppendFormat("systemWidth = {0}, systemHeight = {1}", display.systemWidth, display.systemHeight);
			sb.AppendLine();
		}

		Debug.LogWarning(sb);
		Pools.Release(sb);
	}

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