using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Tag = NaughtyAttributes.TagAttribute;

// ReSharper disable UnusedMember.Local

public class GameSettingsEditorWindow : EditorWindow
{
	[MenuItem("Tools/Game Settings")]
	private static void Init()
	{
		GetWindow<GameSettingsEditorWindow>();
	}

	private void OnGUI()
	{
		Application.targetFrameRate = EditorGUILayout.IntSlider("Target Frame Rate", Application.targetFrameRate, -1, 1000);
		QualitySettings.maxQueuedFrames = EditorGUILayout.IntSlider("Max Queued Frames", QualitySettings.maxQueuedFrames, -1, 10);
		QualitySettings.vSyncCount = EditorGUILayout.IntSlider("V-Sync Count", QualitySettings.vSyncCount, 0, 4);
		GraphicsSettings.lightsUseLinearIntensity = EditorGUILayout.Toggle("Lights Use Linear Intensity", GraphicsSettings.lightsUseLinearIntensity);
		GraphicsSettings.lightsUseColorTemperature = EditorGUILayout.Toggle("Lights Use Color Temperature", GraphicsSettings.lightsUseColorTemperature);
		GraphicsSettings.transparencySortMode = (TransparencySortMode)EditorGUILayout.EnumPopup("Transparency Sort Mode", GraphicsSettings.transparencySortMode);
		if (GraphicsSettings.transparencySortMode == TransparencySortMode.CustomAxis)
			GraphicsSettings.transparencySortAxis = EditorGUILayout.Vector3Field("Transparency Sort Axis", GraphicsSettings.transparencySortAxis);
	}
}