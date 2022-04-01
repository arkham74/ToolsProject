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
using TMPro;
using NaughtyAttributes;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tag = NaughtyAttributes.TagAttribute;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine.Localization.SmartFormat.Utilities;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class ScriptableObjectSearchProvider : ScriptableObject, ISearchWindowProvider
{
	static SearchWindowContext context = new SearchWindowContext(new Vector2(1800, 900) / 2f);

	[MenuItem("Assets/Create Scriptable Object", false, 0)]
	public static void CreateSO(MenuCommand menuCommand)
	{
		ScriptableObjectSearchProvider provider = CreateInstance<ScriptableObjectSearchProvider>();
		SearchWindow.Open(context, provider);
	}

	private List<SearchTreeEntry> list = new List<SearchTreeEntry>();

	public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
	{
		list.Clear();
		var types = TypeCache.GetTypesDerivedFrom<ScriptableObject>().OrderBy(Sort);
		list.Add(new SearchTreeGroupEntry(new GUIContent("ScriptableObject")));

		List<string> groups = new List<string>();
		foreach (Type item in types)
		{
			if (item.IsSubclassOf(typeof(Editor))) continue;
			if (item.IsSubclassOf(typeof(EditorWindow))) continue;
			if (item.IsSubclassOf(typeof(ScriptableRendererFeature))) continue;
			if (item.IsSubclassOf(typeof(VolumeComponent))) continue;
			if (item.IsAbstract) continue;
			if (item.IsNotPublic) continue;
			if (!item.IsPublic) continue;
			if (item.ContainsGenericParameters) continue;

			string[] entryTitle = item.FullName.Split('.');
			// if (entryTitle.Length == 1)
			// 	entryTitle = new string[] { "GlobalNamespace", entryTitle[0] };
			string groupName = "";
			for (int i = 0; i < entryTitle.Length - 1; i++)
			{
				groupName += entryTitle[i];
				if (!groups.Contains(groupName))
				{
					list.Add(new SearchTreeGroupEntry(new GUIContent(entryTitle[i]), i + 1));
					groups.Add(groupName);
				}
				groupName += "/";
			}
			SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(entryTitle.Last()))
			{
				userData = item,
				level = entryTitle.Length,
			};
			list.Add(entry);
		}
		return list;
	}

	private string Sort(Type arg)
	{
		string[] parts = arg.FullName.Split('.');
		if (parts.Length == 1)
			return "ZZZZZZZZZZZZZZ" + arg.FullName;
		else
			return arg.FullName;
	}

	public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
	{
		Type type = entry.userData as Type;
		ScriptableObject instance = CreateInstance(type);
		AssetDatabase.CreateAsset(instance, "Assets/" + type.Name + ".asset");
		AssetDatabase.Refresh();
		return true;
	}
}