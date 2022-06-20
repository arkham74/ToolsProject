using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
#if TOOLS_NAUATTR
using NaughtyAttributes;
#endif

[CreateAssetMenu(fileName = "Build Settings", menuName = "ScriptableObject/Build/Settings", order = 0)]
public class BuildSettings : ScriptableObject
{
	[ScenePath] public string[] scenes;
	public string locationPathName = "";
	public string assetBundleManifestPath;
	public BuildTargetGroup targetGroup = BuildTargetGroup.Standalone;
	public BuildTarget target = BuildTarget.StandaloneWindows64;
	public BuildOptions options = BuildOptions.StrictMode;
	public string[] extraScriptingDefines;

	private void Reset()
	{
		scenes = EditorBuildSettings.scenes.Where(e => e.enabled).Select(e => e.path).ToArray();
		string path = Path.GetDirectoryName(Application.dataPath);
		const string build = "Build";
		string productName = $"{Application.productName}.{GetExt()}";
		locationPathName = Path.Combine(path, build, productName).Replace(@"\", @"/");
	}

#if TOOLS_NAUATTR
	[Button(null, EButtonEnableMode.Editor)]
#endif
	public void LoadScenePaths()
	{
		scenes = EditorBuildSettings.scenes.Where(e => e.enabled).Select(e => e.path).ToArray();
	}

	private BuildReport BuildWithOptions(BuildOptions buildOptions = BuildOptions.None)
	{
		string path = Path.GetDirectoryName(locationPathName);
		if (Directory.Exists(path))
		{
			Directory.Delete(path, true);
		}
		BuildPlayerOptions buildPlayerOptions = BuildPlayerOptions();
		buildPlayerOptions.options |= buildOptions;
		return BuildPipeline.BuildPlayer(buildPlayerOptions);
	}

#if TOOLS_NAUATTR
	[Button(null, EButtonEnableMode.Editor)]
#endif
	private void Build()
	{
		IncrementVersion();
		BuildWithOptions();
	}

#if TOOLS_NAUATTR
	[Button(null, EButtonEnableMode.Editor)]
#endif
	private void BuildAndRun()
	{
		IncrementVersion();
		BuildWithOptions(BuildOptions.AutoRunPlayer);
	}

#if TOOLS_NAUATTR
	[Button(null, EButtonEnableMode.Editor)]
#endif
	private void BuildDev()
	{
		BuildWithOptions(BuildOptions.Development);
	}

#if TOOLS_NAUATTR
	[Button(null, EButtonEnableMode.Editor)]
#endif
	private void BuildAndRunDev()
	{
		BuildWithOptions(BuildOptions.AutoRunPlayer | BuildOptions.Development);
	}

	private BuildPlayerOptions BuildPlayerOptions()
	{
		return new BuildPlayerOptions
		{
			scenes = scenes,
			locationPathName = locationPathName,
			assetBundleManifestPath = assetBundleManifestPath,
			targetGroup = targetGroup,
			target = target,
			options = options,
			extraScriptingDefines = extraScriptingDefines,
		};
	}

	public async Task<BuildReport> BuildAsync(BuildOptions buildOptions = BuildOptions.None)
	{
		var report = BuildWithOptions(buildOptions);
		while (BuildPipeline.isBuildingPlayer)
			await Task.Delay(1000);
		return report;
	}

#if TOOLS_NAUATTR
	[Button(null, EButtonEnableMode.Editor)]
#endif
	public void SelectFolder()
	{
		string ext = GetExt();
		string path = EditorUtility.SaveFilePanel("Select Build Folder", Path.GetDirectoryName(locationPathName),
			Application.productName, ext);
		if (path != string.Empty) locationPathName = path;
	}

	private string GetExt()
	{
		return target switch
		{
			BuildTarget.Android => "apk",
			_ => "exe"
		};
	}

	public static void IncrementVersion()
	{
		string current = PlayerSettings.bundleVersion;
		int[] newVerInt = current.Split('.').Select(int.Parse).ToArray();
		newVerInt[2] += 1;
		string newVersion = string.Join(".", newVerInt);
		Debug.LogWarning($"{PlayerSettings.productName} Version: {newVersion}");
		PlayerSettings.bundleVersion = newVersion;
	}
}