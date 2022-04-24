using IngameDebugConsole;
using Tayx.Graphy;

public static class GraphyCommands
{
	[ConsoleMethod("fps", "Shows fps counter"), UnityEngine.Scripting.Preserve]
	public static void ShowFps()
	{
		GraphyManager.Instance.ToggleActive();
	}
}
