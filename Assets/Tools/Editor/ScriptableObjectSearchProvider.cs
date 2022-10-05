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
using Random = UnityEngine.Random;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine.Rendering;
using System.IO;

namespace JD.Editor
{
	public class ScriptableObjectSearchProvider : ScriptableObject, ISearchWindowProvider
	{
		private static readonly float Width = 300;
		private static readonly float Height = 500;
		private static SearchWindowContext context = new SearchWindowContext(new Vector2(1800, 900) / 2f, Width, Height);
		private static ScriptableObjectSearchProvider provider;

		[MenuItem("Assets/Create Scriptable Object")]
		public static void CreateSO(MenuCommand menuCommand)
		{
			if (provider == null)
			{
				provider = CreateInstance<ScriptableObjectSearchProvider>();
			}

			SearchWindow.Open(context, provider);
		}

		private readonly List<SearchTreeEntry> list = new List<SearchTreeEntry>();

		public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
		{
			Texture2D soIcon = (Texture2D)EditorGUIUtility.IconContent("ScriptableObject Icon").image;
			Texture2D dirIcon = (Texture2D)EditorGUIUtility.IconContent("Folder Icon").image;
			list.Clear();
			IOrderedEnumerable<Type> types = TypeCache.GetTypesDerivedFrom<ScriptableObject>().OrderBy(Sort);
			list.Add(new SearchTreeGroupEntry(new GUIContent("Scriptable Objects")));

			Type editorType = typeof(UnityEditor.Editor);
			Type editorWindowType = typeof(EditorWindow);

#if TOOLS_URP
			Type volumeComponentType = typeof(VolumeComponent);
#endif

			Type scriptableRendererFeatureType = Type.GetType("UnityEngine.Rendering.Universal.ScriptableRendererFeature, Unity.RenderPipelines.Universal.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");

			List<string> groups = new List<string>();
			foreach (Type item in types)
			{
				if (item.IsSubclassOf(editorType))
				{
					continue;
				}

				if (item.IsSubclassOf(editorWindowType))
				{
					continue;
				}

#if TOOLS_URP
				if (item.IsSubclassOf(volumeComponentType))
				{
					continue;
				}
#endif

				if (scriptableRendererFeatureType != null)
				{
					if (item.IsSubclassOf(scriptableRendererFeatureType))
					{
						continue;
					}
				}

				if (item.IsAbstract)
				{
					continue;
				}

				if (item.IsNotPublic)
				{
					continue;
				}

				if (!item.IsPublic)
				{
					continue;
				}

				if (item.ContainsGenericParameters)
				{
					continue;
				}

				string[] entryTitle = item.FullName.Split('.');
				string groupName = "";
				for (int i = 0; i < entryTitle.Length - 1; i++)
				{
					groupName += entryTitle[i];
					if (!groups.Contains(groupName))
					{
						list.Add(new SearchTreeGroupEntry(new GUIContent(entryTitle[i], dirIcon), i + 1));
						groups.Add(groupName);
					}
					groupName += "/";
				}

				GUIContent content = new GUIContent(entryTitle.Last(), soIcon);
				SearchTreeEntry entry = new SearchTreeEntry(content)
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
			{
				return "ZZZZZZZZZZZZZZ" + arg.FullName;
			}

			return arg.FullName;
		}

		public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
		{
			Type type = entry.userData as Type;
			ScriptableObject instance = CreateInstance(type);
			string pathName = Path.ChangeExtension(type.Name, ".asset");
			ProjectWindowUtil.CreateAsset(instance, pathName);
			return true;
		}
	}
}
