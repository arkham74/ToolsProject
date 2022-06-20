using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class GameSettingsEditorWindow : EditorWindow
{
	// private static string testLine = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque blandit libero eu nibh dapibus, sed elementum ante pharetra. Ut porttitor, elit sed ultrices luctus, ante tellus imperdiet nibh, sed ornare ante purus et mi. Fusce pellentesque ultricies enim, ut euismod ipsum. Donec tempor leo augue, et vehicula dui imperdiet vitae. Duis malesuada egestas lacus. Integer vehicula ligula id turpis posuere pellentesque. Nulla id enim viverra, porta mauris vel, lobortis velit. Aliquam erat volutpat. Praesent vel ex scelerisque sem posuere pulvinar et ut diam. Mauris dapibus, velit sed pulvinar lacinia, tellus libero porttitor ex, vel accumsan leo justo a risus. Donec eu facilisis tortor. Suspendisse sit amet congue sapien.";
	// private static int charLimit = 10;

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

		// charLimit = EditorGUILayout.IntField(charLimit);
		// testLine = EditorGUILayout.TextField(testLine);
		// EditorGUILayout.TextArea(testLine.LinebreakAfter(charLimit));
	}
}