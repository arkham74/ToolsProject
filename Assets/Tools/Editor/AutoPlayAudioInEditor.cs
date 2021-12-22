using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Tag = NaughtyAttributes.TagAttribute;

[InitializeOnLoad]
internal class AutoPlayAudioInEditor
{
	static AutoPlayAudioInEditor()
	{
		Selection.selectionChanged += OnSelectionChanged;
	}

	private static void OnSelectionChanged()
	{
		StopAllClips();
		if (Selection.activeObject is AudioClip clip)
		{
			PlayClip(clip);
		}
	}

	private static void PlayClip(AudioClip clip, int startSample = 0, bool loop = false)
	{
		Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;

		Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
		MethodInfo method = audioUtilClass.GetMethod("PlayPreviewClip", BindingFlags.Static | BindingFlags.Public, null,
			new[] {typeof(AudioClip), typeof(int), typeof(bool)}, null);
		method.Invoke(null, new object[] {clip, startSample, loop});
	}

	private static void StopAllClips()
	{
		Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
		Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
		MethodInfo method = audioUtilClass.GetMethod("StopAllPreviewClips", BindingFlags.Static | BindingFlags.Public, null,
			new Type[] { }, null);
		method.Invoke(null, new object[] { });
	}
}